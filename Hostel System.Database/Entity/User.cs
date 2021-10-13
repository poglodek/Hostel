using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Database.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsMale { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string? CarRegistrationNumber { get; set; }
        public Role RoleName { get; set; }
        public List<Reservation> Reservation { get; set; }

    }
}
