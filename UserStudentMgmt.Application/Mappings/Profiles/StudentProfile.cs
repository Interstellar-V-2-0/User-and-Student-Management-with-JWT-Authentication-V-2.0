using AutoMapper;
using UserStudentMgmt.Application.DTOs.Students;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Application.Mappings.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, StudentRequestDto>().ReverseMap();
    }
}