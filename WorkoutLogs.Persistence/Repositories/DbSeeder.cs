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
                        new ExerciseType {  Name = "FullBody B", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                    );
                }
                if (!dbContext.ExerciseGroups.Any())
                {
                    dbContext.ExerciseGroups.AddRange(
                        new ExerciseGroup { Name = "Group 1", ExerciseTypeId = 5 ,DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 2", ExerciseTypeId = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 3", ExerciseTypeId = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 4", ExerciseTypeId = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 5", ExerciseTypeId = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 6", ExerciseTypeId = 5, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 7", ExerciseTypeId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 8", ExerciseTypeId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 9", ExerciseTypeId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 10", ExerciseTypeId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 11", ExerciseTypeId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new ExerciseGroup { Name = "Group 12", ExerciseTypeId = 6, DateCreated = DateTime.Now, DateModified = DateTime.Now }
                        );
                }
                if (!dbContext.Exercises.Any())
                {
                    dbContext.Exercises.AddRange(
                        new Exercise { Name = "Barbell Bench Press", TutorialUrl = "" ,ExerciseGroupId = 17, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Smith Machine Squat", TutorialUrl = "" ,ExerciseGroupId = 18, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Seated Dumbbell Shoulder Press", TutorialUrl = "" ,ExerciseGroupId = 19, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Seated Cable Row (mid/upper back)", TutorialUrl = "" ,ExerciseGroupId = 20, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Leg Press Calf Raise", TutorialUrl = "" ,ExerciseGroupId = 21, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "RKC Plank", TutorialUrl = "" ,ExerciseGroupId = 22, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Barbell Deadlift", TutorialUrl = "" ,ExerciseGroupId = 23, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Low Incline Smith Machine Press", TutorialUrl = "" ,ExerciseGroupId = 24, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Reverse Lunges*", TutorialUrl = "" ,ExerciseGroupId = 25, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Inverted Row", TutorialUrl = "" ,ExerciseGroupId = 26, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Pull-Up Negatives", TutorialUrl = "" ,ExerciseGroupId = 27, DateCreated = DateTime.Now, DateModified = DateTime.Now },
                        new Exercise { Name = "Standing Face Pulls", TutorialUrl = "" ,ExerciseGroupId = 28, DateCreated = DateTime.Now, DateModified = DateTime.Now }
                      );
                }

                dbContext.SaveChanges();
            }
        }
    }
}
