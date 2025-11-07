namespace UserStudentMgmt.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
}