using UserStudentMgmt.Application.DTOs.Students;

namespace UserStudentMgmt.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(StudentRequestDto dto);
    Task UpdateAsync(int id, StudentRequestDto dto);
    Task DeleteAsync(int id);
}