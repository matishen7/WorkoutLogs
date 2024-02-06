using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Commands
{
    public class CreateExerciseLogCommand : IRequest<int>
    {
        public int MemberId { get; set; }
        public DateTime Date { get; set; }
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public int DifficultyId { get; set; }
        public string AdditionalNotes { get; set; }
    }

}
