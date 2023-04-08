using Application.DataTransferObjects.Assignment;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AssignmentRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AssignmentDto>> GetAllAssignmentsAsync()
    {
        var assignments = await _context.Assignments
            .AsNoTracking()
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return assignments;
    }

    public async Task<List<AssignmentDto>> GetAllOpenAssignmentsAsync()
    {
        var assignment = await _context.Assignments
            .AsNoTracking()
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .Where(a => !a.IsCompleted)
            .ToListAsync();
        return assignment;
    }

    public async Task<List<AssignmentDto>> GetAssignmentsByUserIdAsync(string userId)
    {
        var assignments = await _context.Assignments
            .AsNoTracking()
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .Where(a => a.UserId == userId && !a.IsCompleted)
            .ToListAsync();
        return assignments;
    }

    public async Task<AssignmentDto> GetAssignmentByIdAsync(string assignmentId)
    {
        var assignment = await _context.Assignments
            .AsNoTracking()
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(a => a.Id == assignmentId);
        return assignment;
    }

    public async Task<AssignmentDto> UpdateAssignmentAsync(AssignmentDto assignmentDto)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id.ToString() == assignmentDto.Id);
        if (assignment == null) return null;
        _mapper.Map(assignmentDto, assignment);
        assignment.ModifyDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return _mapper.Map<AssignmentDto>(assignment);
    }

    public async Task<AssignmentDto> InsertAssignmentAsync(AssignmentDto assignmentDto)
    {
        var assignment = _mapper.Map<Assignment>(assignmentDto);
        assignment.CreateDate = DateTime.Now;
        assignment.CreatedBy = "";
        assignment.Id = Guid.NewGuid();
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
        return _mapper.Map<AssignmentDto>(assignment);
    }

    public async Task<bool> DeleteAssignmentAsync(string assignmentId)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id.ToString() == assignmentId);
        if (assignment == null) return false;
        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return true;
    }
}