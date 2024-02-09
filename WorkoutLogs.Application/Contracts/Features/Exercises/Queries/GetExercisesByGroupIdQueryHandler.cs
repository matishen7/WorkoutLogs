using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Queries
{
    public class GetExercisesByGroupIdQueryHandler : IRequestHandler<GetExercisesByGroupIdQuery, List<ExerciseDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IMapper _mapper;

        public GetExercisesByGroupIdQueryHandler(IExerciseRepository exerciseRepository, IExerciseGroupRepository exerciseGroupRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGroupRepository = exerciseGroupRepository;
            _mapper = mapper;
        }

        public async Task<List<ExerciseDto>> Handle(GetExercisesByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetExercisesByGroupIdQueryValidator(_exerciseGroupRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exercises = await _exerciseRepository.GetByGroupIdAsync(request.ExerciseGroupId);

            var exerciseDtos = _mapper.Map<List<ExerciseDto>>(exercises);
            return exerciseDtos;
        }
    }
}
