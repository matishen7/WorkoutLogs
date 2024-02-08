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
    public class UpdateExerciseGroupCommandHandler : IRequestHandler<UpdateExerciseGroupCommand, Unit>
    {
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        private readonly IMapper _mapper;

        public UpdateExerciseGroupCommandHandler(IExerciseGroupRepository exerciseGroupRepository, 
            IMapper mapper, 
            IExerciseTypeRepository exerciseTypeRepository)
        {
            _exerciseGroupRepository = exerciseGroupRepository;
            _mapper = mapper;
            _exerciseTypeRepository = exerciseTypeRepository;
        }

        public async Task<Unit> Handle(UpdateExerciseGroupCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateExerciseGroupCommandValidator(_exerciseTypeRepository, _exerciseGroupRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exerciseGroup = await _exerciseGroupRepository.GetByIdAsync(request.Id);
            if (exerciseGroup == null)
            {
                throw new NotFoundException(nameof(ExerciseGroup), request.Id);
            }

            _mapper.Map(request, exerciseGroup);

            await _exerciseGroupRepository.UpdateAsync(exerciseGroup);

            return Unit.Value;
        }
    }

}
