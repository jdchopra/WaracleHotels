namespace WaracleHotels.Models.Requests;

public class CreateRoomRequest
{
    public int HotelId { get; set; }
    public RoomType RoomType { get; set; }
    public int Capacity { get; set; }
}
