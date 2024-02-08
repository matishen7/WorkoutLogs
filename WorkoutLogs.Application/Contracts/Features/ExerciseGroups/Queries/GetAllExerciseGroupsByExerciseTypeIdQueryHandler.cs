using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries
{
    public class GetAllExerciseGroupsByExerciseTypeIdQueryHandler : IRequestHandler<GetAllExerciseGroupsByExerciseTypeIdQuery, IEnumerable<ExerciseGroupDto>>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IMapper _mapper;
        private readonly IExerciseTypeRepository _exerciseTypeRepository;

        public GetAllExerciseGroupsByExerciseTypeIdQueryHandler(IExerciseGroupRepository exerciseGroupRepository, IMapper mapper, IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseGroupRepository = exerciseGroupRepository;
            _mapper = mapper;
            _exerciseTypeRepository = exerciseTypeRepository;
        }

        public async Task<IEnumerable<ExerciseGroupDto>> Handle(GetAllExerciseGroupsByExerciseTypeIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllExerciseGroupsByExerciseTypeIdQueryValidator(_exerciseTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exerciseGroups = await _exerciseGroupRepository.GetAllByExerciseTypeIdAsync(request.ExerciseTypeId);
            return _mapper.Map<IEnumerable<ExerciseGroupDto>>(exerciseGroups);
        }
    }

}
