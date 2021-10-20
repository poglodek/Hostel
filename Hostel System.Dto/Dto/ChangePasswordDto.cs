using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Dto.Dto
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string ActualPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReTypNewPassword { get; set; }
    }
}
