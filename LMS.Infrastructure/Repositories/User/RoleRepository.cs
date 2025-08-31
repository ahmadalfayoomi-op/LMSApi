using AutoMapper;
using LMS.Application.DTOs.User;
using LMS.Application.Interfaces.Configuration;
using LMS.Application.Interfaces.User;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.User
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public RoleRepository(AppDbContext db, IMapper mapper, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<List<RoleDto>> GetAllAsync(CancellationToken ct = default)
        {
            var roles = await _db.Roles
                .Include(r => r.Permissions) 
                .ToListAsync(ct);

            var roleDtos = _mapper.Map<List<RoleDto>>(roles);

            for (int i = 0; i < roles.Count; i++)
            {
                roleDtos[i].PermissionIds = roles[i].Permissions.Select(p => p.Id).ToList();
                roleDtos[i].PermissionNames = roles[i].Permissions.Select(p => p.Key).ToList();
            }

            return roleDtos;
        }

        public async Task<RoleDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var role = await _db.Roles
                .Include(r => r.Permissions) 
                .FirstOrDefaultAsync(r => r.Id == id, ct);

            if (role == null) return null;

            var roleDto = _mapper.Map<RoleDto>(role);
            roleDto.PermissionIds = role.Permissions.Select(p => p.Id).ToList();
            roleDto.PermissionNames = role.Permissions.Select(p => p.Key).ToList();

            return roleDto;
        }

        public async Task<RoleDto> AddAsync(RoleDto roleDto, CancellationToken ct = default)
        {
            var role = _mapper.Map<Role>(roleDto);

            if (roleDto.PermissionNames != null && roleDto.PermissionNames.Count > 0)
            {
                var newPermissions = roleDto.PermissionNames
                    .Select(name => new Permission { Key = name })
                    .ToList();

                foreach (var perm in newPermissions)
                {
                    _db.Permissions.Add(perm);
                    role.Permissions.Add(perm);
                }
            }

            _db.Roles.Add(role);
            await _db.SaveChangesAsync(ct);

            var resultDto = _mapper.Map<RoleDto>(role);
            resultDto.PermissionIds = role.Permissions.Select(p => p.Id).ToList();
            resultDto.PermissionNames = role.Permissions.Select(p => p.Key).ToList();

            return resultDto;
        }



        public async Task<RoleDto> UpdateAsync(RoleDto roleDto, CancellationToken ct = default)
        {
            var role = await _db.Roles
                .Include(r => r.Permissions) 
                .FirstOrDefaultAsync(r => r.Id == roleDto.Id, ct);

            if (role == null) throw new KeyNotFoundException("Role not found");

            _mapper.Map(roleDto, role);

            if (roleDto.PermissionNames != null && roleDto.PermissionNames.Count > 0)
            {
                role.Permissions.Clear();

                foreach (var permName in roleDto.PermissionNames)
                {
                    var permission = new Permission { Key = permName };
                    _db.Permissions.Add(permission); 
                    role.Permissions.Add(permission);
                }
            }

            _db.Roles.Update(role);
            await _db.SaveChangesAsync(ct);

            var resultDto = _mapper.Map<RoleDto>(role);
            resultDto.PermissionIds = role.Permissions.Select(p => p.Id).ToList();
            resultDto.PermissionNames = role.Permissions.Select(p => p.Key).ToList();

            return resultDto;
        }


        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var Role = await _db.Roles.FindAsync(id);
            if (Role != null)
            {
                _db.Roles.Remove(Role);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
