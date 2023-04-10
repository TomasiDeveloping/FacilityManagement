using Application.DataTransferObjects.Assignment;

namespace Application.Interfaces;

public interface IAssignmentRepository
{
    Task<List<AssignmentDto>> GetAllAssignmentsAsync();
    Task<List<AssignmentDto>> GetAllOpenAssignmentsAsync();
    Task<List<AssignmentDto>> GetAssignmentsByUserIdAsync(string userId);
    Task<AssignmentDto> GetAssignmentByIdAsync(string assignmentId);
    Task<AssignmentDto> UpdateAssignmentAsync(AssignmentDto assignmentDto, string userName);
    Task<AssignmentDto> InsertAssignmentAsync(CreateAssignmentDto assignmentDto, string userName);
    Task<bool> DeleteAssignmentAsync(string assignmentId);
}