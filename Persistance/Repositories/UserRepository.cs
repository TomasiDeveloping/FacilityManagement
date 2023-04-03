using Application.DataTransferObjects.User;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var users = await _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
        return users;
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
        return user;
    }
}