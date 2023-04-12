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
            .ForMember(des => des.CreateDate, opt => opt.MapFrom(_ => DateTime.Now));
    }
}