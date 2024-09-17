namespace Gateway.Web.Extensions;

public static class ReverseProxyService
{
    public static void AddReverseProxyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy()
            .LoadFromConfig(configuration.GetSection("ReverseProxy"));
    }
}