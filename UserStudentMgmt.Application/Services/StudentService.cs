using AutoMapper;
using UserStudentMgmt.Application.DTOs.Students;
using UserStudentMgmt.Application.Interfaces;
using UserStudentMgmt.Domain.Entities;
using UserStudentMgmt.Domain.Interfaces;

namespace UserStudentMgmt.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepo, IMapper mapper)
    {
        _studentRepo = studentRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await _studentRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null)
            throw new KeyNotFoundException("Student not found");

        return _mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto> CreateAsync(StudentRequestDto dto)
    {
        var student = _mapper.Map<Student>(dto);
        await _studentRepo.AddAsync(student);

        return _mapper.Map<StudentDto>(student);
    }

    public async Task UpdateAsync(int id, StudentRequestDto dto)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null)
            throw new KeyNotFoundException("Student not found");

        _mapper.Map(dto, student);
        await _studentRepo.UpdateAsync(student);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _studentRepo.GetByIdAsync(id);
        if (student == null)
            throw new KeyNotFoundException("Student not found");

        await _studentRepo.DeleteAsync(student);
    }
}