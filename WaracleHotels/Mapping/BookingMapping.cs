namespace WaracleHotels.Mapping;

public static class BookingMapping
{
    public static Data.Entities.Booking MapToEntity(this Models.Booking booking)
    {
        return new Data.Entities.Booking
        {
            From = booking.From,
            To = booking.To,
            RoomId = booking.RoomId
        };
    }

    public static Models.Booking MapToModel(this Data.Entities.Booking booking)
    {
        return new Models.Booking
        {
            From = booking.From,
            To = booking.To,
            RoomId = booking.RoomId
        };
    }
}
