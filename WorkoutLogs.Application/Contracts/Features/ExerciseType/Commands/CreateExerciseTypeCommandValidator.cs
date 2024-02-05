using FluentValidation;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseType.Commands
{
    public class CreateExerciseTypeCommandValidator : AbstractValidator<CreateExerciseTypeCommand>
    {

        public CreateExerciseTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(500);
        }
    }
}