using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetByDocumentTypeAsync(int docTypeId);
}