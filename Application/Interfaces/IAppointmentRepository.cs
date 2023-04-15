using Application.DataTransferObjects.Appointment;

namespace Application.Interfaces;

public interface IAppointmentRepository
{
    Task<List<AppointmentDto>> GetAppointmentsAsync();
    Task<AppointmentDto> GetAppointmentByIdAsync(string appointmentId);
    Task<List<AppointmentDto>> GetUserAppointmentsAsync(string userId);
    Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto, string userEmail);
    Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto, string userEmail);
    Task<bool> DeleteAppointmentAsync(string appointmentId);
}