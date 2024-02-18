using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Commands
{
    public class CreateExerciseLogCommandHandler : IRequestHandler<CreateExerciseLogCommand, int>
    {
        private readonly IExerciseLogRepository _exerciseLogRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        private readonly ISessionRepository _sessionRepository;

        public CreateExerciseLogCommandHandler(
            IExerciseLogRepository exerciseLogRepository,
            IExerciseRepository exerciseRepository,
            IMemberRepository memberRepository,
            IDifficultyRepository difficultyRepository,
            IMapper mapper,
            ISessionRepository sessionRepository)
        {
            _exerciseLogRepository = exerciseLogRepository;
            _memberRepository = memberRepository;
            _difficultyRepository = difficultyRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            _sessionRepository = sessionRepository; 

        }

        public async Task<int> Handle(CreateExerciseLogCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseLogCommandValidator(_memberRepository, 
                _difficultyRepository, 
                _exerciseRepository,
                _sessionRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var session = await _sessionRepository.GetByIdAsync(request.SessionId);
            
            if (session == null)
            throw new NotFoundException(nameof(session), request.SessionId);

            if (session.Ended == true)
                throw new Exception(nameof(session) + " Session is ended." );
            
            var exerciseLog = _mapper.Map<ExerciseLog>(request);

            await _exerciseLogRepository.CreateAsync(exerciseLog);
            return exerciseLog.Id;
        }
    }

}
