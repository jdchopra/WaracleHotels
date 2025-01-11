using WaracleHotels.Models;

namespace WaracleHotels.Mapping;

public static class RoomMapping
{
    public static Data.Entities.Room MapToEntity(this Room room)
    {
        return new Data.Entities.Room
        {
            Id = room.Id,
            HotelId = room.HotelId,
            Capacity = room.Capacity,
            RoomType = MapToEntity(room.RoomType)
        };
    }

    public static Room MapToModel(this Data.Entities.Room room)
    {
        return new Room
        {
            Id = room.Id,
            HotelId = room.HotelId,
            Capacity = room.Capacity,
            RoomType = MapToModel(room.RoomType)
        };
    }

    public static Data.Entities.RoomType MapToEntity(this RoomType roomType)
    {
        return roomType switch
        {
            RoomType.Single => Data.Entities.RoomType.Single,
            RoomType.Double => Data.Entities.RoomType.Double,
            RoomType.Deluxe => Data.Entities.RoomType.Deluxe,
            _ => throw new NotImplementedException()
        };
    }

    public static RoomType MapToModel(this Data.Entities.RoomType roomType)
    {
        return roomType switch
        {
            Data.Entities.RoomType.Single => RoomType.Single,
            Data.Entities.RoomType.Double => RoomType.Double,
            Data.Entities.RoomType.Deluxe => RoomType.Deluxe,
            _ => throw new NotImplementedException()
        };
    }
}
