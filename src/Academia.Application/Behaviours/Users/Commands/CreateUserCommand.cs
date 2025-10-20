using Academia.Domain.Enums;
using MediatR;
using System.ComponentModel;
using System.Data;

namespace Academia.Application.Behaviours.Users.Commands;

public class CreateUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}
