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
