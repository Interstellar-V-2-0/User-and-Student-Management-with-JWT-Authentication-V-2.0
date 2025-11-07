using AutoMapper;
using UserStudentMgmt.Application.DTOs.Roles;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Application.Mappings.Profiles;

public class RoleProfile : Profile
{ 
    public RoleProfile()
    {
        CreateMap<Role, RoleResponseDto>();
        CreateMap<RoleRequestDto, Role>();
    }

}