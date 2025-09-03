using Application.Auth;
using AutoMapper;
using LMS.Application.DTOs.Instructor;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.Instructor;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Instructor
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserContextService _userContext;


        public InstructorRepository(AppDbContext db, IMapper mapper, IFileService fileService, IUserContextService userContext)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
            _userContext = userContext;
        }

        public async Task<List<InstructorDto>> GetAllAsync(CancellationToken ct = default)
        {
            var Instructors = await _db.Instructors.ToListAsync(ct);
            return _mapper.Map<List<InstructorDto>>(Instructors);
        }

        public async Task<InstructorDto?> GetByIdAsync(CancellationToken ct = default)
        {
            int InstructorId = _userContext.GetInstructorId();

            var Instructor = await _db.Instructors.FindAsync(new object[] { InstructorId }, ct);
            return Instructor == null ? null : _mapper.Map<InstructorDto>(Instructor);
        }
        public async Task<InstructorDto?> GetByUserIdAsync(int id, CancellationToken ct = default)
        {
            var Instructor = await _db.Instructors.FirstOrDefaultAsync(c => c.UserId == id, ct);
            return Instructor == null ? null : _mapper.Map<InstructorDto>((object)Instructor);
        }
        public async Task<InstructorDto> AddAsync(InstructorDto InstructorDto, CancellationToken ct = default)
        {
            var user = _mapper.Map<LMS.Domain.Entities.User>(InstructorDto);
            user.Password = PasswordHasher.HashPassword(InstructorDto.Password);

            if (InstructorDto.ProfileImage != null)
            {
                var imagePath = await _fileService.SaveFileAsync(InstructorDto.ProfileImage, "App_File/users");
                user.ProfileImage = imagePath;
                InstructorDto.ProfileImagePath = imagePath;
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);

            var Instructor = _mapper.Map<LMS.Domain.Entities.Instructor>(InstructorDto);
            Instructor.UserId = user.Id;

            _db.Instructors.Add(Instructor);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<InstructorDto>(Instructor);
        }


        public async Task<InstructorDto> UpdateAsync(InstructorDto InstructorDto, CancellationToken ct = default)
        {
            int InstructorId = _userContext.GetInstructorId();

            var Instructor = await _db.Instructors.FindAsync(new object[] { InstructorId }, ct);
            if (Instructor == null) throw new KeyNotFoundException("Instructor not found");


            _mapper.Map(InstructorDto, Instructor);
            Instructor.Id = InstructorId;

            _db.Instructors.Update(Instructor);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<InstructorDto>(Instructor);
        }

        public async Task DeleteAsync(CancellationToken ct = default)
        {
            int id = _userContext.GetInstructorId();
            var Instructor = await _db.Instructors.FindAsync(id);
            if (Instructor != null)
            {
                _db.Instructors.Remove(Instructor);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
