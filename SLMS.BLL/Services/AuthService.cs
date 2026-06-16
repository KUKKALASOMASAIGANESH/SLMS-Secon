using SLMS.BLL.Interfaces;
using SLMS.DAL.Repositories.Interfaces;
using SLMS.Shared.DTOs;
using SLMS.Shared.Responses;
using BCrypt.Net;
using SLMS.DOL.Entities;
using SLMS.BLL.Helpers;

namespace SLMS.BLL.Services;





public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly JwtTokenHelper _jwtTokenHelper;


    public AuthService(
        IUserRepository userRepository,
        IEmployeeRepository employeeRepository,
        JwtTokenHelper jwtTokenHelper)
    {
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _jwtTokenHelper = jwtTokenHelper;
    }

    /*public async Task<AuthResponse> LoginAsync(LoginDto dto)
    {
        throw new NotImplementedException();
    }*/

    /*   public async Task<AuthResponse> RegisterAsync(RegisterDto dto)
       {
           throw new NotImplementedException();
       } */



    public async Task<AuthResponse> LoginAsync(LoginDto dto)
    {
        var user = (await _userRepository.FindAsync(u => u.Username == dto.Username))
            .FirstOrDefault();

        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!isValid)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Invalid password"
            };
        }

        var token = _jwtTokenHelper.GenerateToken(user);

        return new AuthResponse
        {
            Success = true,
            Message = "Login successful",
            Token = token
        };
    }







    public async Task<AuthResponse> RegisterAsync(RegisterDto dto)
    {
        var employee = (await _employeeRepository.FindAsync(
            e => e.EmployeeNumber == dto.EmployeeNumber))
            .FirstOrDefault();

        if (employee == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Employee not found"
            };
        }

        var existingUser = (await _userRepository.FindAsync(
            u => u.Username == dto.Username))
            .FirstOrDefault();

        if (existingUser != null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Username already exists"
            };
        }

        var user = new User
        {
            EmployeeId = employee.Id,
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse
        {
            Success = true,
            Message = "Registration successful"
        };
    }
}