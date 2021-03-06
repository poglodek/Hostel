namespace Hostel_System.Database.Entity
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public Room BookingRoom { get; set; }
        public User BookingUser { get; set; }
        public string? AdditionalInformation { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; } //booked, actual, completed, canceled

    }
}
