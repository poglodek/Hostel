using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
