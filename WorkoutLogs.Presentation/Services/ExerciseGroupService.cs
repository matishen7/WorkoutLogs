using AutoMapper;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class ExerciseGroupService : BaseHttpService, IExerciseGroupService
    {
        private readonly IMapper mapper;

        public ExerciseGroupService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<ICollection<ExerciseGroupDto>> GetExerciseGroupsAsync(int exerciseTypeId)
        {
            return await _client.ByExerciseTypeAsync(exerciseTypeId);
        }
    }
}
