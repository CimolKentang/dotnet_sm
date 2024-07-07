using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.auth;

namespace api.DTO.comments
{
  public class CommentDTO
  {
    public int CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int PostId { get; set; }
    public string? UserId { get; set; }
    public UserWithoutTokenDTO? User { get; set; }
  }
}