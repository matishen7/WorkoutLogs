using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.Exercises.Commands
{
    public class UpdateExerciseCommand : IRequest<Unit>
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string TutorialUrl { get; set; }
        public int ExerciseGroupId { get; set; }
    }

}
