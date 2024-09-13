using Microsoft.Extensions.DependencyInjection;
using Profile.Domain.Pets;
using Profile.Domain.Pets.PetTypes;
using Profile.Domain.Profiles;
using Profile.Infrastructure.Repositories;

namespace Profile.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IPetTypeRepository, PetTypeRepository>();
        return services;
    }
}