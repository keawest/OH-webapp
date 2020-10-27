using Microsoft.EntityFrameworkCore;
using OptimalHealthApp.Models;

namespace OptimalHealthApp.Data
{
    public class OptimalHealthAppContext : DbContext
    {
        public OptimalHealthAppContext (DbContextOptions<OptimalHealthAppContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Appointment>()
                .HasKey(t => new
                    {
                    t.D_ID,
                    t.U_ID
                     });
        }
        public DbSet<OptimalHealthApp.Models.County> County { get; set; }
        public DbSet<OptimalHealthApp.Models.City> City { get; set; }
        public DbSet<OptimalHealthApp.Models.Health_center> Health_center { get; set; }
        public DbSet<OptimalHealthApp.Models.Doctor> Doctor { get; set; }
        public DbSet<OptimalHealthApp.Models.UserTable> UserTable { get; set; }
        public DbSet<OptimalHealthApp.Models.Appointment> Appointment { get; set; }
    }
}
