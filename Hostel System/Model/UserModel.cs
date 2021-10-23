namespace Hostel_System.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsMale { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public string? CarRegistrationNumber { get; set; }
    }
}
