using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using WorkoutLogs.Persistence.DbContexts;

namespace WorkoutLogs.Persistence.Repositories
{
    public class ExerciseGroupRepository : GenericRepository<ExerciseGroup>, IExerciseGroupRepository
    {
        public ExerciseGroupRepository(WorkoutLogsDbContext context) : base(context)
        {
        }

       
    }
}
