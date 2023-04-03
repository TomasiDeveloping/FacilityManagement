using Application.DataTransferObjects.Authentication;
using Application.DataTransferObjects.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationDto, User>()
            .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<User, UserDto>()
            .ForMember(des => des.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
    }
}