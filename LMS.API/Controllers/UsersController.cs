using LMS.Application.DTOs.User;
using LMS.Application.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserDto user)
        {
            await _repository.CreateAsync(user);

            return Ok("User created successfully");
        }
    }

}
