namespace Domain.Entities;

public class Maintenance : BaseEntity
{
    public int Interval { get; set; }
    public string Name { get; set; }
    public DateTime? LastExecution { get; set; }
}