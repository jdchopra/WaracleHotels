using Microsoft.EntityFrameworkCore;
using WaracleHotels.Data.Entities;

namespace WaracleHotels.Data.Context;

public class HotelContext : DbContext
{
    public virtual DbSet<Hotel> Hotels { get; set; } = null!;
    public virtual DbSet<Room> Rooms { get;set; } = null!;
    public virtual DbSet<Booking> Bookings { get; set; } = null!;
}
