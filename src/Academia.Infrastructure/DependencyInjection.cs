using Academia.Domain.Abstractions;
using Academia.Infrastructure.Persistence;
using Academia.Infrastructure.Persistence.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Entity Framework Core DbContext
        services.AddDbContext<AcademiaDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()
            )
        );

        // Register Repositories
        services.AddScoped(typeof(IRepo<>), typeof(Repo<>));
        services.AddScoped<IUserRepo, UserRepo>();

        return services;
    }
}
