using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Model
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsMale { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string? CarRegistrationNumber { get; set; }
    }
}
