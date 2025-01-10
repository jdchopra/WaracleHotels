namespace WaracleHotels.Data.Entities;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateOnly Date { get; set; }
    public required string Reference { get; set; }
}
