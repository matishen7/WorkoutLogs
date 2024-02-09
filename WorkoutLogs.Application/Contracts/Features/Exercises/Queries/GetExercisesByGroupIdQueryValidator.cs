using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Queries
{
    public class GetExercisesByGroupIdQueryValidator : AbstractValidator<GetExercisesByGroupIdQuery>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;

        public GetExercisesByGroupIdQueryValidator(IExerciseGroupRepository exerciseGroupRepository)
        {
            _exerciseGroupRepository = exerciseGroupRepository;

            RuleFor(x => x.ExerciseGroupId)
                .GreaterThan(0)
                .WithMessage("ExerciseGroupId must be greater than 0")
                .MustAsync(ExerciseGroupExists)
                .WithMessage("ExerciseGroupId does not exist");
        }

        private async Task<bool> ExerciseGroupExists(int exerciseGroupId, CancellationToken cancellationToken)
        {
            return await _exerciseGroupRepository.ExistsAsync(exerciseGroupId);
        }
    }
}
