using Microsoft.Extensions.DependencyInjection;
using WorkoutLogs.Core;

namespace WorkoutLogs.Persistence
{

    public interface IDbSeeder
    {
        void SeedData();
    }
}
