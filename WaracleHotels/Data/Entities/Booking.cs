using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleHotels.Data.Entities;

[Table("Booking")]
public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
}
