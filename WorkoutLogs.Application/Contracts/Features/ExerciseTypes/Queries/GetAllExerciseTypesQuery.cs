using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseTypes.Queries
{
    public class GetAllExerciseTypesQuery : IRequest<List<ExerciseTypeDto>>
    {
    }
}
