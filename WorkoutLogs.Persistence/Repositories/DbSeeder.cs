using Microsoft.Extensions.DependencyInjection;
using WorkoutLogs.Core;
using WorkoutLogs.Persistence.DbContexts;

namespace WorkoutLogs.Persistence.Repositories
{
    public class DbSeeder : IDbSeeder
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbSeeder(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void SeedData()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<WorkoutLogsDbContext>();


                if (!dbContext.Difficulties.Any())
                {
                    dbContext.Difficulties.AddRange(
                        new Difficulty { Level = "Easy - I could have done 3 more reps", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Difficulty { Level = "Medium - I could have done 2 more reps", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Difficulty { Level = "Hard - I could have done 1 more rep", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Difficulty { Level = "Max effort - I could not have done any more reps", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Difficulty { Level = "Failed - I tried to do another rep but couldn't", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                    );
                }

                if (!dbContext.Members.Any())
                {
                    dbContext.Members.AddRange(
                        new Member { Username = "admin@gmail.com", Role = "Admin", Password = "321", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Member { Username = "member1@gmail.com", Role = "Trainee", Password = "123", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                    );
                }

                if (!dbContext.ExerciseTypes.Any())
                {
                    dbContext.ExerciseTypes.AddRange(
                        new ExerciseType {  Name = "FullBody A", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseType {  Name = "FullBody B", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    );
                }

                if (!dbContext.ExerciseGroups.Any())
                {
                    dbContext.ExerciseGroups.AddRange(
                        new ExerciseGroup { Name = "Group 1", ExerciseTypeId = 1 ,DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 2", ExerciseTypeId = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 3", ExerciseTypeId = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 4", ExerciseTypeId = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 5", ExerciseTypeId = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 6", ExerciseTypeId = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 7", ExerciseTypeId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 8", ExerciseTypeId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 9", ExerciseTypeId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 10", ExerciseTypeId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 11", ExerciseTypeId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 12", ExerciseTypeId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        );
                }

                if (!dbContext.Exercises.Any())
                {
                    dbContext.Exercises.AddRange(
                        new Exercise { Name = "Barbell Bench Press", TutorialUrl = "" ,ExerciseGroupId = 1, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Smith Machine Squat", TutorialUrl = "" ,ExerciseGroupId = 2, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Seated Dumbbell Shoulder Press", TutorialUrl = "" ,ExerciseGroupId = 3, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Seated Cable Row (mid/upper back)", TutorialUrl = "" ,ExerciseGroupId = 4, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Leg Press Calf Raise", TutorialUrl = "" ,ExerciseGroupId = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "RKC Plank", TutorialUrl = "" ,ExerciseGroupId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Barbell Deadlift", TutorialUrl = "" ,ExerciseGroupId = 7, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Low Incline Smith Machine Press", TutorialUrl = "" ,ExerciseGroupId = 8, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Reverse Lunges*", TutorialUrl = "" ,ExerciseGroupId = 9, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Inverted Row", TutorialUrl = "" ,ExerciseGroupId = 10, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Pull-Up Negatives", TutorialUrl = "" ,ExerciseGroupId = 11, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Standing Face Pulls", TutorialUrl = "" ,ExerciseGroupId = 12, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                      );
                }

                dbContext.SaveChanges();
            }
        }
    }
}
