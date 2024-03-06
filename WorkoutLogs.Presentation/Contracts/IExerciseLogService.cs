using WorkoutLogs.Core;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface IExerciseLogService
    {
        public Task<ICollection<ExerciseLogDto>> GetExerciseLogsBySessionId(int id);
    }
}
