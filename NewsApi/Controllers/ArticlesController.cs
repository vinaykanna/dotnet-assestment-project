using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Controllers;

[ApiController]
[Route("api/news")]
public class NewsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        var articles = await _context.Articles
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(articles);
    }
}