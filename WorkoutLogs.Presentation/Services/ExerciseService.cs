using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class ExerciseService : BaseHttpService, IExerciseService
    {
        public ExerciseService(IClient client) : base(client)
        {
        }
    }
}
