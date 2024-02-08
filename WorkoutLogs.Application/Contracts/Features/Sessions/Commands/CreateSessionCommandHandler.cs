using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.Sessions.Commands
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, int>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;

        public CreateSessionCommandHandler(ISessionRepository sessionRepository, IMapper mapper, IMemberRepository memberRepository)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        public async Task<int> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSessionCommandValidator(_memberRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var session = _mapper.Map<Session>(request);

            await _sessionRepository.CreateAsync(session);

            return session.Id;
        }
    }

}
