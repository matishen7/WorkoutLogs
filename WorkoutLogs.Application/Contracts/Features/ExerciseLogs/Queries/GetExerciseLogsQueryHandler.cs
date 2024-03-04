using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Middleware;
using WorkoutLogs.Application.Persistence;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Queries
{
    public class GetExerciseLogsQueryHandler : IRequestHandler<GetExerciseLogsQuery, List<ExerciseLogDto>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IExerciseLogRepository _exerciseLogRepository;
        private readonly IMapper _mapper;
        public GetExerciseLogsQueryHandler(ISessionRepository sessionRepository, 
            IExerciseLogRepository exerciseLogRepository,
            IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _exerciseLogRepository = exerciseLogRepository;
            _mapper = mapper;
        }

        public async Task<List<ExerciseLogDto>> Handle(GetExerciseLogsQuery request, CancellationToken cancellationToken)
        {
            var sessionId = _sessionRepository.GetByIdAsync(request.SessionId);
            if (sessionId == null)
            {
                throw new NotFoundException(nameof(sessionId), request.SessionId);
            }

            var exerciseLogs = await _exerciseLogRepository.GetExerciseLogsBySessionId(request.SessionId);
            return _mapper.Map<List<ExerciseLogDto>>(exerciseLogs);
        }
    }
}
