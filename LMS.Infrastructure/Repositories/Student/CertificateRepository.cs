using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;


namespace LMS.Infrastructure.Repositories.Student
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContext;



        public CertificateRepository(
            AppDbContext context,
            IMapper mapper,
            IFileService fileService,
            IUserContextService userContext)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
            _userContext = userContext;
        }

        public async Task<IEnumerable<CertificateDto>> GetAllByStudentAsync(CancellationToken ct = default)
        {

            int studentId = _userContext.GetStudentId();

            var certificates = await _context.Certificates
                .Where(c => c.StudentId == studentId)
                .OrderByDescending(c => c.IssuedAt)
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<CertificateDto>>(certificates);
        }

        public async Task<CertificateDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var certificate = await _context.Certificates.FindAsync(new object[] { id }, ct);
            return certificate == null ? null : _mapper.Map<CertificateDto>(certificate);
        }

        public async Task<CertificateDto> CreateCertificateAsync(int courseId, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == studentId, ct);
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId, ct);

            if (student == null) throw new KeyNotFoundException("Student not found");
            if (course == null) throw new KeyNotFoundException("Course not found");

            byte[] pdfBytes = GenerateCertificatePdf(student.User!.FirstName + " " + student.User.LastName, course.Title, DateTime.UtcNow);

            using (var ms = new MemoryStream(pdfBytes))
            {
                var formFile = new FormFile(ms, 0, pdfBytes.Length, "certificate", "certificate.pdf")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/pdf"
                };

                string url = await _fileService.SaveFileAsync(formFile, "App_File/certificates");

                var certificate = new Certificate
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    IssuedAt = DateTime.UtcNow,
                    CertificateUrl = url
                };

                _context.Certificates.Add(certificate);
                await _context.SaveChangesAsync(ct);


                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = studentId,
                    ActivityType = "CertificateIssued",
                    Description = $"Issued certificate for Course {course.Title}",
                    CreatedAt = DateTime.UtcNow
                };
                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _context.StudentActivityLogs.AddAsync(activitylog);
                await _context.SaveChangesAsync(ct);

                return _mapper.Map<CertificateDto>(certificate);
            }
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var certificate = await _context.Certificates.FindAsync(new object[] { id }, ct);
            if (certificate != null)
            {
                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = certificate.StudentId,
                    ActivityType = "CertificateIssued",
                    Description = $"Issued certificate for Course {certificate.Course.Title}",
                    CreatedAt = DateTime.UtcNow
                };
                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _context.StudentActivityLogs.AddAsync(activitylog);
                _context.Certificates.Remove(certificate);
                await _context.SaveChangesAsync(ct);
            }
        }

        private byte[] GenerateCertificatePdf(string studentName, string courseName, DateTime date)
        {
            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(QuestPDF.Helpers.PageSizes.A4);
                    page.Margin(50);
                    page.Content().Column(column =>
                    {
                        column.Item().Text("Certificate of Completion").FontSize(36).Bold().AlignCenter();
                        column.Item().Text($"This is to certify that").FontSize(18).AlignCenter();
                        column.Item().Text(studentName).FontSize(24).Bold().AlignCenter();
                        column.Item().Text($"has successfully completed the course").FontSize(18).AlignCenter();
                        column.Item().Text(courseName).FontSize(24).Bold().AlignCenter();
                        column.Item().Text($"Date: {date:MMMM dd, yyyy}").FontSize(14).AlignCenter();
                    });
                });
            }).GeneratePdf();

            return pdfBytes;
        }
    }

}
