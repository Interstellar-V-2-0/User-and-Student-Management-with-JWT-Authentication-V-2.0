using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Domain.Interfaces;

public interface IDocumentTypeRepository : IRepository<DocumentType>
{
    Task<DocumentType>  GetByDocumentTypeIdAsync(int documentTypeId);
}