using Academia.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academia.Domain.Models.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
}
