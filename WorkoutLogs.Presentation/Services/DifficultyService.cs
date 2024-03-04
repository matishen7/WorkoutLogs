using AutoMapper;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class DifficultyService : BaseHttpService, IDifficultyService
    {
        private readonly IMapper mapper;

        public DifficultyService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<ICollection<DifficultyDto>> GetAllDifficulties()
        {
            return await _client.DifficultyAllAsync();
        }
    }
}
