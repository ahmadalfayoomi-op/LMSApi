using Application.Auth;
using AutoMapper;
using LMS.Application.DTOs.User;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.User;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly IFileService _fileService;


        public UserRepository(IMapper mapper, AppDbContext db, IFileService fileService)
        {
            _mapper = mapper;
            _db = db;
            _fileService = fileService;
        }

        public async Task<UserDto> CreateAsync(UserDto dto, CancellationToken ct = default)
        {
            var user = _mapper.Map<LMS.Domain.Entities.User>(dto);
            user.Password = PasswordHasher.HashPassword(dto.Password);

            if (dto.ProfileImage != null)
            {
                var imagePath = await _fileService.SaveFileAsync(dto.ProfileImage, "App_File/users");
                user.ProfileImage = imagePath;
                dto.ProfileImagePath = imagePath; 
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);
            return _mapper.Map<UserDto>(user);
        }


        public async Task<UserDto?> GetByUsernameAsync(string username, CancellationToken ct = default)
        {
            var user = await _db.Users
                .Include(u => u.Roles)                     
                    .ThenInclude(r => r.Permissions)       
                .FirstOrDefaultAsync(u => u.Username == username, ct);

            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetAllAsync(CancellationToken ct = default)
        {
            var Users = await _db.Users.ToListAsync(ct);
            return _mapper.Map<List<UserDto>>(Users);
        }
    }


}
