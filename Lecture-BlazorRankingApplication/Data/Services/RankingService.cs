using Lecture_BlazorRankingApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lecture_BlazorRankingApplication.Data.Services
{
    public class RankingService
    {
        ApplicationDbContext _context;

        public RankingService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Create
        public Task<GameResult> AddGameResult(GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            _context.SaveChanges();

            return Task.FromResult(gameResult);
        }

        // Read
        public Task<List<GameResult>> GetGameResultsAsync()
        {
            List<GameResult> results = _context.GameResults.ToList()
                                .OrderByDescending(item => item.Score)
                                .ToList();

            return Task.FromResult(results);
        }

        // Update
         public Task<bool> UpdateGameResult(GameResult gameResult)
        {
            var find = _context.GameResults
                        .Where(x => x.Id == gameResult.Id)
                        .FirstOrDefault();

            if (find == null)
            {
                return Task.FromResult(false);
            }

            find.UserName = gameResult.UserName;
            find.Score = gameResult.Score;
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        // Delete
        public Task<bool> DeleteGameResult(GameResult gameResult)
        {
            var find = _context.GameResults
                        .Where(x => x.Id == gameResult.Id)
                        .FirstOrDefault();

            if (find == null)
            {
                return Task.FromResult(false);
            }

            _context.GameResults.Remove(gameResult);
            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
