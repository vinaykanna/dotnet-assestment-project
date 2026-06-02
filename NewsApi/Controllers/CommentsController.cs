using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApi.DTOs;
using NewsApi.Services.Interfaces;

namespace NewsApi.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentsController(ICommentsService commentsService) : ControllerBase
{
    [HttpGet("article/{articleId}")]
    [Authorize]
    public async Task<IActionResult> GetComments(Guid articleId)
    {
        var comments = await commentsService.GetComments(articleId);
        return Ok(comments);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComment(CommentDto commentDto)
    {
        var userId = User.GetUserId();
        var comment = await commentsService.CreateComment(commentDto, userId);
        return Ok(comment);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var userId = User.GetUserId();
        await commentsService.DeleteComment(id, userId);
        return NoContent();
    }
}