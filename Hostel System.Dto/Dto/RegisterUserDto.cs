namespace Hostel_System.Dto.Dto
{
    public class RegisterUserDto
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
