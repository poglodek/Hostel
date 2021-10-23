using System.ComponentModel.DataAnnotations;

namespace Hostel_System.Model
{
    public class RoomModel
    {
        public int Id { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public double PriceForDay { get; set; }
        [Required]
        public int MaxPeopleInRoom { get; set; }
    }
}
