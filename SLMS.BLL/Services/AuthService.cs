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
        dto.Username = dto.Username.Trim().ToLower();

        var user = await _userRepository
            .GetUserWithRolesAsync(dto.Username);

        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        bool isValid =
            BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

        if (!isValid)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "wrong password"
            };
        }

        var roleName =
            user.UserRoles
                .Select(ur => ur.Role.RoleName)
                .FirstOrDefault();

        if (string.IsNullOrEmpty(roleName))
        {
            return new AuthResponse
            {
                Success = false,
                Message = "No role assigned to this user"
            };
        }

        var token = _jwtTokenHelper.GenerateToken(user, roleName);

        return new AuthResponse
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            UserId = user.Id,
            Role = roleName,
            EmployeeId = user.EmployeeId
        };
    }

    public async Task<AuthResponse> RegisterAsync(RegisterDto dto)
    {
        var employeeNumber = dto.EmployeeNumber.Trim().ToLower();
        var employeeName = dto.EmployeeName.Trim();
        var username = dto.Username.Trim().ToLower();

        var employees = await _employeeRepository.FindAsync(
            e => e.EmployeeNumber.ToLower() == employeeNumber);

        var employee = employees.FirstOrDefault();

        if (employee == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Employee Number not found"
            };
        }

        if (!employee.FullName.Trim().Equals(
                employeeName,
                StringComparison.OrdinalIgnoreCase))
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Employee Name does not match"
            };
        }

        if (string.IsNullOrWhiteSpace(dto.Password) ||
            dto.Password.Length < 8 ||
            !dto.Password.Any(char.IsUpper) ||
            !dto.Password.Any(char.IsLower) ||
            !dto.Password.Any(char.IsDigit) ||
            !dto.Password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character."
            };
        }

        var existingUser = (await _userRepository.FindAsync(
            u => u.Username.ToLower() == username))
            .FirstOrDefault();

        if (existingUser != null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Username already exists"
            };
        }

        var employeeAlreadyRegistered = (await _userRepository.FindAsync(
            u => u.EmployeeId == employee.Id))
            .FirstOrDefault();

        if (employeeAlreadyRegistered != null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Employee is already registered"
            };
        }

        var user = new User
        {
            EmployeeId = employee.Id,
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        await _userRepository.AssignRoleAsync(user.Id, 3);

        return new AuthResponse
        {
            Success = true,
            Message = "Registration successful"
        };
    }
















    /* public async Task<AuthResponse> RegisterAsync(RegisterDto dto)
     {
         dto.Username = dto.Username.Trim().ToLower();
         dto.EmployeeName = dto.EmployeeName.Trim();
         dto.EmployeeNumber = dto.EmployeeNumber.Trim();

         var employee = (await _employeeRepository.FindAsync(
             e => e.EmployeeNumber == dto.EmployeeNumber &&
                  e.FullName.ToLower() == dto.EmployeeName.Trim().ToLower()))
             .FirstOrDefault();

         if (employee == null)
         {
             return new AuthResponse
             {
                 Success = false,
                 Message = "Employee Number not found"
             };
         }

         if (!employee.FullName.Equals(dto.EmployeeName.Trim(),
             StringComparison.OrdinalIgnoreCase))
         {
             return new AuthResponse
             {
                 Success = false,
                 Message = "Employee Name does not match"
             };
         }

         // Password Validation
         if (string.IsNullOrWhiteSpace(dto.Password) ||
             dto.Password.Length < 8 ||
             !dto.Password.Any(char.IsUpper) ||
             !dto.Password.Any(char.IsLower) ||
             !dto.Password.Any(char.IsDigit) ||
             !dto.Password.Any(ch => !char.IsLetterOrDigit(ch)))
         {
             return new AuthResponse
             {
                 Success = false,
                 Message = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number and one special character."
             };
         }

         var existingUser = (await _userRepository.FindAsync(
             u => u.Username.ToLower() == dto.Username))
             .FirstOrDefault();

         if (existingUser != null)
         {
             return new AuthResponse
             {
                 Success = false,
                 Message = "Username already exists"
             };
         }

         // Prevent same employee from registering twice
         var employeeAlreadyRegistered = (await _userRepository.FindAsync(
             u => u.EmployeeId == employee.Id))
             .FirstOrDefault();

         if (employeeAlreadyRegistered != null)
         {
             return new AuthResponse
             {
                 Success = false,
                 Message = "Employee is already registered"
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


         // Assign default User role to every newly registered user
         await _userRepository.AssignRoleAsync(user.Id, 3);

         return new AuthResponse
         {
             Success = true,
             Message = "Registration successful"
         };
     }*/

    public async Task<AuthResponse> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        dto.Username = dto.Username.Trim().ToLower();

        var user = (await _userRepository.FindAsync(
            u => u.Username.ToLower() == dto.Username))
            .FirstOrDefault();

        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        var employee = await _employeeRepository.GetByIdAsync(user.EmployeeId);

        if (employee == null ||
            employee.EmployeeNumber != dto.EmployeeNumber)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "Employee verification failed"
            };
        }

        user.PasswordHash =
            BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

        await _userRepository.SaveChangesAsync();

        return new AuthResponse
        {
            Success = true,
            Message = "Password reset successful"
        };
    }
}
