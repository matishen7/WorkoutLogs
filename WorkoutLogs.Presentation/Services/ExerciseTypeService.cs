using AutoMapper;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class ExerciseTypeService : BaseHttpService, IExerciseTypeService
    {
        private readonly IMapper mapper;

        public ExerciseTypeService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<ICollection<ExerciseTypeDto>> GetAllExerciseTypes()
        {
            return await _client.GetAllExerciseTypesAsync(new GetAllExerciseTypesQuery());
        }
    }
}
