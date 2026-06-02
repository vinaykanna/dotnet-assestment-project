using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApi.DTOs;
using NewsApi.Enums;
using NewsApi.Models;
using NewsApi.Services.Interfaces;

namespace NewsApi.Controllers;

[ApiController]
[Route("api/articles")]
public class NewsController(IArticlesService articlesService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PagedResponse<ArticleResponseDto>>> GetArticles(
      [FromQuery] GetArticlesRequestDto request
      )
    {
        var result = await articlesService.GetArticles(
            request.PageNumber!.Value,
            request.PageSize!.Value);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = nameof(Role.Author))]
    public async Task<ActionResult<ArticleResponseDto>> CreateArticle(ArticleDto articleDto)
    {
        var authorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var article = await articlesService.CreateArticle(articleDto, authorId!);

        return Ok(article);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    Guid id,
    UpdateArticleDto updateArticleDto)
    {
        var userId = User.GetUserId();
        var article = await articlesService.UpdateArticle(
            id,
            updateArticleDto,
            userId);

        return Ok(article);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.GetUserId();
        await articlesService.DeleteArticle(id, userId);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id}/view")]
    public async Task<IActionResult> AddArticleView(Guid id)
    {
        var userId = User.GetUserId();
        await articlesService.AddArticleView(id, userId);
        return NoContent();
    }
}