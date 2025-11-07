using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserStudentMgmt.Application.DTOs.Auth;
using UserStudentMgmt.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("admin/create-user")]
    public async Task<IActionResult> CreateUserAsAdmin([FromBody] RegisterRequestDto dto)
    {
        var creatorRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var result = await _authService.RegisterAsync(dto, creatorRole);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }
}