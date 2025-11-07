using Microsoft.EntityFrameworkCore;
using UserStudentMgmt.Domain.Entities;
using UserStudentMgmt.Domain.Interfaces;
using UserStudentMgmt.Infrastructure.Data;

namespace UserStudentMgmt.Infrastructure.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(r => r.Name == name);
    }
}