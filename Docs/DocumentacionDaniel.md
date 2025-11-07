# User & Student Management API con .NET 8

## 📝 Descripción General
API REST desarrollada en **.NET 8** con arquitectura en capas (API, Application, Domain, Infrastructure) para gestión de usuarios y estudiantes, autenticación JWT y persistencia en MySQL.  
Cuenta con documentación Swagger, Dockerización y un seeder inicial para crear un usuario administrador y tipos de documento.

---

## 📂 Estructura del Proyecto

```
UserStudentMgmt/
│
├─ UserStudentMgmt.Api/ # Controladores, Program.cs, Swagger
├─ UserStudentMgmt.Application/ # Servicios, DTOs, interfaces
├─ UserStudentMgmt.Domain/ # Entidades, interfaces de repositorios
├─ UserStudentMgmt.Infrastructure/ # DbContext, repositorios, migraciones, seed
└─ docker-compose.yml
```

---

```csharp
 Capas:

- **API:** Orquesta llamadas, maneja rutas y Swagger.
- **Application:** Contiene servicios (`IUserService`, `UserService`), DTOs, lógica de negocio y pruebas unitarias.
- **Domain:** Entidades puras (`User`, `Person`, `DocumentType`), interfaces de repositorios y reglas de negocio.
- **Infrastructure:** Implementación de repositorios, `DbContext`, migraciones EF Core y seed de datos.
```

---

## 🔹 Entidades Principales

### Person (abstracta)
```csharp
public abstract class Person
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int DocTypeId { get; set; }
    public DocumentType? DocType { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
```

User

```csharp
public class User : Person
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
```

DocumentType
```csharp
public class DocumentType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; } = new List<User>();
}
```
🔹 DbContext
```csharp
public class UserStudentMgmtDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }

    public UserStudentMgmtDbContext(DbContextOptions<UserStudentMgmtDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
```
🔹 Seeder
Seeder inicial para crear:

Tipos de documento (Cédula de Ciudadanía, Tarjeta de Identidad, Cédula de Extranjería)

Usuario administrador (admin / Admin123*)

```csharp
public static class DbSeeder
{
    public static async Task SeedAsync(UserStudentMgmtDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!context.Set<DocumentType>().Any())
        {
            var docTypes = new List<DocumentType>
            {
                new DocumentType { Name = "Cédula de Ciudadanía" },
                new DocumentType { Name = "Tarjeta de Identidad" },
                new DocumentType { Name = "Cédula de Extranjería" }
            };
            await context.Set<DocumentType>().AddRangeAsync(docTypes);
            await context.SaveChangesAsync();
        }

        if (!context.Set<User>().Any(u => u.UserName == "admin"))
        {
            var firstDocType = await context.Set<DocumentType>().FirstAsync();
            var adminUser = new User
            {
                Name = "Administrador",
                LastName = "General",
                DocTypeId = firstDocType.Id,
                DocumentNumber = "1000000000",
                Email = "admin@mail.com",
                PhoneNumber = "3000000000",
                UserName = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*")
            };
            await context.Set<User>().AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }
}
```

Seeder ejecutado en Program.cs antes de iniciar la app:

```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserStudentMgmtDbContext>();
    await DbSeeder.SeedAsync(dbContext);
}
```

🔹 Dockerización
Dockerfile para la API .NET 8

docker-compose.yml levanta:

MySQL 8.0

API .NET

phpMyAdmin (opcional para administración visual)

Contenedores levantados correctamente y migraciones ejecutadas al iniciar el seeder.

🔹 Swagger
Disponible en: http://localhost:5000 (o puerto definido)

Documenta todos los endpoints, incluyendo JWT en cabecera Authorization.

🔹 Comprobación de Seeder

```sql
SELECT UserName, Email FROM Users WHERE UserName = 'admin';
```

Resultado:

```pgsql
+----------+----------------+
| UserName | Email          |
+----------+----------------+
| admin    | admin@mail.com |
+----------+----------------+
Usuario administrador creado correctamente.
```

🔹 Próximos pasos (HU completada)
Validar endpoints con Postman

Crear al menos 2 pruebas unitarias para capa Application

Confirmar que todas las migraciones se aplican correctamente

Entrega: código, docker-compose funcional y colección Postman