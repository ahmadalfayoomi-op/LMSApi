using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICateogryRepository _categoryRepo;

        public CategoriesController(ICateogryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // GET: api/categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var categories = await _categoryRepo.GetAllAsync(ct);
            return Ok(categories);
        }

        // GET: api/categories/{id}
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var category = await _categoryRepo.GetByIdAsync(id, ct);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        [Authorize(Policy = "Categories.Create")]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _categoryRepo.AddAsync(categoryDto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/categories/{id}
        [HttpPut("{id:int}")]
        [Authorize(Policy = "Categories.Update")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto, CancellationToken ct)
        {
            if (id != categoryDto.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _categoryRepo.UpdateAsync(categoryDto, ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Categories.Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _categoryRepo.DeleteAsync(id, ct);
            return NoContent();
        }
    }

}
