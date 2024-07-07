using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.comments;
using api.Models;

namespace api.Mappers
{
  public static class CommentMapper
  {
    public static CommentDTO ToCommentDTO(this Comment comment)
    {
      return new CommentDTO
      {
        CommentId = comment.CommentId,
        Content = comment.Content,
        CreatedOn = comment.CreatedOn,
        PostId = comment.PostId,
        UserId = comment.UserId,
        User = comment.User?.ToUserWithoutTokenDTO()
      };
    }

    public static Comment ToCommentFromCommentDTO(this CommentRequestDTO commentRequestDTO)
    {
      return new Comment
      {
        Content = commentRequestDTO.Content
      };
    }
  }
}