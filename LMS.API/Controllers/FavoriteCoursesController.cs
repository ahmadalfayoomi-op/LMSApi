using LMS.Application.DTOs.Course;
using LMS.Application.Interfaces.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Student")]
    public class FavoriteCoursesController : ControllerBase
    {
        private readonly IFavoriteCourseRepository _favoriteRepo;

        public FavoriteCoursesController(IFavoriteCourseRepository favoriteRepo)
        {
            _favoriteRepo = favoriteRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteCourseDto>>> GetFavorites(CancellationToken ct)
        {
            var favorites = await _favoriteRepo.GetByStudentAsync(ct);
            return Ok(favorites);
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteCourseDto>> AddFavorite([FromBody] FavoriteCourseDto favoriteDto, CancellationToken ct)
        {
            if (favoriteDto == null || favoriteDto.CourseId <= 0)
                return BadRequest("Invalid favorite course data.");

            var result = await _favoriteRepo.AddAsync(favoriteDto, ct);
            return CreatedAtAction(nameof(GetFavorites), new { }, result);
        }

        [HttpDelete("{courseId:int}")]
        public async Task<IActionResult> RemoveFavorite(int courseId, CancellationToken ct)
        {
            await _favoriteRepo.RemoveAsync(courseId, ct);
            return NoContent();
        }
    }
}
