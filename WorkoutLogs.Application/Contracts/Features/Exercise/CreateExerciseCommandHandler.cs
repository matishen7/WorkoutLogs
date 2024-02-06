using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Exercise
{
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, int>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IExerciseGroupRepository _exerciseGroupRepository;

        public CreateExerciseCommandHandler(IExerciseRepository exerciseRepository, IExerciseGroupRepository exerciseGroupRepository)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGroupRepository = exerciseGroupRepository;
        }

        public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseCommandValidator(_exerciseGroupRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exercise = new Core.Exercise
            {
                Name = request.Name,
                TutorialUrl = request.TutorialUrl,
                ExerciseGroupId = request.ExerciseGroupId
            };

            await _exerciseRepository.CreateAsync(exercise);

            return exercise.Id;
        }
    }

}
