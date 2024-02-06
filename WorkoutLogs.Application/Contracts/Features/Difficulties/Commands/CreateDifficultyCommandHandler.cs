using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Commands
{
    public class CreateDifficultyCommandHandler : IRequestHandler<CreateDifficultyCommand, int>
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public CreateDifficultyCommandHandler(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        public async Task<int> Handle(CreateDifficultyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDifficultyCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var difficulty = new Difficulty
            {
                Level = request.Level
            };

            await _difficultyRepository.CreateAsync(difficulty);

            return difficulty.Id;
        }
    }

}
