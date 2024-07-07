using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.comments;
using api.Models;

namespace api.Interfaces
{
  public interface ICommentrepository
  {
    Task<List<Comment>> GetCommentsAsync();
    Task<Comment?> GetCommentByIdAsync(int commentId);
    Task<Comment> CreateCommentAsync(Comment comment);
    Task<Comment?> UpdateCommentAsync(int commentId, CommentRequestDTO commentRequestDTO);
    Task<Comment?> DeleteCommentAsync(int commentId);
  }
}