using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class QuizRepository : IQuizRepository
  {
    private readonly ApplicationDBContext _dbContext;
    public QuizRepository(ApplicationDBContext dbContext)
    {
      _dbContext = dbContext;
    }
    public async Task<Quiz> CreateQuizAsync(Quiz quiz)
    {
      await _dbContext.AddAsync(quiz);
      await _dbContext.SaveChangesAsync();

      return quiz;
    }

    public Task<Quiz?> DeleteQuizAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<Quiz?> GetQuizByIdAsync(int id)
    {
      return await _dbContext.Quizzes
        .Include(quiz => quiz.User)
        .FirstOrDefaultAsync(quiz => quiz.QuizId == id);
    }

    public Task<List<Quiz>> GetQuizzesAsync()
    {
      throw new NotImplementedException();
    }

    public Task<Quiz?> UpdateQuizAsync()
    {
      throw new NotImplementedException();
    }
  }
}