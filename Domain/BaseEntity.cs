namespace Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreateDate { get; set; }
    public string ModifyBy { get; set; }
    public DateTime? ModifyDate { get; set; }
}