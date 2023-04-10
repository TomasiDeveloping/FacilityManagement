using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DataTransferObjects.Assignment;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly ILogger<AssignmentsController> _logger;

    public AssignmentsController(IAssignmentRepository assignmentRepository, ILogger<AssignmentsController> logger)
    {
        _assignmentRepository = assignmentRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<AssignmentDto>>> GetAllAssignments()
    {
        try
        {
            var assignments = await _assignmentRepository.GetAllAssignmentsAsync();
            if (!assignments.Any()) return NoContent();
            return Ok(assignments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("{assignmentId}")]
    public async Task<ActionResult<AssignmentDto>> GetAssignment(string assignmentId)
    {
        try
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(assignmentId);
            if (assignment == null) return BadRequest($"No Assignment found with id: {assignmentId}");
            return Ok(assignment);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("users/{userId}")]
    public async Task<ActionResult<List<AssignmentDto>>> GetUserAssignments(string userId)
    {
        try
        {
            var userAssignments = await _assignmentRepository.GetAssignmentsByUserIdAsync(userId);
            if (!userAssignments.Any()) return NoContent();
            return Ok(userAssignments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<AssignmentDto>>> GetOpenAssignments()
    {
        try
        {
            var assignments = await _assignmentRepository.GetAllOpenAssignmentsAsync();
            if (!assignments.Any()) return NoContent();
            return Ok(assignments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentDto>> InsertAssignment(CreateAssignmentDto assignmentDto)
    {
        try
        {
            var assignment = await _assignmentRepository.InsertAssignmentAsync(assignmentDto, GetUserEmail());
            return CreatedAtAction(nameof(GetAssignment), new {assignmentId = assignment.Id}, assignment);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{assignmentId}")]
    public async Task<ActionResult<AssignmentDto>> UpdateAssignment(string assignmentId, AssignmentDto assignmentDto)
    {
        try
        {
            if (assignmentId != assignmentDto.Id) return BadRequest("Error in Id");
            var assignment = await _assignmentRepository.UpdateAssignmentAsync(assignmentDto, GetUserEmail());
            return Ok(assignment);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{assignmentId}")]
    public async Task<ActionResult<bool>> DeleteAssignment(string assignmentId)
    {
        try
        {
            var deleteResponse = await _assignmentRepository.DeleteAssignmentAsync(assignmentId);
            return deleteResponse ? Ok(true) : BadRequest("Could not delete Assignment");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    private string GetUserEmail()
    {
        var test =  HttpContext.User.Identity is ClaimsIdentity identity ? identity.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown" : "Unknown";
        return test;
    }
}