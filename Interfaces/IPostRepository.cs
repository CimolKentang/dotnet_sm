using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.posts;
using api.Models;

namespace api.Interfaces
{
  public interface IPostRepository
  {
    Task<List<Post>> GetPostsAsync();
    Task<Post> CreatePostAsync(Post post);
    Task<Post?> GetPostByIdAsync(int postId);
    Task<Post?> UpdatePostAsync(int postId, PostRequestDTO postRequestDTO);
    Task<Post?> DeletePostAsync(int postId);
    Task<bool> PostExist(int postId);
    Task<Like?> PostLiked(string userId, int postId);
    Task<Like> LikePost(Like like);
    Task<Like> UnlikePost(Like like);
  }
}