namespace Domain.Entities;

public class Appointment : BaseEntity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
}