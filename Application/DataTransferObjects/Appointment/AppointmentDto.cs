namespace Application.DataTransferObjects.Appointment;

public class AppointmentDto
{
    public string Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public string UserId { get; set; }
    public string CreatedBy { get; set; }
    public string UserFullName { get; set; }
}