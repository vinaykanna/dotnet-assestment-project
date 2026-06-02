using NewsApi.DTOs;
using NewsApi.Exceptions;
using NewsApi.Models;
using NewsApi.Repositories.Interfaces;
using NewsApi.Services.Interfaces;
namespace NewsApi.Services;

public class CommentsService(ICommentRepository commentRepository) : ICommentsService
{
    public async Task<List<CommentResponseDto>> GetComments(Guid articleId)
    {
        var comments = await commentRepository.GetByArticleIdAsync(articleId);

        return comments.Select(a => new CommentResponseDto
        {
            Id = a.Id,
            Content = a.Content,
            UserName = a.User!.Name,
            CreatedAt = a.CreatedAt,
        }).ToList();
    }

    public async Task<CommentResponseDto?> CreateComment(CommentDto commentDto, Guid currentUserId)
    {
        var newComment = new Comment
        {
            ArticleId = commentDto.ArticleId,
            UserId = currentUserId,
            Content = commentDto.Content,
            CreatedAt = DateTime.UtcNow
        };

        var comment = await commentRepository.CreateAsync(newComment);

        return new CommentResponseDto
        {
            Id = comment!.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
        };
    }

    public async Task<CommentResponseDto?> DeleteComment(Guid id, Guid currentUserId)
    {
        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            throw new NotFoundException("Comment", id.ToString());
        }

        if (comment?.UserId != currentUserId)
        {
            throw new ForbiddenException("You can only delete your own favourite articles.");
        }

        await commentRepository.DeleteAsync(comment);

        return new CommentResponseDto
        {
            Id = comment!.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
        };
    }
}