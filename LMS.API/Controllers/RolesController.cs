using LMS.Application.DTOs.User;
using LMS.Application.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/roles
        [HttpGet]
        [Authorize(Policy = "Roles.View")]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        [Authorize(Policy = "Roles.View")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        // POST: api/roles
        [HttpPost]
        [Authorize(Policy = "Roles.Create")]
        public async Task<ActionResult<RoleDto>> Create([FromBody] RoleDto roleDto)
        {
            if (roleDto == null) return BadRequest();

            var createdRole = await _roleRepository.AddAsync(roleDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRole.Id }, createdRole);
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        [Authorize(Policy = "Roles.Update")]
        public async Task<ActionResult<RoleDto>> Update(int id, [FromBody] RoleDto roleDto)
        {
            if (roleDto == null || id != roleDto.Id) return BadRequest();

            try
            {
                var updatedRole = await _roleRepository.UpdateAsync(roleDto);
                return Ok(updatedRole);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "Roles.Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
