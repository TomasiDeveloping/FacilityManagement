using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public Department Department { get; set; }
    public Guid? DepartmentId { get; set; }
    public string Function { get; set; }
    public ICollection<Address> Address { get; set; }
    public ICollection<Assignment> Assignments { get; set; }
}