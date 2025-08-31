using Application.Auth;
using AutoMapper;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Student;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Student
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContext;


        public StudentRepository(AppDbContext db, IMapper mapper, IFileService fileService, IUserContextService userContext)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
            _userContext = userContext;
        }

        public async Task<List<StudentDto>> GetAllAsync(CancellationToken ct = default)
        {
            var Students = await _db.Students.ToListAsync(ct);
            return _mapper.Map<List<StudentDto>>(Students);
        }

        public async Task<StudentDto?> GetByIdAsync(CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var Student = await _db.Students.FindAsync(new object[] { studentId }, ct);
            return Student == null ? null : _mapper.Map<StudentDto>(Student);
        }
        public async Task<StudentDto?> GetByUserIdAsync(int id, CancellationToken ct = default)
        {
            var Student = await _db.Students.FirstOrDefaultAsync(c=>c.UserId==id , ct);
            return Student == null ? null : _mapper.Map<StudentDto>(Student);
        }
        public async Task<StudentDto> AddAsync(StudentDto studentDto, CancellationToken ct = default)
        {
            var user = _mapper.Map<LMS.Domain.Entities.User>(studentDto);
            user.Password = PasswordHasher.HashPassword(studentDto.Password);

            if (studentDto.ProfileImage != null)
            {
                var imagePath = await _fileService.SaveFileAsync(studentDto.ProfileImage, "App_File/users");
                user.ProfileImage = imagePath;
                studentDto.ProfileImagePath = imagePath;
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);

            var student = _mapper.Map<LMS.Domain.Entities.Student>(studentDto);
            student.UserId = user.Id;

            _db.Students.Add(student);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StudentDto>(student);
        }


        public async Task<StudentDto> UpdateAsync(StudentDto StudentDto, CancellationToken ct = default)
        {
            int studentId = _userContext.GetStudentId();

            var Student = await _db.Students.FindAsync(new object[] { studentId }, ct);
            if (Student == null) throw new KeyNotFoundException("Student not found");


            _mapper.Map(StudentDto, Student);
            Student.Id = studentId;

            _db.Students.Update(Student);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<StudentDto>(Student);
        }

        public async Task DeleteAsync(CancellationToken ct = default)
        {
            int id = _userContext.GetStudentId();
            var Student = await _db.Students.FindAsync(id);
            if (Student != null)
            {
                _db.Students.Remove(Student);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
