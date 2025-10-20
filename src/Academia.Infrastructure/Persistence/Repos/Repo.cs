using Academia.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Academia.Infrastructure.Persistence.Repos;

public class Repo<T> : IRepo<T> where T : class
{
    private readonly AcademiaDbContext _db;
    private readonly ILogger<Repo<T>> _logger;

    public Repo(AcademiaDbContext db, ILogger<Repo<T>> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _db.Set<T>().AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdAsync(object id, CancellationToken ct = default)
    {
        return await _db.Set<T>().FindAsync([id], ct);
    }

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default)
    {
        return await _db.Set<T>().ToListAsync(ct);
    }

    public void Update(T entity)
    {
        _db.Set<T>().Update(entity);
    }
}
