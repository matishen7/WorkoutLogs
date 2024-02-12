using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;
using ValidationException = WorkoutLogs.Application.Middleware.ValidationException;

namespace WorkoutLogs.Application.Contracts.Features.Sessions.Commands
{
    public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, Unit>
    {
        private readonly ISessionRepository _sessionRepository;

        public UpdateSessionCommandHandler(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Unit> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSessionCommandValidator(_sessionRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var session = await _sessionRepository.GetByIdAsync(request.Id);

            if (session == null)
            {
                throw new NotFoundException(nameof(Session), request.Id);
            }

            session.Ended = true;

            await _sessionRepository.UpdateAsync(session);

            return Unit.Value;
        }
    }
}
