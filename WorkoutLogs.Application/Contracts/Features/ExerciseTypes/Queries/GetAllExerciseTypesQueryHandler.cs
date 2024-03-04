using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Queries
{
    public class GetAllExerciseTypesQueryHandler : IRequestHandler<GetAllExerciseTypesQuery, List<ExerciseTypeDto>>
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        private readonly IMapper _mapper;

        public GetAllExerciseTypesQueryHandler(IExerciseTypeRepository exerciseTypeRepository, IMapper mapper)
        {
            _exerciseTypeRepository = exerciseTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<ExerciseTypeDto>> Handle(GetAllExerciseTypesQuery request, CancellationToken cancellationToken)
        {
            var exerciseTypes = await _exerciseTypeRepository.GetAsync();
            return _mapper.Map<List<ExerciseTypeDto>>(exerciseTypes);
        }
    }
}
