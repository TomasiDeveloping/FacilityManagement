using Application.DataTransferObjects.Authentication;

namespace Application.Interfaces;

public interface IAccountRepository
{
    Task<RegistrationResponseDto> Register(RegistrationDto registrationDto, string role);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}