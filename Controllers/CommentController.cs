using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.comments;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/comment")]
  [ApiController]
  public class CommentController: ControllerBase
  {
    private readonly ICommentrepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly UserManager<User> _userManager;
    public CommentController(
      ICommentrepository commentrepository, 
      IPostRepository postRepository,
      UserManager<User> userManager)
    {
      _commentRepository = commentrepository;
      _postRepository = postRepository;
      _userManager = userManager;
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetComments()
    {
      var comments = await _commentRepository.GetCommentsAsync();
      var commentsDTO = comments.Select(comment => comment.ToCommentDTO());

      return Ok(commentsDTO);
    }

    [HttpGet("{commentId}")]
    [Authorize]
    public async Task<IActionResult> GetCommentById([FromRoute] int commentId)
    {
      var comment = await _commentRepository.GetCommentByIdAsync(commentId);

      if (comment == null)
      {
        return NotFound();
      }

      return Ok(comment.ToCommentDTO());
    }

    [HttpPost("{postId}")]
    [Authorize]
    public async Task<IActionResult> CreateComment([FromRoute] int postId, [FromBody] CommentRequestDTO commentRequestDTO)
    {
      if (!await _postRepository.PostExist(postId))
      {
        return NotFound();
      }

      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      var comment = commentRequestDTO.ToCommentFromCommentDTO();
      comment.PostId = postId;
      comment.UserId = user!.Id;

      await _commentRepository.CreateCommentAsync(comment);

      return CreatedAtAction(nameof(GetCommentById), new { commentId = comment.CommentId }, comment.ToCommentDTO());
    }

    [HttpPut("{commentId}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] CommentRequestDTO commentRequestDTO)
    {
      var comment = await _commentRepository.GetCommentByIdAsync(commentId);

      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      if (comment == null)
      {
        return NotFound();
      }

      if (user!.Id != comment.UserId)
      {
        return Unauthorized();
      }

      var editedComment = await _commentRepository.UpdateCommentAsync(commentId, commentRequestDTO);

      return Ok(editedComment!.ToCommentDTO());
    }

    [HttpDelete("{commentId}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
      var comment = await _commentRepository.GetCommentByIdAsync(commentId);

      var username = User.GetUsername();
      var user = await _userManager.FindByNameAsync(username);

      if (comment == null)
      {
        return NotFound();
      }

      if (user!.Id != comment.UserId)
      {
        return Unauthorized();
      }

      await _commentRepository.DeleteCommentAsync(commentId);

      return NoContent();
    }
  }
}