using Academia.Domain.Abstractions;
using Academia.Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Academia.Application.Behaviours.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepo _userRepo;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IUserRepo repo,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userRepo = repo ?? throw new ArgumentNullException(nameof(repo));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    async Task<Guid> IRequestHandler<CreateUserCommand, Guid>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new user with username: {Username}", request.Username);
        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Username = request.Username,
            PasswordHash = HashPassword(request.Password),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            IsActive = true
        };
        await _userRepo.AddAsync(newUser, cancellationToken);
        return newUser.UserId;
    }

    private static string HashPassword(string password)
    {
        // Implement a proper password hashing mechanism here
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
