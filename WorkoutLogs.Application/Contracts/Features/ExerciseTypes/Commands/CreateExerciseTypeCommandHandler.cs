using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Commands
{
    public class CreateExerciseTypeCommandHandler : IRequestHandler<CreateExerciseTypeCommand, int>
    {
        private readonly IExerciseTypeRepository _exerciseTypeRepository;
        private readonly IMapper _mapper;

        public CreateExerciseTypeCommandHandler(
            IExerciseTypeRepository exerciseTypeRepository,
            IMapper mapper)
        {
            _exerciseTypeRepository = exerciseTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateExerciseTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseTypeCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var exerciseType = _mapper.Map<Core.ExerciseType>(request);
            await _exerciseTypeRepository.CreateAsync(exerciseType);

            return exerciseType.Id;

        }
    }
}
