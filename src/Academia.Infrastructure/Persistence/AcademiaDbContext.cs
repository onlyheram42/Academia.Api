using Microsoft.EntityFrameworkCore;
using Academia.Domain.Models.Entities;

namespace Academia.Infrastructure.Persistence;

public class AcademiaDbContext : DbContext
{
    DbSet<User> Users => Set<User>();
    public AcademiaDbContext(DbContextOptions<AcademiaDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademiaDbContext).Assembly);
    }
}
