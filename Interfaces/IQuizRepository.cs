using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
  public interface IQuizRepository
  {
    Task<List<Quiz>> GetQuizzesAsync();
    Task<Quiz?> GetQuizByIdAsync(int id);
    Task<Quiz> CreateQuizAsync(Quiz quiz);
    Task<Quiz?> UpdateQuizAsync();
    Task<Quiz?> DeleteQuizAsync();
  }
}