using Academia.Domain.Abstractions;
using Academia.Domain.Models.Entities;
using Microsoft.Extensions.Logging;

namespace Academia.Infrastructure.Persistence.Repos;


public class UserRepo : Repo<User>, IUserRepo
{
    private readonly AcademiaDbContext _db;
    private readonly ILogger<UserRepo> _logger;

    public UserRepo(
        AcademiaDbContext db,
        ILogger<UserRepo> logger) : base(db, logger)
    {
        _db = db;
        _logger = logger;
    }
}
