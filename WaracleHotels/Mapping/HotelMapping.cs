namespace WaracleHotels.Mapping;

public static class HotelMapping
{
   public static Data.Entities.Hotel MapToEntity(this Models.Hotel hotel)
    {
        return new Data.Entities.Hotel
        {
            Name = hotel.Name
        };
    }

    public static Models.Hotel MapToModel(this Data.Entities.Hotel hotel)
    {
        return new Models.Hotel
        {
            Name = hotel.Name,
            Id = hotel.Id
        };
    }
}
