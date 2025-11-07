using AutoMapper;
using UserStudentMgmt.Application.DTOs.Users;
using UserStudentMgmt.Application.Interfaces;
using UserStudentMgmt.Domain.Interfaces;
using UserStudentMgmt.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace UserStudentMgmt.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    // NUEVO MÉTODO IMPLEMENTADO
    public async Task<UserDto> CreateUserByAdminAsync(AdminCreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        
        // Hashing de la contraseña antes de guardar
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        // El RoleId ya viene seteado en el DTO por el admin
        
        await _userRepo.AddAsync(user);
        
        return _mapper.Map<UserDto>(user);
    }
    
    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null)
            return null;

        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateAsync(int id, UserRequestDto dto)
    {
        var existingUser = await _userRepo.GetByIdAsync(id);
        if (existingUser == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        _mapper.Map(dto, existingUser);

        // Si se proporciona una nueva contraseña, la hashea
        if (!string.IsNullOrEmpty(dto.Password))
        {
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }
        
        // No permitimos actualizar el RoleId aquí (eso se puede hacer en otro endpoint o se asume
        // que es parte de UserRequestDto, pero lo dejamos simple por ahora).

        await _userRepo.UpdateAsync(existingUser);
    }

    public async Task DeleteAsync(int id)
    {
        var  existingUser = await _userRepo.GetByIdAsync(id);
        if (existingUser == null) throw new KeyNotFoundException($"User with ID {id} not found.");
        await _userRepo.DeleteAsync(existingUser);
    }
}