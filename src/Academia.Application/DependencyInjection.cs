using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Academia.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR with all handlers from this assembly
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        // Add any other application-level services here
        // For example: validators, mappers, etc.

        return services;
    }
}
