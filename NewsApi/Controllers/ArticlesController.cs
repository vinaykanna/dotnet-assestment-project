using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApi.DTOs;
using NewsApi.Enums;
using NewsApi.Models;
using NewsApi.Services;
namespace NewsApi.Controllers;

[ApiController]
[Route("api/articles")]
public class NewsController(IArticlesService articlesService) : ControllerBase
{
    private readonly IArticlesService _articlesService = articlesService;
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Article>>> GetArticles()
    {
        var articles = await _articlesService.GetArticles();

        return Ok(articles);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Role.Author))]
    public async Task<ActionResult<ArticleResponseDto>> CreateArticle(ArticleDto articleDto)
    {
        var authorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var article = await _articlesService.CreateArticle(articleDto, authorId!);

        return Ok(article);
    }
}