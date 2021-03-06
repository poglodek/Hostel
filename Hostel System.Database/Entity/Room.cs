using System.ComponentModel.DataAnnotations;

namespace Hostel_System.Database.Entity
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public int Phone { get; set; }
        public double PriceForDay { get; set; }
        public int MaxPeopleInRoom { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
