using WorkoutLogs.Presentation.Models.Exercise;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface IExerciseService
    {
        Task<ExerciseVM> GetByGroupIdAsync(int id);
    }
}
