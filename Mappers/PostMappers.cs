using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.posts;
using api.Models;

namespace api.Mappers
{
  public static class PostMappers
  {
    public static Post FromPostRequestToPost(this PostRequestDTO postRequestDTO)
    {
      return new Post
      {
        Content = postRequestDTO.Content
      };
    }

    public static PostDTO ToPostDTO(this Post post)
    {
      return new PostDTO
      {
        PostId = post.PostId,
        Content = post.Content,
        CreatedOn = post.CreatedOn,
        Comments = post.Comments.Select(comment => comment.ToCommentDTO()).ToList(),
        UserId = post.UserId,
        User = post.User!.ToUserWithoutTokenDTO()
      };
    }
  }
}