using Microsoft.Extensions.DependencyInjection;
using PetPalsProfile.Application.Abstractions.Authentication;
using PetPalsProfile.Application.Abstractions.PasswordManager;

namespace PetPalsProfile.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        return services;
    }
}