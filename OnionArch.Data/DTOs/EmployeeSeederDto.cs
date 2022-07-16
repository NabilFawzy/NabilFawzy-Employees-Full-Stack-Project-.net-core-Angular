using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Repository.DTOs
{
    public class EmployeeSeederDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Job { get; set; }
        public Guid PositionTypeId { get; set; }
        public string PositionType { get; set; }

        public Boolean isAdmin { get; set; } = false;
    }
}
