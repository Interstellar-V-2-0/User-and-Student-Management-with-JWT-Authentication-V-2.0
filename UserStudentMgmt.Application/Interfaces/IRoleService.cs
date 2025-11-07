using UserStudentMgmt.Application.DTOs.Roles;

namespace UserStudentMgmt.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDto>> GetAllAsync();
    Task<RoleResponseDto> CreateAsync(RoleRequestDto dto);
}