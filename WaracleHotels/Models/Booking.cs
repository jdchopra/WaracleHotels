namespace WaracleHotels.Models;

public class Booking
{
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
}
