using Hostel_System.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Hostel_System.Database
{
    public class HostelSystemDbContext : DbContext
    {
        public HostelSystemDbContext()
        {

        }
        public HostelSystemDbContext(DbContextOptions<HostelSystemDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=sql-server;Database=HostelSystem;User=sa;Password=P@s5Word&;");
            //for local db optionsBuilder.UseSqlServer("Server=.;Database=HostelSystem;Trusted_Connection=True;");
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

    }
}
