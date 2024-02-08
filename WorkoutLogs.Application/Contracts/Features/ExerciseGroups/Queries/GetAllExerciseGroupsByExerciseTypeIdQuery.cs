using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries
{
    public class GetAllExerciseGroupsByExerciseTypeIdQuery : IRequest<IEnumerable<ExerciseGroupDto>>
    {
        public int ExerciseTypeId { get; set; }
    }

}
