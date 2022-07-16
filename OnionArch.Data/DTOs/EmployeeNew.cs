using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Data.DTOs
{
    public class EmployeeNew
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Job { get; set; }
        public Guid PositionTypeId { get; set; }
        public string PositionType { get; set; }

        public Boolean isAdmin { get; set; } = false;
        public string Password { get; set; }
    }
}
