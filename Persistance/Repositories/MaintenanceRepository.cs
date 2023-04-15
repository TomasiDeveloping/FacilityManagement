using Application.DataTransferObjects.Maintenance;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MaintenanceRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MaintenanceDto>> GetMaintenancesAsync()
    {
        var maintenances = await _context.Maintainances
            .AsNoTracking()
            .ProjectTo<MaintenanceDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return maintenances;
    }

    public async Task<List<MaintenanceDto>> GetMaintenancesForMonthOrNotExecuted(int month)
    {
        var dateToFilter = new DateTime(DateTime.Now.Year, month, 1);
        var maintenances = await _context.Maintainances
            .AsNoTracking()
            .Where(m => m.NextExecution <= dateToFilter)
            .OrderBy(m => m.NextExecution)
            .ProjectTo<MaintenanceDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return maintenances;
    }

    public async Task<MaintenanceDto> GetMaintenanceByIdAsync(string id)
    {
        var maintenance = await _context.Maintainances
            .AsNoTracking()
            .ProjectTo<MaintenanceDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(m => m.Id == id);
        return maintenance;
    }

    public async Task<MaintenanceDto> CreateMaintenanceAsync(CreateMaintenanceDto createMaintenanceDto, string userName)
    {
        var maintenance = _mapper.Map<Maintenance>(createMaintenanceDto);
        maintenance.CreatedBy = userName;
        maintenance.ModifyDate = null;
        await _context.Maintainances.AddAsync(maintenance);
        await _context.SaveChangesAsync();
        return _mapper.Map<MaintenanceDto>(maintenance);
    }

    public async Task<bool> CloseMaintenanceAsync(string maintenanceId, MaintenanceDto maintenanceDto, string userName)
    {
        var maintenance = await _context.Maintainances.FirstOrDefaultAsync(m => m.Id.ToString() == maintenanceId);
        if (maintenance == null) return false;
        maintenance.LastExecution = DateTime.Now;
        maintenance.NextExecution =
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(maintenanceDto.Interval);
        maintenance.ModifyBy = userName;
        maintenance.ModifyDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMaintenanceAsync(string id)
    {
        var maintenance = await _context.Maintainances.FirstOrDefaultAsync(m => m.Id.ToString() == id);
        if (maintenance == null) return false;
        _context.Maintainances.Remove(maintenance);
        await _context.SaveChangesAsync();
        return true;
    }
}