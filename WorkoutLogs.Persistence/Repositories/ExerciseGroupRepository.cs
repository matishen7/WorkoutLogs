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

        public async Task<bool> ExistsAsync(int exerciseGroupId)
        {
            var exerciseGroup = _context.ExerciseGroups
            .FirstOrDefault(x => x.Id == exerciseGroupId);
            return exerciseGroup != null;
        }

        public async Task<IEnumerable<ExerciseGroup>> GetAllByExerciseTypeIdAsync(int exerciseTypeId)
        {
            var exerciseGroups = _context.ExerciseGroups
            .Where(x => x.ExerciseTypeId == exerciseTypeId);
            return exerciseGroups;
        }
    }
}
