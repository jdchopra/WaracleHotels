namespace WaracleHotels.Data.Entities;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
}
