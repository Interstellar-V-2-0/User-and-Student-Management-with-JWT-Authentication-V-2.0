using Microsoft.EntityFrameworkCore;
using UserStudentMgmt.Domain.Entities;

namespace UserStudentMgmt.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentTypes");

                entity.HasKey(dt => dt.Id);

                entity.Property(dt => dt.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(dt => dt.Users)
                      .WithOne(u => u.DocType)
                      .HasForeignKey(u => u.DocTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.DocumentNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(u => u.PhoneNumber)
                      .HasMaxLength(20);

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(255);
            });
        }
    }
}
