using WaracleHotels.Data.Repositories;
using WaracleHotels.Mapping;

namespace WaracleHotels.Services;

public interface IHotelService
{
    Task AddHotel(string hotelName);
    Task<Models.Hotel?> GetHotelAsync(string name);
}

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }
    public async Task AddHotel(string hotelName)
    {
        var hotelEntity = new Data.Entities.Hotel
        { 
            Name = hotelName
        };

        await _hotelRepository.CreateAsync(hotelEntity);
    }

    public async Task<Models.Hotel?> GetHotelAsync(string name)
    {
        var hotelEntity = await _hotelRepository.GetByNameAsync(name);

        if (hotelEntity == null)
            return null;

        return hotelEntity.MapToModel();
    }
}
