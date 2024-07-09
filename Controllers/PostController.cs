using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.posts;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/post")]
  [ApiController]
  public class PostController: ControllerBase
  {
    private readonly IPostRepository _postRepository;
    private readonly UserManager<User> _userManager;
    public PostController(
      IPostRepository postRepository,
      UserManager<User> userManager
    )
    {
      _postRepository = postRepository;
      _userManager = userManager;
    }

    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetPosts()
    {
      var posts = await _postRepository.GetPostsAsync();
      var postsDTO = posts.Select(post => post.ToPostDTO()).ToList();

      return Ok(postsDTO);
    }

    [HttpGet("{postId}")]
    // [Authorize]
    public async Task<IActionResult> GetPostById([FromRoute] int postId)
    {
      var post = await _postRepository.GetPostByIdAsync(postId);

      if (post == null)
      {
        return NotFound();
      }

      return Ok(post.ToPostDTO());
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] PostRequestDTO postRequestDTO)
    {
      var post = postRequestDTO.FromPostRequestToPost();
      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      post.UserId = user!.Id;

      await _postRepository.CreatePostAsync(post);

      return CreatedAtAction(nameof(GetPostById), new {postId = post.PostId}, post.ToPostDTO());
    }

    [HttpPut("{postId}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] PostRequestDTO postRequestDTO)
    {
      var post = await _postRepository.GetPostByIdAsync(postId);

      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      if (user!.Id != post!.UserId)
      {
        return Unauthorized();
      }

      if (post == null)
      {
        return NotFound();
      }

      var editedPost = await _postRepository.UpdatePostAsync(postId, postRequestDTO);

      return Ok(editedPost!.ToPostDTO());
    }

    [HttpDelete("{postId}")]
    [Authorize]
    public async Task<IActionResult> DeletePost([FromRoute] int postId)
    {
      var post = await _postRepository.GetPostByIdAsync(postId);

      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      if (post == null)
      {
        return NotFound();
      }

      if (user!.Id != post.UserId)
      {
        return Unauthorized();
      }

      await _postRepository.DeletePostAsync(postId);      

      return NoContent();
    }

    [HttpPost]
    [Route("like/{postId}")]
    [Authorize]
    public async Task<IActionResult> LikePost([FromRoute] int postId)
    {
      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      var like = await _postRepository.PostLiked(user!.Id, postId);

      if (like == null)
      {
        await _postRepository.LikePost(
          new Like
          {
            PostId = postId,
            UserId = user.Id
          }
        );

        return Ok("Post liked");
      }

      await _postRepository.UnlikePost(like);

      return Ok("Post unliked");
    }
  }
}