using Microsoft.Extensions.DependencyInjection;
using WorkoutLogs.Core;
using WorkoutLogs.Persistence.DbContexts;

namespace WorkoutLogs.Persistence.Repositories
{
    public class DifficultyDbSeeder : IDbSeeder
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DifficultyDbSeeder(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void SeedData()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<WorkoutLogsDbContext>();


                if (dbContext.Difficulties.Any())
                {
                    return;
                }

                dbContext.Difficulties.AddRange(
                    new Difficulty { Level = "Easy - I could have done 3 more reps" , DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Difficulty { Level = "Medium - I could have done 2 more reps", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Difficulty { Level = "Hard - I could have done 1 more rep", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Difficulty { Level = "Max effort - I could not have done any more reps", DateCreated = DateTime.Now, DateModified = DateTime.Now },
                    new Difficulty { Level = "Failed - I tried to do another rep but couldn't", DateCreated = DateTime.Now, DateModified = DateTime.Now }
                );

                dbContext.SaveChanges();
            }
        }
    }
}
