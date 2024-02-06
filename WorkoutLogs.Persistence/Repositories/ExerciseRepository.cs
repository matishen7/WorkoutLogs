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
    public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(WorkoutLogsDbContext context) : base(context)
        {
        }

        public async Task<bool> ExerciseExists(int id, CancellationToken cancellationToken)
        {
            var exercise = _context.Exercises
           .FirstOrDefault(x => x.Id == id);
            return exercise != null;
        }
    }
}
