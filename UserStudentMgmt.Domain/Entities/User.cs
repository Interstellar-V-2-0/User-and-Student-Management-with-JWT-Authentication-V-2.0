namespace UserStudentMgmt.Domain.Entities;

public class User : Person
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public int RoleId {get; set;}
    public Role? Role {get; set;}
}