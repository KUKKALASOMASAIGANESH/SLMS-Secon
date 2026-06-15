    namespace SLMS.Shared.DTOs.User;

    public class UserUpdateDto
    {
        public int EmployeeId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }