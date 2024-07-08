using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Like
  {
    public int LikeId { get; set; }
    public int PostId { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
  }
}