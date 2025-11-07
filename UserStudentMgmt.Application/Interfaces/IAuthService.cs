using UserStudentMgmt.Application.DTOs.Auth;
using UserStudentMgmt.Application.DTOs.Users;

namespace UserStudentMgmt.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request, string? creatorRole = null);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
}