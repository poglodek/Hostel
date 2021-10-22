using System.ComponentModel.DataAnnotations;

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
        [Required]
        public DateTime BirthDay { get; set; }
        public bool IsMale { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string? CarRegistrationNumber { get; set; }
    }
}
