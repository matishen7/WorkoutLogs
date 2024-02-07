using AutoMapper;
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
    public class UpdateDifficultyCommandHandler : IRequestHandler<UpdateDifficultyCommand, Unit>
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;

        public UpdateDifficultyCommandHandler(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDifficultyCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDifficultyCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingDifficulty = await _difficultyRepository.GetByIdAsync(request.Id);
            if (existingDifficulty == null)
            {
                throw new NotFoundException(nameof(Difficulty), request.Id);
            }

            var updatedDifficulty = _mapper.Map(request, existingDifficulty);
            await _difficultyRepository.UpdateAsync(updatedDifficulty);

            return Unit.Value;
        }
    }

}
