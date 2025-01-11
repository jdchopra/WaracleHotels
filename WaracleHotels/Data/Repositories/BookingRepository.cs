using Microsoft.EntityFrameworkCore;
using WaracleHotels.Data.Context;
using WaracleHotels.Data.Entities;

namespace WaracleHotels.Data.Repositories;

public interface IBookingRepository : IRepositoryBase<Booking>
{
    Task<HashSet<int>> GetRoomIdsBookedForRange(int[] roomIds, DateOnly From, DateOnly To);
}

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    private readonly HotelContext _context;

    public BookingRepository(HotelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<HashSet<int>> GetRoomIdsBookedForRange(int[] roomIds, DateOnly from, DateOnly to)
    {
        return await _context.Bookings.Where(b => 
            roomIds.Contains(b.RoomId)
            && (b.To >= from && b.To <= to) // The existing booking ends at some point during the requested dates 
            || (b.From >= from && b.From <= to) // or it starts at some point during the requested dates 
        ).Select(b => b.RoomId).ToHashSetAsync();
    }
}
