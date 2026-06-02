using NewsApi.DTOs;
using NewsApi.Models;

namespace NewsApi.Services.Interfaces;

public interface ICommentsService
{
    Task<List<CommentResponseDto>> GetComments(Guid articleId);
    Task<CommentResponseDto?> CreateComment(CommentDto commentDto, Guid currentUserId);
    Task<CommentResponseDto?> DeleteComment(Guid id, Guid currentUserId);
}