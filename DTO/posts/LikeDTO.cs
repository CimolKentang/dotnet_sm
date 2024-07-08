using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.auth;

namespace api.DTO.posts
{
  public class LikeDTO
  {
    public int LikeId { get; set; }
    public int PostId { get; set; }
    public string? UserId { get; set; }
    public UserWithoutTokenDTO? User { get; set; }
  }
}