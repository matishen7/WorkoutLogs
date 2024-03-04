using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface IExerciseGroupService
    {
        public Task<ICollection<ExerciseGroupDto>> GetExerciseGroupsAsync(int exerciseTypeId);
    }
}
