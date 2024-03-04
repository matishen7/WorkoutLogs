using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseLogs.Queries
{
    public class ExerciseLogDto
    {
        public int SessionId { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public int DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }

        public string AdditionalNotes { get; set; }
    }
}
