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
    public class ExerciseLogRepository : GenericRepository<ExerciseLog>, IExerciseLogRepository
    {
        public ExerciseLogRepository(WorkoutLogsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ExerciseLog>> GetExerciseLogsBySessionId(int id)
        {
            var exerciseLogs = _context.ExerciseLogs.Where(x => x.SessionId == id);
            return exerciseLogs;
        }
    }
}
