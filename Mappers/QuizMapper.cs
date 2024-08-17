using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.quizzes;
using api.Models;

namespace api.Mappers
{
  public static class QuizMapper
  {
    public static Quiz FromQuizRequestToQuiz(this QuizRequestDTO quizRequestDTO)
    {
      return new Quiz
      {
        Questions = quizRequestDTO.Questions,
        Answers = quizRequestDTO.Answers,
        AnswerKeys = quizRequestDTO.AnswerKeys,
        Description = quizRequestDTO.Description,
        QuestionNumber = quizRequestDTO.QuestionNumber,
      };
    }

    public static QuizDTO FromQuizToQuizDTO(this Quiz quiz)
    {
      return new QuizDTO
      {
        QuizId = quiz.QuizId,
        Questions = string.Empty,
        Answers = string.Empty,
        AnswerKeys = string.Empty,
        Description = string.Empty,
        QuestionNumber = quiz.QuestionNumber,
        CreatedOn = quiz.CreatedOn,
        UpdatedOn = quiz.UpdatedOn,
        UpdatedTimes = quiz.UpdatedTimes,
        UserId = quiz.UserId,
        User = quiz.User!.ToUserWithoutTokenDTO()
      };
    }
  }
}