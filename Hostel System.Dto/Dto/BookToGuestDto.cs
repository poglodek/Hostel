namespace Hostel_System.Dto.Dto
{
    public class BookToGuestDto
    {
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public double PriceForDay { get; set; }
        public string? AdditionalInformation { get; set; }
        public string RoomName { get; set; }
        public string GuestEmail { get; set; }
    }
}
