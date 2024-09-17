using Microsoft.Extensions.DependencyInjection;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;
using Profile.Domain.Profiles;

namespace Profile.Application;

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