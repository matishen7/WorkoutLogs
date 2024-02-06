using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands
{
    public class CreateExerciseGroupCommandValidator : AbstractValidator<CreateExerciseGroupCommand>
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;

        public CreateExerciseGroupCommandValidator(IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseTypeRepository = exerciseTypeRepository;

            RuleFor(x => x.Name).NotEmpty().MaximumLength(500);
            RuleFor(x => x.ExerciseTypeId).NotEmpty().MustAsync(ExerciseTypeExists).WithMessage("Exercise type does not exist.");
        }

        private async Task<bool> ExerciseTypeExists(int exerciseTypeId, CancellationToken cancellationToken)
        {
            return await _exerciseTypeRepository.ExerciseTypeExists(exerciseTypeId, cancellationToken);
        }
    }


}
