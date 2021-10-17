namespace Hostel_System.Dto.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }
        public double PriceForDay { get; set; }
        public int MaxPeopleInRoom { get; set; }
    }
}
