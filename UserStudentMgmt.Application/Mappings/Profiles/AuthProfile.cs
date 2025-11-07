using AutoMapper;
using UserStudentMgmt.Application.DTOs.Auth;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Application.Mappings.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequestDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.DocType, opt => opt.Ignore());
    }
}