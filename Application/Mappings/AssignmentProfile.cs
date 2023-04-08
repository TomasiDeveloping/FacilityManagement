using Application.DataTransferObjects.Assignment;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class AssignmentProfile : Profile
{
    public AssignmentProfile()
    {
        CreateMap<AssignmentDto, Assignment>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

        CreateMap<Assignment, AssignmentDto>();
    }
}