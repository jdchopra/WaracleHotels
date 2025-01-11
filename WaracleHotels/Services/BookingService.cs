using WaracleHotels.Data.Repositories;
using WaracleHotels.Mapping;
using WaracleHotels.Models;
using WaracleHotels.Models.Requests;

namespace WaracleHotels.Services;

public interface IBookingService
{
    Task<int> AddBooking(BookingRequest bookingRequest);
    Task<Booking?> GetBookingAsync(int id);
}

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    public async Task<int> AddBooking(BookingRequest bookingRequest)
    {
        var booking = new Data.Entities.Booking
        {
            RoomId = bookingRequest.RoomId,
            From = bookingRequest.From,
            To = bookingRequest.To
        };

        var createdBooking = await _bookingRepository.CreateAsync(booking);

        return createdBooking!.Id;
    }

    public async Task<Booking?> GetBookingAsync(int id)
    {
        var bookingEntity = await _bookingRepository.ReadAsync(id);

        if (bookingEntity == null)
            return null;

        return bookingEntity.MapToModel();
    }
}
