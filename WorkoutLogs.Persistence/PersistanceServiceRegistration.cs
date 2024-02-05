using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutLogs.Persistence.DbContexts;

namespace WorkoutLogs.Persistence
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkoutLogsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WorkoutLogsDatabaseConnectionString"));
            });

            return services;
        }
    }
}