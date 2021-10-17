namespace Hostel_System.Model
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }
        public double PriceForDay { get; set; }
        public int MaxPeopleInRoom { get; set; }
    }
}
