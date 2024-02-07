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

        public CreateExerciseLogCommandHandler(
            IExerciseLogRepository exerciseLogRepository,
            IExerciseRepository exerciseRepository,
            IMemberRepository memberRepository,
            IDifficultyRepository difficultyRepository,
            IMapper mapper)
        {
            _exerciseLogRepository = exerciseLogRepository;
            _memberRepository = memberRepository;
            _difficultyRepository = difficultyRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;

        }

        public async Task<int> Handle(CreateExerciseLogCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseLogCommandValidator(_memberRepository, _difficultyRepository, _exerciseRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exerciseLog = _mapper.Map<ExerciseLog>(request);

            await _exerciseLogRepository.CreateAsync(exerciseLog);
            return exerciseLog.Id;
        }
    }

}
