namespace Domain.Entities;

public class Maintenance : BaseEntity
{
    public int Interval { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime NextExecution { get; set; }
    public DateTime? LastExecution { get; set; }
}