using Academia.Application;
using Academia.Infrastructure;
using Academia.Infrastructure.Persistence;
using System.Reflection;

namespace Academia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add Application Layer (MediatR, Handlers, etc.)
            builder.Services.AddApplication();

            // Add Infrastructure Layer (EF Core, Repositories, etc.)
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AcademiaDbContext>();
                try
                {
                    var canConnect = dbContext.Database.CanConnect();
                    if (canConnect)
                    {
                        app.Logger.LogInformation("Successfully connected to database");
                    }
                    else
                    {
                        app.Logger.LogWarning("Cannot connect to database");
                    }
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Error testing database connection");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/api/health", () =>
            {
                return Results.Ok(new { status = "Healthy" });
            });

            app.MapControllers();

            app.Run();
        }
    }
}
