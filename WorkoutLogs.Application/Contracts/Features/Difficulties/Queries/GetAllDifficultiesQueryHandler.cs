using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Difficulties.Queries
{
    public class GetAllDifficultiesQueryHandler : IRequestHandler<GetAllDifficultiesQuery, IEnumerable<DifficultyDto>>
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;

        public GetAllDifficultiesQueryHandler(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DifficultyDto>> Handle(GetAllDifficultiesQuery request, CancellationToken cancellationToken)
        {
            var difficulties = await _difficultyRepository.GetAsync();
            return _mapper.Map<IEnumerable<DifficultyDto>>(difficulties);
        }
    }

}
