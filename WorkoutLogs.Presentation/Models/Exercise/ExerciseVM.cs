using System.ComponentModel.DataAnnotations;

namespace WorkoutLogs.Presentation.Models.Exercise
{
    public class ExerciseVM
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string? TutorialUrl { get; set; }
    }
}
