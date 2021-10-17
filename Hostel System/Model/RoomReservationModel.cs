using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hostel_System.Database.Entity;

namespace Hostel_System.Model
{
    public class RoomReservationModel
    {
        public int Id { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }

        public double PriceForDay { get; set; }
        public string AdditionalInformation { get; set; }
        public double TotalCost { get; set; }
    }
}
