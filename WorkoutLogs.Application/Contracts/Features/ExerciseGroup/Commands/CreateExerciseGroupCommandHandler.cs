using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroup.Commands
{
    public class CreateExerciseGroupCommandHandler : IRequestHandler<CreateExerciseGroupCommand, int>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IExerciseTypeRepository _exerciseTypeRepository;

        public CreateExerciseGroupCommandHandler(
            IExerciseGroupRepository exerciseGroupRepository,
            IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseGroupRepository = exerciseGroupRepository;
            _exerciseTypeRepository = exerciseTypeRepository;
        }

        public async Task<int> Handle(CreateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseGroupCommandValidator(_exerciseTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exerciseGroup = new Core.ExerciseGroup
            {
                Name = request.Name,
                ExerciseTypeId = request.ExerciseTypeId
            };

            await _exerciseGroupRepository.CreateAsync(exerciseGroup);

            return exerciseGroup.Id;
        }
    }


}
