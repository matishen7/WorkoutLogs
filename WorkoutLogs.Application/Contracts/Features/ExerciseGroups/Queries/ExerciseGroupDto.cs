using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WorkoutLogs.Core;

namespace WorkoutLogs.Application.Contracts.Features.ExerciseGroups.Queries
{
    public class ExerciseGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ExerciseTypeId { get; set; }

        public ExerciseType ExerciseType { get; set; }
    }
}