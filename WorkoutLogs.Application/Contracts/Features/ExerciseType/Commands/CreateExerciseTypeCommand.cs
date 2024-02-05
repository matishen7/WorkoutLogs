using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseType.Commands
{
    public class CreateExerciseTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
