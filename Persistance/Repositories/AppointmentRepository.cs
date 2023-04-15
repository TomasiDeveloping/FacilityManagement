using Application.DataTransferObjects.Appointment;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<AppointmentDto>> GetAppointmentsAsync()
    {
        var appointments = await _context.Appointments
            .AsNoTracking()
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return appointments;
    }

    public async Task<AppointmentDto> GetAppointmentByIdAsync(string appointmentId)
    {
        var appointment = await _context.Appointments
            .AsNoTracking()
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);
        return appointment;
    }

    public async Task<List<AppointmentDto>> GetUserAppointmentsAsync(string userId)
    {
        var userAppointments = await _context.Appointments
            .AsNoTracking()
            .Where(a => a.UserId.ToString() == userId)
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return userAppointments;
    }

    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto, string userEmail)
    {
        var appointment = _mapper.Map<Appointment>(createAppointmentDto);
        appointment.CreatedBy = userEmail;
        appointment.CreateDate = DateTime.Now;
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto, string userEmail)
    {
        var appointment =
            await _context.Appointments.FirstOrDefaultAsync(a => a.Id.ToString() == appointmentDto.Id);
        if (appointment == null) return null;
        _mapper.Map(appointmentDto, appointment);
        appointment.ModifyBy = userEmail;
        appointment.ModifyDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<bool> DeleteAppointmentAsync(string appointmentId)
    {
        var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id.ToString() == appointmentId);
        if (appointment == null) return false;
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
}