using WorkoutLogs.Presentation.Models.Exercise;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Contracts
{
    public interface ISessionService
    {
        public Task<Response<Guid>> CreateSession(int memberId, CancellationToken cancellationToken);
        public Task<Response<Guid>> EndSession(int id, CancellationToken cancellationToken);
    }
}
