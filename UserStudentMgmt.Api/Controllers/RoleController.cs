using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserStudentMgmt.Application.DTOs.Roles;
using UserStudentMgmt.Application.Interfaces;

namespace UserStudentMgmt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => Ok(await _roleService.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RoleRequestDto dto)
    {
        var role = await _roleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAllAsync), new { id = role.Id }, role);
    }
}