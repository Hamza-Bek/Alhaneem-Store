namespace WebAPI.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}