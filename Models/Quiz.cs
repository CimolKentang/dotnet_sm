using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Quiz
  {
    public int QuizId { get; set; }
    public string Questions { get; set; } = string.Empty;
    public string Answers { get; set; } = string.Empty;
    public string AnswerKeys { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int QuestionNumber { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime UpdatedOn { get; set; }
    public int UpdatedTimes { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
  }
}