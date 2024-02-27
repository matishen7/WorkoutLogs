using AutoMapper;
using WorkoutLogs.Application.Contracts.Features.Exercises.Queries;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Models.Exercise;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class ExerciseService : BaseHttpService, IExerciseService
    {
        private readonly IMapper mapper;

        public ExerciseService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<ICollection<ExerciseVM>> GetByGroupIdAsync(int id)
        {
            var exercises = await _client.ByGroupIdAsync(id);
            return mapper.Map<List<ExerciseVM>>(exercises);
        }
        public async Task<Response<Guid>> CreateLeaveType(ExerciseVM leaveType)
        {
            try
            {
                var createLeaveTypeCommand = mapper.Map<CreateExerciseCommand>(leaveType);
                await _client.Create2Async(createLeaveTypeCommand);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
