using GarageMVC.Models.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace GarageMVC.Data
{
    public class GarageMVCContext : DbContext
    {
        private readonly string? _connectionString;

        public GarageMVCContext(DbContextOptions<GarageMVCContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasDiscriminator<string>("VehicleType")
                .HasValue<Car>(nameof(Car))
                .HasValue<Bus>(nameof(Bus))
                .HasValue<Boat>(nameof(Boat))
                .HasValue<Motorcycle>(nameof(Motorcycle))
                .HasValue<Airplane>(nameof(Airplane));

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }

}
