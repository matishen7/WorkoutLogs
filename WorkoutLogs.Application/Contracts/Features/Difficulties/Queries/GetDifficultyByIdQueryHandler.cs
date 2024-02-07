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

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Queries
{
    public class GetDifficultyByIdQueryHandler : IRequestHandler<GetDifficultyByIdQuery, DifficultyDto>
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;

        public GetDifficultyByIdQueryHandler(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
        }

        public async Task<DifficultyDto> Handle(GetDifficultyByIdQuery request, CancellationToken cancellationToken)
        {
            var difficulty = await _difficultyRepository.GetByIdAsync(request.Id);

            if (difficulty == null)
            {
                throw new NotFoundException(nameof(Difficulty), request.Id);
            }

            return _mapper.Map<DifficultyDto>(difficulty);
        }
    }

}
