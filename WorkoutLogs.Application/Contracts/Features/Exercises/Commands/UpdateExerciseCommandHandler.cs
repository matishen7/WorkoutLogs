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

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Commands
{
    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, Unit>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IMapper _mapper;

        public UpdateExerciseCommandHandler(IExerciseRepository exerciseRepository, IExerciseGroupRepository exerciseGroupRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGroupRepository = exerciseGroupRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExerciseCommandValidator(_exerciseGroupRepository, _exerciseRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingExercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

            if (existingExercise == null)
            {
                throw new NotFoundException(nameof(Exercise), request.ExerciseId);
            }

            existingExercise.TutorialUrl = request.TutorialUrl;

            await _exerciseRepository.UpdateAsync(existingExercise);

            return Unit.Value;
        }
    }

}
