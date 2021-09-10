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

        public Task<List<GameResult>> GetGameResultsAsync()
        {
            List<GameResult> results = _context.GameResults.ToList()
                                .OrderByDescending(item => item.Score)
                                .ToList();

            return Task.FromResult(results);
        }
    }
}
