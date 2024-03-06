using AutoMapper;
using WorkoutLogs.Core;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class ExerciseLogService : BaseHttpService, IExerciseLogService
    {
        private readonly IMapper mapper;

        public ExerciseLogService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<ICollection<ExerciseLogDto>> GetExerciseLogsBySessionId(int id)
        {
            var request = new GetExerciseLogsQuery() { SessionId = id };
            return await _client.GetBySessionIdAsync(request);
        }
    }
}
