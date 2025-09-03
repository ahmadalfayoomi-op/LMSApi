using LMS.Application.DTOs.Ticket;
using LMS.Application.Interfaces.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly IKnowledgeBaseArticleRepository _articleRepo;

        public KnowledgeBaseController(
            IKnowledgeBaseArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        [HttpGet]
        [Authorize(Policy = "KnowledgeBaseArticle.View")]

        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleRepo.GetAllAsync();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "KnowledgeBaseArticle.View")]

        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _articleRepo.GetByIdAsync(id);
            if (article == null) return NotFound();
            return Ok(article);
        }

        [HttpPost]
        [Authorize(Policy = "KnowledgeBaseArticle.Add")]

        public async Task<IActionResult> AddAsync([FromBody] KnowledgeBaseArticleDto dto)
        {
            var id = await _articleRepo.AddAsync(dto);
            return Ok(id);
        }

        [HttpPut]
        [Authorize(Policy = "KnowledgeBaseArticle.Update")]

        public async Task<IActionResult> UpdateAsync([FromBody] KnowledgeBaseArticleDto dto)
        {
            var id = await _articleRepo.UpdateAsync(dto);
            return Ok(id);
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "KnowledgeBaseArticle.Delete")]

        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _articleRepo.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        [Authorize(Policy = "KnowledgeBaseArticle.Search")]

        public async Task<IActionResult> SearchArticles([FromQuery] string query, [FromQuery] int? categoryId)
        {
            var results = await _articleRepo.SearchAsync(query, categoryId);
            return Ok(results);
        }


    }
}
