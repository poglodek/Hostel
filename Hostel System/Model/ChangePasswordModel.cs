using System.ComponentModel.DataAnnotations;

namespace Hostel_System.Model
{
    public class ChangePasswordModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string ActualPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string ReTypNewPassword { get; set; }
    }
}
