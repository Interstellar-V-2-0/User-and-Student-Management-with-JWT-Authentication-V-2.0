using UserStudentMgmt.Application.DTOs.DocumentTypes;

namespace UserStudentMgmt.Application.Interfaces;

public interface IDocumentTypeService
{
    Task<IEnumerable<DocumentTypeDto>> GetAllAsync();
}