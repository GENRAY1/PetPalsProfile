using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Profile.Domain.Abstractions;
using Profile.Infrastructure.Database;
using Profile.Infrastructure.Repositories;

namespace Profile.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistence(services, configuration);
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
                .EnableSensitiveDataLogging() // Включает логирование чувствительных данных
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information) // Логирование команд базы данных в консоль
                .EnableDetailedErrors()); // Включает подробные ошибки
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}