namespace WaracleHotels.Models.Requests;

public class AvailableRoomsRequest
{
    public int NumberOfGuests { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
}
