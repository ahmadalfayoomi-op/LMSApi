using Application.Auth;
using AutoMapper;
using LMS.Application.DTOs.Auth;
using LMS.Application.DTOs.Student;
using LMS.Application.Interfaces.Auth;
using LMS.Application.Interfaces.Instructor;
using LMS.Application.Interfaces.Student;
using LMS.Application.Interfaces.User;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace LMS.Infrastructure.Repositories.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService, IMapper mapper, AppDbContext db, IStudentRepository studentRepository, IInstructorRepository instructorRepository)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _db = db;
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
        }
        public async Task<RegisterDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            var chkIfExist = await _userRepository.GetByUsernameAsync(dto.Username);
            if (chkIfExist != null)
                throw new InvalidOperationException("The Username already exists.");

            var user = _mapper.Map<User>(dto);
            user.Password = PasswordHasher.HashPassword(dto.Password);

            Role? studentRole = null;
            if (dto.Type== "Student")
            {
                studentRole = await _db.Roles
                    .Include(r => r.Permissions)
                    .FirstOrDefaultAsync(r => r.Name == "Student");

            }
            else
            {
                studentRole = await _db.Roles
                    .Include(r => r.Permissions)
                    .FirstOrDefaultAsync(r => r.Name == "Instructor");
            }


            if (studentRole == null)
            {
                studentRole = new Role
                {
                    Name = "Student",
                    Permissions = new List<Permission>
            {
                new Permission { Key = "ViewCourses" },
                new Permission { Key = "EnrollCourse" },
                new Permission { Key = "ViewGrades" }
            }
                };
                _db.Roles.Add(studentRole);
                await _db.SaveChangesAsync(ct);
            }

                user.Roles.Add(studentRole);

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);

            if (dto.Type == "Student")
            {
                var student = _mapper.Map<LMS.Domain.Entities.Student>(dto);
                student.UserId = user.Id;
                _db.Students.Add(student);
                await _db.SaveChangesAsync(ct);
                return _mapper.Map<RegisterDto>(user);


            }
            else
            {
                var Instructor = _mapper.Map<LMS.Domain.Entities.Instructor>(dto);
                Instructor.UserId = user.Id;
                _db.Instructors.Add(Instructor);
                await _db.SaveChangesAsync(ct);
                return _mapper.Map<RegisterDto>(user);

            }

        }



        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid username or password.");

            int? studentId = null;
            int? InstructorId = null;

            // check if user has Student role
            if (user.StringRoles.Contains("Student"))
            {
                var student = await _studentRepository.GetByUserIdAsync(user.Id);
                if (student != null)
                    studentId = student.Id;

                StudentActivityLogDto logDto = new StudentActivityLogDto
                {
                    StudentId = studentId.HasValue ? studentId.Value : 0,
                    ActivityType = "Login",
                    Description = "Student logged in",
                    CreatedAt = DateTime.UtcNow
                };


                var activitylog = _mapper.Map<StudentActivityLog>(logDto);
                await _db.StudentActivityLogs.AddAsync(activitylog);
                await _db.SaveChangesAsync();
            }


            if (user.StringRoles.Contains("Instructor"))
            {
                var Instructor = await _instructorRepository.GetByUserIdAsync(user.Id);
                if (Instructor != null)
                    InstructorId = Instructor.Id;


            }





            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user , studentId, InstructorId);

            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id,
                Username = user.Username,
                Roles = user.StringRoles.ToList()
            };
        }
    }
}
