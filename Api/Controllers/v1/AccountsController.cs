﻿using Application.DataTransferObjects.Authentication;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(IAccountRepository accountRepository, ILogger<AccountsController> logger)
    {
        _accountRepository = accountRepository;
        _logger = logger;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        try
        {
            var loginResponse = await _accountRepository.LoginAsync(loginDto);
            if (loginResponse.IsSuccessful) return Ok(loginResponse);

            return Unauthorized(loginResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}