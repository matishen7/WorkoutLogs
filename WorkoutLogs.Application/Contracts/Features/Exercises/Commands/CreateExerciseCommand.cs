using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Commands
{
    public class CreateExerciseCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string TutorialUrl { get; set; }
        public int ExerciseGroupId { get; set; }
    }
}
