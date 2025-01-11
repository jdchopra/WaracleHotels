using WaracleHotels.Data.Repositories;
using WaracleHotels.Mapping;
using WaracleHotels.Models;
using WaracleHotels.Models.Requests;

namespace WaracleHotels.Services;

public interface IRoomService
{
    Task AddRoom(CreateRoomRequest createRoomRequest);
    Task<Room[]> FindAvailableRooms(AvailableRoomsRequest availableRoomsRequest);
}

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;

    public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
    }
    public async Task AddRoom(CreateRoomRequest createRoomRequest)
    {
        var room = new Data.Entities.Room
        {
            HotelId = createRoomRequest.HotelId,
            Capacity = createRoomRequest.Capacity,
            RoomType = createRoomRequest.RoomType.MapToEntity()
        };

        await _roomRepository.CreateAsync(room);
    }

    public async Task<Room[]> FindAvailableRooms(AvailableRoomsRequest availableRoomsRequest)
    {
        var rooms = await _roomRepository.GetAllRoomsForCapacity(availableRoomsRequest.NumberOfGuests);
        if (rooms.Length == 0)
            return [];

        var roomIds = rooms.Select(r => r.Id).ToArray();
        var bookedRoomIds = await _bookingRepository.GetRoomIdsBookedForRange(roomIds, availableRoomsRequest.From, availableRoomsRequest.To);

        var availableRoomIds = roomIds.Where(id => !bookedRoomIds.Contains(id)).ToHashSet();


        return rooms.Where(r => availableRoomIds.Contains(r.Id))
            .Select(r => r.MapToModel()).ToArray();
    }
}
