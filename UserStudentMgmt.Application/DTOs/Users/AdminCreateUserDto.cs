namespace UserStudentMgmt.Application.DTOs.Users;

public class AdminCreateUserDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int DocTypeId { get; set; }
    public string DocumentNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; } 
}