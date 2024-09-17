using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetPalsProfile.Application.Abstractions.Authentication;
using PetPalsProfile.Application.Abstractions.PasswordManager;
using PetPalsProfile.Domain.Absractions;
using PetPalsProfile.Domain.Accounts;
using PetPalsProfile.Infrastructure.Authentication;
using PetPalsProfile.Infrastructure.Database;
using PetPalsProfile.Infrastructure.Repositories;

namespace PetPalsProfile.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistence(services, configuration);
        AddAuthentication(services, configuration);
        AddAuthorization(services, configuration);
        
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
    
    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptionsConfiguration = configuration.GetSection("JwtOptions");
            
        services.Configure<JwtOptions>(jwtOptionsConfiguration);

        var jwtOptions = jwtOptionsConfiguration.Get<JwtOptions>();
        
        var rsa = RSA.Create();
        
        rsa.ImportRSAPrivateKey(
            source: Convert.FromBase64String(jwtOptions.AccessTokenSettings.PrivateKey),
            bytesRead: out int _);
        
        RsaSecurityKey privateKey = new RsaSecurityKey(rsa);
        services.AddSingleton(privateKey);
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                    ValidIssuer = jwtOptions.AccessTokenSettings.Issuer,
                    ValidAudience = jwtOptions.AccessTokenSettings.Audience,
                    IssuerSigningKey = privateKey,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });
        
        services.AddSingleton<IPasswordManager, PasswordManager.PasswordManager>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
    }
    
    private static void AddAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
    }

}