using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Shared.DTOs.User
{
    public class UserRoleDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

    }
}
