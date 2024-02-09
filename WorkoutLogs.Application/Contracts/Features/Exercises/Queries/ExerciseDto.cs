namespace WorkoutLogs.Application.Contracts.Features.Exercises.Queries
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? TutorialUrl { get; set; }
    }
}