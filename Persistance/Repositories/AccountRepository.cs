using System.IdentityModel.Tokens.Jwt;
using Application.DataTransferObjects.Authentication;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<User> _userManager;

    public AccountRepository(UserManager<User> userManager, IJwtService jwtService, IMapper mapper, IEmailSender emailSender)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    public async Task<RegistrationResponseDto> Register(RegistrationDto registrationDto, string role)
    {
        var user = _mapper.Map<User>(registrationDto);
        var result = await _userManager.CreateAsync(user, registrationDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return new RegistrationResponseDto()
            {
                IsSuccessful = false,
                Errors = errors
            };
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (roleResult.Succeeded)
            return new RegistrationResponseDto()
            {
                IsSuccessful = true
            };
        {
            var errors = result.Errors.Select(e => e.Description);
            return new RegistrationResponseDto()
            {
                IsSuccessful = false,
                Errors = errors
            };
        }

    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return new AuthResponseDto
            {
                ErrorMessage = "Invalid Authentication",
                IsSuccessful = false
            };

        var signinCredentials = _jwtService.GetSigningCredentials();
        var claims = await _jwtService.GetClaimsAsync(user);
        var tokenOptions = _jwtService.GenerateTokenOptions(signinCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new AuthResponseDto
        {
            IsSuccessful = true,
            Token = token
        };
    }

    public async Task<ForgotPasswordResponseDto> SendForgotPasswordEmailAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null)
        {
            return new ForgotPasswordResponseDto()
            {
                IsSuccessful = false,
                ErrorMessage = "Invalid Request"
            };
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var param = new Dictionary<string, string>()
        {
            {"token", token},
            {"email", forgotPasswordDto.Email}
        };
        var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientUri, param);
        var message = new EmailMessage(new[] {user.Email}, "Reset Password", callback);

        await _emailSender.SendEmailAsync(message);
        return new ForgotPasswordResponseDto
        {
            IsSuccessful = true
        };
    }

    public async Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
        {
            return new ResetPasswordResponseDto()
            {
                IsSuccessful = false,
                Errors = new[] {"Invalid Request"}
            };
        }

        var resetPasswordResult =
            await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (resetPasswordResult.Succeeded)
        {
            return new ResetPasswordResponseDto()
            {
                IsSuccessful = true
            };
        }

        var errors = resetPasswordResult.Errors.Select(x => x.Description);
        return new ResetPasswordResponseDto()
        {
            IsSuccessful = false,
            Errors = errors
        };
    }
}