using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.quizzes;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/quiz")]
  [ApiController]
  public class QuizController: ControllerBase
  {
    private readonly IQuizRepository _quizRepository;
    private readonly UserManager<User> _userManager;
    public QuizController(
      IQuizRepository quizRepository,
      UserManager<User> userManager
    ) {
      _quizRepository = quizRepository;
      _userManager = userManager;
    }

    [HttpGet("{quizId}")]
    [Authorize]
    public async Task<IActionResult> GetQuizById([FromRoute] int quizId)
    {
      var quiz = await _quizRepository.GetQuizByIdAsync(quizId);

      if (quiz == null)
      {
        return NotFound();
      }

      return Ok(quiz.FromQuizToQuizDTO());
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateQuiz([FromBody] QuizRequestDTO quizRequestDTO)
    // public async Task<IActionResult> CreateQuiz([FromBody] QuizRequestDTO quizRequestDTO)
    {
      // var quiz = quizRequestDTO.FromQuizRequestToQuiz();
      // var username = User.GetUsername();
      // var user = await _userManager.FindByNameAsync(username);

      // quiz.UserId = user?.Id;
      // quiz.UpdatedOn = quiz.CreatedOn;

      // await _quizRepository.CreateQuizAsync(quiz);

      // return CreatedAtAction(nameof(GetQuizById), new { quizId = quiz.QuizId }, quiz.FromQuizToQuizDTO());
      return Ok(quizRequestDTO);
    } 
  }
}