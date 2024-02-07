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

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Commands
{
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, int>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IExerciseGroupRepository _exerciseGroupRepository;
        private readonly IMapper _mapper;

        public CreateExerciseCommandHandler(IExerciseRepository exerciseRepository, IExerciseGroupRepository exerciseGroupRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGroupRepository = exerciseGroupRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateExerciseCommandValidator(_exerciseGroupRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var exercise = _mapper.Map<Exercise>(request);

            await _exerciseRepository.CreateAsync(exercise);

            return exercise.Id;
        }
    }

}
