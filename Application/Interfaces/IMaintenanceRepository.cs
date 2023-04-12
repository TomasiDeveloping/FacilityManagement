using Application.DataTransferObjects.Maintenance;

namespace Application.Interfaces;

public interface IMaintenanceRepository
{
    Task<List<MaintenanceDto>> GetMaintenancesAsync();
    Task<MaintenanceDto> GetMaintenanceByIdAsync(string id);
    Task<MaintenanceDto> CreateMaintenanceAsync(CreateMaintenanceDto createMaintenanceDto, string userName);
    Task<bool> DeleteMaintenanceAsync(string id);
}