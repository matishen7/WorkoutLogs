using AutoMapper;
using WorkoutLogs.Presentation.Contracts;
using WorkoutLogs.Presentation.Models.Exercise;
using WorkoutLogs.Presentation.Services.Base;

namespace WorkoutLogs.Presentation.Services
{
    public class SessionService : BaseHttpService, ISessionService
    {

        public SessionService(IClient client) : base(client)
        {
        }


        public async Task<Response<Guid>> CreateSession(int memberId, CancellationToken cancellationToken)
        {
            try
            {
                var createSessionCommand = new CreateSessionCommand() { MemberId = 2 };
                await _client.Create6Async(createSessionCommand);
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

        public Task<Response<Guid>> EndSession(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
