using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
}