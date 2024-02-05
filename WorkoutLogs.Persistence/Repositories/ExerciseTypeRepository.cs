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

        public async Task<bool> ExerciseTypeExists(int exerciseTypeId, CancellationToken cancellationToken)
        {
            var exerciseType = _context.ExerciseTypes
            .FirstOrDefault(x => x.Id == exerciseTypeId);
            return exerciseType != null;

        }
    }
}
