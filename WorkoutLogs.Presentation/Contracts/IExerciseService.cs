using WorkoutLogs.Presentation.Models.Exercise;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface IExerciseService
    {
        Task<ICollection<ExerciseVM>> GetByGroupIdAsync(int id);
        Task<Response<Guid>> CreateLeaveType(ExerciseVM exerciseVM);
    }
}
