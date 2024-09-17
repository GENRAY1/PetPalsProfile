using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Profile.Domain.Abstractions;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;
using Profile.Domain.Profiles;
using Profile.Infrastructure.Authentication;
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
        AddAuthentication(services, configuration);
        services.AddAuthorization();
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention()
                .EnableSensitiveDataLogging() // Включает логирование чувствительных данных
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information) // Логирование команд базы данных в консоль
                .EnableDetailedErrors()); // Включает подробные ошибки

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IPetTypeRepository, PetTypeRepository>();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptionsConfiguration = configuration.GetSection("JwtOptions");

        services.Configure<JwtOptions>(jwtOptionsConfiguration);

        var jwtOptions = jwtOptionsConfiguration.Get<JwtOptions>();

        var rsa = RSA.Create();

        rsa.ImportRSAPublicKey(
            source: Convert.FromBase64String(jwtOptions.PublicKey),
            bytesRead: out int _);

        RsaSecurityKey publicKey = new RsaSecurityKey(rsa);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = publicKey,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });
    }
}