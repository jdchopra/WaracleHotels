using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleHotels.Data.Entities;

[Table("Room")]
public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public RoomType RoomType { get; set; }
    public int Capacity { get; set; }
}
