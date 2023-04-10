using Application.DataTransferObjects.Assignment;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class AssignmentProfile : Profile
{
    public AssignmentProfile()
    {
        CreateMap<CreateAssignmentDto, Assignment>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(des => des.CreateDate, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<AssignmentDto, Assignment>();
        CreateMap<Assignment, AssignmentDto>()
            .ForMember(des => des.UserFullName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
    }
}