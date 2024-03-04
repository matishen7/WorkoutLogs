
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface IExerciseTypeService
    {
        public Task<ICollection<ExerciseTypeDto>> GetAllExerciseTypes();
    }
}
