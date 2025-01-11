using Microsoft.EntityFrameworkCore;
using WaracleHotels.Data.Context;
using WaracleHotels.Data.Entities;

namespace WaracleHotels.Data.Repositories;

public interface IHotelRepository : IRepositoryBase<Hotel>
{
    Task<Hotel?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
{
    private readonly HotelContext _context;

    public HotelRepository(HotelContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Hotel?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Hotels.Where(h => h.Name == name).SingleOrDefaultAsync();
    }
}
