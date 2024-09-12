using Microsoft.OpenApi.Models;

namespace Profile.Api.Extentions;

public static class SwaggerServicesExtensions
{
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PetPalsProfile.Profile.Api", Version = "v0.1" });
        });
    }
}