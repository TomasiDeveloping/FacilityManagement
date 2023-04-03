using Application.DataTransferObjects.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetUsersAsync();
            if (!users.Any()) return NoContent();
            return Ok(users);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}