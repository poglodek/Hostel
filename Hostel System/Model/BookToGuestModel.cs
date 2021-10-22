using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Model
{
    public class BookToGuestModel
    {
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public double PriceForDay { get; set; }
        public string? AdditionalInformation { get; set; }
        public string RoomName { get; set; }
        public string GuestEmail { get; set; }
    }
}
