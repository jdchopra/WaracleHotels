using Microsoft.EntityFrameworkCore;
using WaracleHotels.Data.Context;
using WaracleHotels.Data.Entities;

namespace WaracleHotels.Data.Repositories;

public interface IRoomRepository : IRepositoryBase<Room>
{
    Task<Room[]> GetAllRoomsForCapacity(int capacity);
}

public class RoomRepository : RepositoryBase<Room>, IRoomRepository
{
    private readonly HotelContext _context;

    public RoomRepository(HotelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Room[]> GetAllRoomsForCapacity(int capacity)
    {
        return await _context.Rooms.Where(r => r.Capacity == capacity).ToArrayAsync();
    }
}
