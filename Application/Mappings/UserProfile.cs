using Application.DataTransferObjects.Authentication;
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
    }
}