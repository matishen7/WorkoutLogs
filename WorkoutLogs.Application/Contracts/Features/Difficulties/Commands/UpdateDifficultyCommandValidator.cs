using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Commands
{
    public class UpdateDifficultyCommandValidator : AbstractValidator<UpdateDifficultyCommand>
    {
        public UpdateDifficultyCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Level).NotEmpty().MaximumLength(100);
        }
    }

}
