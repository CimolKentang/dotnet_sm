using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Post
  {
    public int PostId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Like> Likes { get; set; } = new List<Like>();
    public string? UserId { get; set; }
    public User? User { get; set; }
  }
}