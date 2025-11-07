using AutoMapper;
using UserStudentMgmt.Application.DTOs.DocumentTypes;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Application.Mappings.Profiles;

public class DocumentTypeProfile : Profile
{
    public DocumentTypeProfile()
    {
        CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
    }
}