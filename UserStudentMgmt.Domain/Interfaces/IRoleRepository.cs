using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Domain.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name);
}