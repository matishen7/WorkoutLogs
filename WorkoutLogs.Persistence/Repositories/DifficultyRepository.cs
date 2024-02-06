using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using WorkoutLogs.Persistence.DbContexts;

namespace WorkoutLogs.Persistence.Repositories
{
    public class DifficultyRepository : GenericRepository<Difficulty>, IDifficultyRepository
    {
        public DifficultyRepository(WorkoutLogsDbContext context) : base(context)
        {
        }

        public async Task<bool> DifficultyExists(int id, CancellationToken cancellationToken)
        {
            var difficulty = _context.Difficulties
           .FirstOrDefault(x => x.Id == id);
            return difficulty != null;
        }
    }
}
