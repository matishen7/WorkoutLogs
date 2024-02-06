using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkoutLogs.Application.Persistence;
using WorkoutLogs.Persistence.DbContexts;
using WorkoutLogs.Persistence.Repositories;

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
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IExerciseTypeRepository, ExerciseTypeRepository>();
            services.AddScoped<IExerciseGroupRepository, ExerciseGroupRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IDifficultyRepository, DifficultyRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IExerciseLogRepository, ExerciseLogRepository>();
            services.AddScoped<IDbSeeder, DbSeeder>();

            return services;
        }

    }
}