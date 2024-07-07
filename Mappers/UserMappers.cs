using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.auth;
using api.Models;

namespace api.Mappers
{
  public static class UserMappers
  {
    public static UserWithoutTokenDTO ToUserWithoutTokenDTO(this User user)
    {
      return new UserWithoutTokenDTO
      {
        Id = user.Id,
        UserName = user.UserName!,
        Email = user.Email!
      };
    }
  }
}