using Application.DataTransferObjects.Authentication;

namespace Application.Interfaces;

public interface IAccountRepository
{
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}