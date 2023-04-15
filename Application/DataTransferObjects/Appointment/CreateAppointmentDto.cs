using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.Appointment;

public class CreateAppointmentDto
{
    [Required] public DateTime StartDate { get; set; }

    [Required] public DateTime EndDate { get; set; }

    [Required] [MaxLength(200)] public string Reason { get; set; }

    [Required] public string UserId { get; set; }
}