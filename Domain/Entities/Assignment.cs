namespace Domain.Entities;

public class Assignment : BaseEntity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}