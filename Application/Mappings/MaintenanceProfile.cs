using Application.DataTransferObjects.Maintenance;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MaintenanceProfile : Profile
{
    public MaintenanceProfile()
    {
        CreateMap<Maintenance, MaintenanceDto>().ReverseMap();
        CreateMap<CreateMaintenanceDto, Maintenance>()
            .ForMember(des => des.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(des => des.NextExecution, opt => opt.MapFrom(src => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(src.Interval)))
            .ForMember(des => des.CreateDate, opt => opt.MapFrom(_ => DateTime.Now));
    }
}