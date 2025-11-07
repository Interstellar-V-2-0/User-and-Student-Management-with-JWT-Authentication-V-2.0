using Microsoft.EntityFrameworkCore;
using UserStudentMgmt.Domain.Entities;
using UserStudentMgmt.Domain.Interfaces;
using UserStudentMgmt.Infrastructure.Data;

namespace UserStudentMgmt.Infrastructure.Repositories
{
    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(AppDbContext context) : base(context) {}

        public async Task<DocumentType> GetByDocumentTypeIdAsync(int documentTypeId)
        {
            return await _dbSet.FirstOrDefaultAsync(d => d.Id == documentTypeId);
        }
    }
}