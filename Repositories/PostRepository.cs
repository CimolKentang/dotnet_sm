using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.posts;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class PostRepository : IPostRepository
  {
    private readonly ApplicationDBContext _dbContext;
    public PostRepository(ApplicationDBContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
      await _dbContext.AddAsync(post);
      await _dbContext.SaveChangesAsync();

      return post;
    }

    public async Task<Post?> DeletePostAsync(int postId)
    {
      var post = await _dbContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId);

      if (post == null)
      {
        return null;
      }

      _dbContext.Posts.Remove(post);
      await _dbContext.SaveChangesAsync();

      return post;
    }

    public async Task<Post?> GetPostByIdAsync(int postId)
    {
      return await _dbContext.Posts
        .Include(post => post.User)
        .Include(post => post.Comments).ThenInclude(comment => comment.User)
        .FirstOrDefaultAsync(post => post.PostId == postId);
    }

    public async Task<List<Post>> GetPostsAsync()
    {
      return await _dbContext.Posts
        .Include(post => post.User)
        .Include(post => post.Comments)
        .ToListAsync();
    }

    public async Task<bool> PostExist(int postId)
    {
      return await _dbContext.Posts.AnyAsync(post => post.PostId == postId);
    }

    public async Task<Post?> UpdatePostAsync(int postId, PostRequestDTO postRequestDTO)
    {
      var post = await _dbContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId);

      if (post == null)
      {
        return null;
      }

      post.Content = postRequestDTO.Content;

      await _dbContext.SaveChangesAsync();

      return post;
    }
  }
}