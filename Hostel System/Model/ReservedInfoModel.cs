namespace Hostel_System.Model
{
    public class ReservedInfoModel
    {
        public int Id { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public string RoomName { get; set; }
        public UserModel BookingUser { get; set; }
        public string? AdditionalInformation { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
    }
}
