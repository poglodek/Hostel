using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Model
{
    public class RegisterUserModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }
        [Required]
        public bool IsMale { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string? CarRegistrationNumber { get; set; }
    }
}
