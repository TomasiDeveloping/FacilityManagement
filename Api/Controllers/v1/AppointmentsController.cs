using System.Security.Claims;
using Application.DataTransferObjects.Appointment;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IAppointmentRepository appointmentRepository, ILogger<AppointmentsController> logger)
    {
        _appointmentRepository = appointmentRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<AppointmentDto>>> GetAllAppointments()
    {
        try
        {
            var appointments = await _appointmentRepository.GetAppointmentsAsync();
            if (!appointments.Any()) return NoContent();
            return Ok(appointments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("{appointmentId}")]
    public async Task<ActionResult<AppointmentDto>> GetAppointment(string appointmentId)
    {
        try
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
            if (appointment == null) return BadRequest($"No Appointment found for id: {appointmentId}");
            return Ok(appointment);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("/users/{userId}")]
    public async Task<ActionResult<List<AppointmentDto>>> GetUserAppointments(string userId)
    {
        try
        {
            var userAppointments = await _appointmentRepository.GetUserAppointmentsAsync(userId);
            if (!userAppointments.Any()) return NoContent();
            return Ok(userAppointments);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{appointmentId}")]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointment(string appointmentId,
        AppointmentDto appointmentDto)
    {
        try
        {
            if (appointmentId != appointmentDto.Id) return BadRequest("Error in Ids");
            var appointment = await _appointmentRepository.UpdateAppointmentAsync(appointmentDto, GetUserEmail());
            if (appointment == null) return BadRequest("Could not Update Appointment");
            return Ok(appointment);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto createAppointmentDto)
    {
        try
        {
            var appointment = await _appointmentRepository.CreateAppointmentAsync(createAppointmentDto, GetUserEmail());
            if (appointment == null) return BadRequest("Could not Create new Appointment");
            return Ok(appointment);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{appointmentId}")]
    public async Task<ActionResult<bool>> DeleteAppointment(string appointmentId)
    {
        try
        {
            var isDelete = await _appointmentRepository.DeleteAppointmentAsync(appointmentId);
            return isDelete ? Ok(true) : BadRequest("Could not delete Appointment");
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
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