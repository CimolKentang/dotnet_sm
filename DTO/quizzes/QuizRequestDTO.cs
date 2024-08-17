using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.quizzes
{
  public class QuizRequestDTO
  {
    public string Questions { get; set; } = string.Empty;
    public string Answers { get; set; } = string.Empty;
    public string AnswerKeys { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int QuestionNumber { get; set; }
  }
}