using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands
{
    public class UpdateExerciseGroupCommandValidator : AbstractValidator<UpdateExerciseGroupCommand>
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        private readonly IExerciseGroupRepository _exerciseGroupRepository;

        public UpdateExerciseGroupCommandValidator(IExerciseTypeRepository exerciseTypeRepository, IExerciseGroupRepository exerciseRepository)
        {
            _exerciseTypeRepository = exerciseTypeRepository;
            _exerciseGroupRepository = exerciseRepository;

            RuleFor(x => x.Id).NotEmpty().MustAsync(ExerciseGroupExists).WithMessage("Exercise does not exist."); 
            RuleFor(x => x.Name).NotEmpty().MaximumLength(500); 
            RuleFor(x => x.ExerciseTypeId).NotEmpty().MustAsync(ExerciseTypeExists).WithMessage("Exercise type does not exist.");
        }
        private async Task<bool> ExerciseTypeExists(int exerciseTypeId, CancellationToken cancellationToken)
        {
            return await _exerciseTypeRepository.ExerciseTypeExists(exerciseTypeId, cancellationToken);
        }
        private async Task<bool> ExerciseGroupExists(int exerciseGroupId, CancellationToken cancellationToken)
        {
            return await _exerciseGroupRepository.ExistsAsync(exerciseGroupId);
        }
    }

}
