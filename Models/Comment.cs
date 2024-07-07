using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Comment
  {
    public int CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int PostId { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
  }
}