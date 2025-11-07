using UserStudentMgmt.Application.DTOs.Users;

namespace UserStudentMgmt.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUserByAdminAsync(AdminCreateUserDto dto);
    
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, UserRequestDto dto);
    Task DeleteAsync(int id);
}