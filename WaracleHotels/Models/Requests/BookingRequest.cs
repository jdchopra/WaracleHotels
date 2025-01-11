namespace WaracleHotels.Models.Requests;

public class BookingRequest
{
    public int RoomId { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
}
