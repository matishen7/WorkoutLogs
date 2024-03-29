﻿using AutoMapper;
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


        public async Task<Response<int>> CreateSession(int memberId, CancellationToken cancellationToken)
        {
            try
            {
                var createSessionCommand = new CreateSessionCommand() { MemberId = 2 };
                var id = await _client.CreateSessionAsync(createSessionCommand);
                return new Response<int>()
                {
                    Data = id,
                    Success = true,
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<Guid>> EndSession(int id, CancellationToken cancellationToken)
        {
            try
            {
                var updateSessionCommand = new UpdateSessionCommand() { Id = id };
                await _client.EndSessionAsync(updateSessionCommand);
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
