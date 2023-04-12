namespace Application.DataTransferObjects.Maintenance;

public class MaintenanceDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Interval { get; set; }
    public DateTime? LastExecution { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string ModifyBy { get; set; }
}