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
    public class ExerciseTypeRepository : GenericRepository<ExerciseType>, IExerciseTypeRepository
    {
        public ExerciseTypeRepository(WorkoutLogsDbContext context) : base(context)
        {
        }
    }
}
