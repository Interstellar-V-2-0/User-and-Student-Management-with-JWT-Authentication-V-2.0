using AutoMapper;
using UserStudentMgmt.Application.DTOs.Users;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Application.Mappings.Profiles;

public class UserProfile : Profile
{

    public UserProfile()
    {
        // Mapeo de Entidad a DTO de Respuesta
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocType.Name))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        
        // Mapeo para la Actualización (PUT)
        CreateMap<UserRequestDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.DocType, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
            
        // NUEVO MAPEO PARA CREACIÓN POR ADMIN (POST)
        CreateMap<AdminCreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Lo hasheamos manualmente
            .ForMember(dest => dest.DocType, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }

}