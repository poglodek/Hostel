using Hostel_System.Database.Entity;

namespace Hostel_System.Dto.Dto
{
    public class ReservedInfoDto
    {
        public int Id { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public string RoomName { get; set; }
        public UserDto BookingUser { get; set; }
        public string? AdditionalInformation { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
    }
}
