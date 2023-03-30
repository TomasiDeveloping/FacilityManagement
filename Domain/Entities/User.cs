using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public ICollection<Address> Address { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
}