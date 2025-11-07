using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserStudentMgmt.Application.DTOs.Auth;
using UserStudentMgmt.Application.Interfaces;
using UserStudentMgmt.Domain.Entities;
using UserStudentMgmt.Domain.Interfaces;

namespace UserStudentMgmt.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepository;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepo, IConfiguration config, IMapper mapper, IRoleRepository roleRepository)
    {
        _userRepo = userRepo;
        _config = config;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }

    // Agregué parámetro creatorRole para validar si el usuario que crea tiene permisos de Admin
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request, string? creatorRole = null)
    {
        Role roleToAssign;

        // Solo si el creador es Admin y se especifica rol distinto a User, se asigna el rol solicitado
        if (creatorRole == "Admin" && !string.IsNullOrWhiteSpace(request.RoleName) && request.RoleName != "User")
        {
            roleToAssign = await _roleRepository.GetByNameAsync(request.RoleName);
            if (roleToAssign == null)
                throw new Exception($"The specified role '{request.RoleName}' does not exist.");
        }
        else
        {
            // Por defecto o si el creador no es Admin, se asigna el rol User
            roleToAssign = await _roleRepository.GetByNameAsync("User");
            if (roleToAssign == null)
            {
                // En caso que no exista el rol User en la BD, se crea
                roleToAssign = new Role { Name = "User" };
                await _roleRepository.AddAsync(roleToAssign);
            }
        }

        var user = _mapper.Map<User>(request);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        user.RoleId = roleToAssign.Id;

        await _userRepo.AddAsync(user);

        return GenerateJwtToken(user, roleToAssign.Name);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepo.GetByUserNameAsync(request.UserName);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials.");

        var role = user.Role != null ? user.Role.Name : "User";
        return GenerateJwtToken(user, role);
    }

    private AuthResponseDto GenerateJwtToken(User user, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.Role, role)  // Aquí uso ClaimTypes.Role que coincide con RoleClaimType configurado
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserName = user.UserName,
            Role = role
        };
    }
}
