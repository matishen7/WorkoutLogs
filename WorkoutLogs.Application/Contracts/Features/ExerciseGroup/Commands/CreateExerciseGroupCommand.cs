using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroup.Commands
{
    public class CreateExerciseGroupCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int ExerciseTypeId { get; set; }
    }

}
