using AutoMapper;
using UserStudentMgmt.Application.DTOs.Roles;
using UserStudentMgmt.Application.Interfaces;
using UserStudentMgmt.Domain.Entities;
using UserStudentMgmt.Domain.Interfaces;

namespace UserStudentMgmt.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDto>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleResponseDto>>(roles);
    }

    public async Task<RoleResponseDto> CreateAsync(RoleRequestDto dto)
    {
        var exists = await _roleRepository.GetByNameAsync(dto.Name);
        if (exists != null)
            throw new Exception($"El rol '{dto.Name}' ya existe.");

        var role = _mapper.Map<Role>(dto);
        await _roleRepository.AddAsync(role);

        return _mapper.Map<RoleResponseDto>(role);
    }
}