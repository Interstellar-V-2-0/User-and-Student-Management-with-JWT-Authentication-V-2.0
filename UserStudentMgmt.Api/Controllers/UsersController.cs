using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserStudentMgmt.Application.DTOs.Users;
using UserStudentMgmt.Application.Interfaces;

namespace UserStudentMgmt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Todos requieren autenticación
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, User")] // Todos pueden ver la lista
    public async Task<IActionResult> GetAllAsync() => Ok(await _userService.GetAllAsync());

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // Solo Admin puede modificar
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserRequestDto dto)
    {
        await _userService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Solo Admin puede eliminar
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}