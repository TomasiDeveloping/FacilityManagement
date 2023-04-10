using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObjects.Assignment;

public class CreateAssignmentDto
{
    [Required] public string UserId { get; set; }

    [Required] [MaxLength(200)] public string Name { get; set; }

    public string Description { get; set; }
}