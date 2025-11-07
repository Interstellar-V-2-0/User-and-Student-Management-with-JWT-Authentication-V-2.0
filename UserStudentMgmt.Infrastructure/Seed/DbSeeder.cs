using Microsoft.EntityFrameworkCore;
using UserStudentMgmt.Infrastructure.Data;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();
            
            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "User" }
                };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }
            
            if (!await context.DocumentTypes.AnyAsync())
            {
                var docTypes = new List<DocumentType>
                {
                    new DocumentType { Name = "Citizenship Card" },
                    new DocumentType { Name = "Identity Card" },
                    new DocumentType { Name = "Foreigner ID" },
                    new DocumentType { Name = "Other" }
                };

                await context.DocumentTypes.AddRangeAsync(docTypes);
                await context.SaveChangesAsync();
            }
            
            if (!await context.Users.AnyAsync(u => u.UserName == "superAdmin"))
            {
                var firstDocType = await context.DocumentTypes.FirstAsync();
                var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");

                var adminUser = new User
                {
                    Name = "Administrador",
                    LastName = "General",
                    DocTypeId = firstDocType.Id,
                    DocumentNumber = "1000000000",
                    Email = "admin@mail.com",
                    PhoneNumber = "3000000000",
                    UserName = "superAdmin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                    RoleId = adminRole.Id
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}