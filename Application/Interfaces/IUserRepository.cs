using Application.DataTransferObjects.User;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<List<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserByIdAsync(string userId);
}