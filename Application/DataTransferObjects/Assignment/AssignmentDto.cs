namespace Application.DataTransferObjects.Assignment;

public class AssignmentDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsCompleted { get; set; }
}