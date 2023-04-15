using System.Security.Claims;
using Application.DataTransferObjects.Maintenance;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class MaintenancesController : ControllerBase
{
    private readonly ILogger<MaintenancesController> _logger;
    private readonly IMaintenanceRepository _maintenanceRepository;

    public MaintenancesController(IMaintenanceRepository maintenanceRepository, ILogger<MaintenancesController> logger)
    {
        _maintenanceRepository = maintenanceRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<MaintenanceDto>>> GetMaintenances()
    {
        try
        {
            var maintenances = await _maintenanceRepository.GetMaintenancesAsync();
            if (!maintenances.Any()) return NoContent();
            return Ok(maintenances);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<MaintenanceDto>>> GetMaintenancesByMonth(int month)
    {
        try
        {
            var maintenances = await _maintenanceRepository.GetMaintenancesForMonthOrNotExecuted(month);
            if (!maintenances.Any()) return NoContent();
            return Ok(maintenances);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("{maintenanceId}")]
    public async Task<ActionResult<MaintenanceDto>> GetMaintenance(string maintenanceId)
    {
        try
        {
            var maintenance = await _maintenanceRepository.GetMaintenanceByIdAsync(maintenanceId);
            if (maintenance == null) return NotFound($"No Maintenance found for id: {maintenanceId}");
            return Ok(maintenance);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceDto>> CreateMaintenance(CreateMaintenanceDto createMaintenanceDto)
    {
        try
        {
            var maintenance =
                await _maintenanceRepository.CreateMaintenanceAsync(createMaintenanceDto, GetUserEmail());
            return CreatedAtAction(nameof(GetMaintenance), new {maintenanceId = maintenance.Id}, maintenance);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not Create Maintenance");
        }
    }

    [HttpPut("[action]/{maintenanceId}")]
    public async Task<ActionResult<bool>> CloseMaintenance(string maintenanceId,
        [FromBody] MaintenanceDto maintenanceDto)
    {
        try
        {
            if (maintenanceId != maintenanceDto.Id) return BadRequest("Error in ids");
            var response =
                await _maintenanceRepository.CloseMaintenanceAsync(maintenanceId, maintenanceDto, GetUserEmail());
            return response ? Ok(true) : BadRequest("Could not Close Maintenance");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{maintenanceId}")]
    public async Task<ActionResult<bool>> DeleteMaintenance(string maintenanceId)
    {
        try
        {
            var checkDelete = await _maintenanceRepository.DeleteMaintenanceAsync(maintenanceId);
            return checkDelete ? Ok(true) : BadRequest("Could not Delete Maintenance");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not Delete Maintenance");
        }
    }

    private string GetUserEmail()
    {
        var test = HttpContext.User.Identity is ClaimsIdentity identity
            ? identity.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown"
            : "Unknown";
        return test;
    }
}