using Microsoft.EntityFrameworkCore;
using UserStudentMgmt.Domain.Entities;
using UserStudentMgmt.Domain.Interfaces;
using UserStudentMgmt.Infrastructure.Data;

namespace UserStudentMgmt.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Student>> GetByDocumentTypeAsync(int docTypeId)
    {
        return await _dbSet.Where(s => s.DocTypeId == docTypeId).ToListAsync();
    }
}