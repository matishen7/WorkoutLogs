using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Commands
{
    public class DeleteDifficultyCommandHandler : IRequestHandler<DeleteDifficultyCommand, Unit>
    {
        private readonly IDifficultyRepository _difficultyRepository;

        public DeleteDifficultyCommandHandler(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository ?? throw new ArgumentNullException(nameof(difficultyRepository));
        }

        public async Task<Unit> Handle(DeleteDifficultyCommand request, CancellationToken cancellationToken)
        {
            var difficulty = await _difficultyRepository.GetByIdAsync(request.Id);
            if (difficulty == null)
            {
                throw new NotFoundException("Difficulty", request.Id);
            }

            await _difficultyRepository.DeleteAsync(difficulty);
            return Unit.Value;
        }
    }

}
