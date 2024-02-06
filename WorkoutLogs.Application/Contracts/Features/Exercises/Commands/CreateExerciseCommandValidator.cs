using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Commands
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;

        public CreateExerciseCommandValidator(IExerciseGroupRepository exerciseGroupRepository)
        {
            _exerciseGroupRepository = exerciseGroupRepository;

            RuleFor(x => x.Name).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.TutorialUrl).NotNull().NotEmpty();
            RuleFor(x => x.ExerciseGroupId).NotEmpty().MustAsync(ExerciseGroupExists).WithMessage("Exercise group does not exist.");
        }

        private async Task<bool> ExerciseGroupExists(int exerciseGroupId, CancellationToken cancellationToken)
        {
            return await _exerciseGroupRepository.ExistsAsync(exerciseGroupId);
        }
    }

}
