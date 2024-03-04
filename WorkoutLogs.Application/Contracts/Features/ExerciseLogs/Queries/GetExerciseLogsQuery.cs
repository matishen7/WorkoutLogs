using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Queries
{
    public class GetExerciseLogsQuery : IRequest<List<ExerciseLogDto>>
    {
        public int SessionId { get; set; }
    }
}
