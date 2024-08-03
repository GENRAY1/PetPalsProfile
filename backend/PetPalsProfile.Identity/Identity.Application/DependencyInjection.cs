using Microsoft.Extensions.DependencyInjection;
using PetPalsProfile.Infrastructure.Authentication;
using PetPalsProfile.Infrastructure.PasswordManager;

namespace PetPalsProfile.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        services.AddSingleton<IPasswordManager, PasswordManager>();
        services.AddSingleton<IJwtProvider, JwtProvider>();

        return services;
    }
}