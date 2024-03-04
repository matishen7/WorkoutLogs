using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface IDifficultyService
    {
        public Task<ICollection<DifficultyDto>> GetAllDifficulties();
    }
}
