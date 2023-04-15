using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.Maintenance;

public class CreateMaintenanceDto
{
    [Required] [MaxLength(250)] public string Name { get; set; }
    public string Description { get; set; }

    [Required] public int Interval { get; set; }
}