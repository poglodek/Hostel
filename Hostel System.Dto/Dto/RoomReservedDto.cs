using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hostel_System.Database.Entity;

namespace Hostel_System.Dto.Dto
{
    public class RoomReservedDto
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
