using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.auth;
using api.DTO.comments;
using api.Models;

namespace api.DTO.posts
{
  public class PostDTO
  {
    public int PostId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    public List<LikeDTO> Likes { get; set; } = new List<LikeDTO>();
    public string? UserId { get; set; }
    public UserWithoutTokenDTO? User { get; set; }
  }
}