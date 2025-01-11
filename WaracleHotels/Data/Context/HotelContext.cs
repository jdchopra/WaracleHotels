using Microsoft.EntityFrameworkCore;
using WaracleHotels.Data.Entities;

namespace WaracleHotels.Data.Context;

public class HotelContext : DbContext
{
    public virtual DbSet<Hotel> Hotels { get; set; } = null!;
    public virtual DbSet<Room> Rooms { get;set; } = null!;
    public virtual DbSet<Booking> Bookings { get; set; } = null!;

    public string DbPath { get; }

    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "hotel.db");

        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
