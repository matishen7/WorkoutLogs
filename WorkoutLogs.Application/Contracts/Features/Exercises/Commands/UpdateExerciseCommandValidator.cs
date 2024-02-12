using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Commands
{
    public class UpdateExerciseCommandValidator : AbstractValidator<UpdateExerciseCommand>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public UpdateExerciseCommandValidator(IExerciseGroupRepository exerciseGroupRepository, IExerciseRepository exerciseRepository)
        {
            _exerciseGroupRepository = exerciseGroupRepository;
            _exerciseRepository = exerciseRepository;

            RuleFor(x => x.ExerciseId).GreaterThan(0).WithMessage("ExerciseId must be greater than 0");
            RuleFor(x => x.TutorialUrl).NotEmpty();
            RuleFor(x => x.ExerciseId).MustAsync(ExerciseExists).WithMessage("Exercise with this ID does not exist.");
        }

        private async Task<bool> ExerciseExists(int exerciseId, CancellationToken cancellationToken)
        {
            return await _exerciseRepository.Exists(exerciseId);
        }
    }

}
