using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Dto.Dto
{
    public class RoomReservationDto
    {
        public int Id { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }

        public double PriceForDay { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
