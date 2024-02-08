using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries
{
    public class GetAllExerciseGroupsByExerciseTypeIdQueryValidator : AbstractValidator<GetAllExerciseGroupsByExerciseTypeIdQuery>
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;

        public GetAllExerciseGroupsByExerciseTypeIdQueryValidator(IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseTypeRepository = exerciseTypeRepository;

            RuleFor(x => x.ExerciseTypeId)
                .GreaterThan(0).WithMessage("Exercise type ID must be greater than 0")
                .MustAsync(ExerciseTypeExists).WithMessage("Exercise type does not exist");
        }

        private async Task<bool> ExerciseTypeExists(int exerciseTypeId, CancellationToken cancellationToken)
        {
            return await _exerciseTypeRepository.ExerciseTypeExists(exerciseTypeId, cancellationToken);
        }
    }

}
