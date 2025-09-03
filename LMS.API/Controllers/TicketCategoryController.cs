using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TicketCategoryController : ControllerBase
    {
        private readonly ITicketCategoryRepository _categoryRepo;

        public TicketCategoryController(
            ITicketCategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        [HttpGet]
        [Authorize(Policy = "TicketCategory.View")]

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "TicketCategory.View")]

        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Policy = "TicketCategory.Create")]

        public async Task<IActionResult> AddAsync([FromBody] TicketCategoryDto dto)
        {
            var id = await _categoryRepo.AddAsync(dto);
            return Ok(id);
        }

        [HttpPut]
        [Authorize(Policy = "TicketCategory.Update")]

        public async Task<IActionResult> UpdateAsync([FromBody] TicketCategoryDto dto)
        {
            var id = await _categoryRepo.UpdateAsync(dto);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "TicketCategory.Delete")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
