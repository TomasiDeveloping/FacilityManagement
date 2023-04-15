using Application.DataTransferObjects.Maintenance;

namespace Application.Interfaces;

public interface IMaintenanceRepository
{
    Task<List<MaintenanceDto>> GetMaintenancesAsync();
    Task<List<MaintenanceDto>> GetMaintenancesForMonthOrNotExecuted(int month);
    Task<MaintenanceDto> GetMaintenanceByIdAsync(string id);
    Task<MaintenanceDto> CreateMaintenanceAsync(CreateMaintenanceDto createMaintenanceDto, string userName);
    Task<bool> CloseMaintenanceAsync(string maintenanceId, MaintenanceDto maintenanceDto, string userName);
    Task<bool> DeleteMaintenanceAsync(string id);
}