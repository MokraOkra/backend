using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base (options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<DoctorData> DoctorsData { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Address> Addresses { get; set; }
}