using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.comments;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class CommentRepository : ICommentrepository
  {
    private readonly ApplicationDBContext _dbContext;
    public CommentRepository(ApplicationDBContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
      await _dbContext.Comments.AddAsync(comment);
      await _dbContext.SaveChangesAsync();

      return comment;
    }

    public async Task<Comment?> DeleteCommentAsync(int commentId)
    {
      var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);

      if (comment == null)
      {
        return null;
      }

      _dbContext.Comments.Remove(comment);
      await _dbContext.SaveChangesAsync();

      return comment;
    }

    public async Task<Comment?> GetCommentByIdAsync(int commentId)
    {
      return await _dbContext.Comments.Include(comment => comment.User).FirstOrDefaultAsync(comment => comment.CommentId == commentId);
    }

    public async Task<List<Comment>> GetCommentsAsync()
    {
      return await _dbContext.Comments.Include(comment => comment.User).ToListAsync();
    }

    public async Task<Comment?> UpdateCommentAsync(int commentId, CommentRequestDTO commentRequestDTO)
    {
      var comment = await _dbContext.Comments.FirstOrDefaultAsync(comment => comment.CommentId == commentId);

      if (comment == null)
      {
        return null;
      }

      comment.Content = commentRequestDTO.Content;

      await _dbContext.SaveChangesAsync();

      return comment;
    }
  }
}