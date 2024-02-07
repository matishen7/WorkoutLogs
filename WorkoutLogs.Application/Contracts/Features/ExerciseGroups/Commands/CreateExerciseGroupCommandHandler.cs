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

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Commands
{
    public class CreateExerciseGroupCommandHandler : IRequestHandler<CreateExerciseGroupCommand, int>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        private readonly IMapper _mapper;

        public CreateExerciseGroupCommandHandler(
            IExerciseGroupRepository exerciseGroupRepository,
            IExerciseTypeRepository exerciseTypeRepository,
            IMapper mapper)
        {
            _exerciseGroupRepository = exerciseGroupRepository;
            _exerciseTypeRepository = exerciseTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseGroupCommandValidator(_exerciseTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exerciseGroup = _mapper.Map<ExerciseGroup>(request);

            await _exerciseGroupRepository.CreateAsync(exerciseGroup);

            return exerciseGroup.Id;
        }
    }


}
