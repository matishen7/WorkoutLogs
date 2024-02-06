using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Difficulty.Commands
{
    public class CreateDifficultyCommandValidator : AbstractValidator<CreateDifficultyCommand>
    {
        public CreateDifficultyCommandValidator()
        {
            RuleFor(x => x.Level).NotEmpty().MaximumLength(100);
        }
    }

}
