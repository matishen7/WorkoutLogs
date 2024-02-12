using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Sessions.Commands
{
    public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
    {
        private readonly ISessionRepository _sessionRepository;

        public UpdateSessionCommandValidator(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("SessionId must be greater than 0")
                .MustAsync(SessionExists).WithMessage("Session does not exist.");
        }

        private async Task<bool> SessionExists(int sessionId, CancellationToken cancellationToken)
        {
            return await _sessionRepository.Exists(sessionId);
        }
    }
}
