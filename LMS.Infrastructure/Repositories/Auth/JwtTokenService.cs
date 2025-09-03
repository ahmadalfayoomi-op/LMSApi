using LMS.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LMS.Application.DTOs.User;
using LMS.Application.Interfaces.User;

namespace LMS.Infrastructure.Repositories.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly IRoleRepository _roleRepository;

        public JwtTokenService(IConfiguration config , IRoleRepository roleRepository)
        {
            _config = config;
            _roleRepository = roleRepository;
        }

        public string GenerateToken(UserDto user, int? studentId = null, int? InstructorId = null)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),

        };

            if (studentId.HasValue)
            {
                claims.Add(new Claim("studentId", studentId.Value.ToString()));
            }
            if (InstructorId.HasValue)
            {
                claims.Add(new Claim("instructorId", InstructorId.Value.ToString()));
            }


            foreach (var role in user.StringRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                foreach (var permName in user.PermissionNames)
                    claims.Add(new Claim("permission", permName));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
