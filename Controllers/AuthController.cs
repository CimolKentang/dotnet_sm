using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.auth;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthController: ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;
    public AuthController(
      UserManager<User> userManager,
      ITokenService tokenService,
      SignInManager<User> signInManager
    ) {
      _userManager = userManager;
      _tokenService = tokenService;
      _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
      try
      {
        var user = new User
          {
            UserName = registerDTO.UserName,
            Email = registerDTO.Email
          };

          var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);

          if (createdUser.Succeeded)
        {
          var roleResult = await _userManager.AddToRoleAsync(user, "User");

          if (roleResult.Succeeded)
          {
            return Ok(
              new UserDTO
              {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
              }
            );
          } else {
            return StatusCode(500, createdUser.Errors);
          }
        } else {
          return StatusCode(500, createdUser.Errors);
        }
      }
      catch (Exception e)
      {
        return StatusCode(500, e);
      }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == loginDTO.Email);

      if (user == null) return Unauthorized("Invalid Credential");

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

      if (!result.Succeeded) return Unauthorized("Invalid Credential");

      return Ok(
        new UserDTO
        {
          Id = user.Id,
          UserName = user.UserName!,
          Email = user.Email!,
          Token = _tokenService.CreateToken(user)
        }
      );
    }
  }
}