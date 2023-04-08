using Application.DataTransferObjects.Assignment;

namespace Application.Interfaces;

public interface IAssignmentRepository
{
    Task<List<AssignmentDto>> GetAllAssignmentsAsync();
    Task<List<AssignmentDto>> GetAllOpenAssignmentsAsync();
    Task<List<AssignmentDto>> GetAssignmentsByUserIdAsync(string userId);
    Task<AssignmentDto> GetAssignmentByIdAsync(string assignmentId);
    Task<AssignmentDto> UpdateAssignmentAsync(AssignmentDto assignmentDto);
    Task<AssignmentDto> InsertAssignmentAsync(AssignmentDto assignmentDto);
    Task<bool> DeleteAssignmentAsync(string assignmentId);
}