using System.ComponentModel.DataAnnotations.Schema;

namespace WaracleHotels.Data.Entities;

[Table("Hotel")]
public class Hotel
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
