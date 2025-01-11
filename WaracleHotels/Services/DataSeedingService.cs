using WaracleHotels.Data.Entities;
using WaracleHotels.Data.Repositories;

namespace WaracleHotels.Services;

public interface IDataSeedingService
{
    Task Initialize();
    Task TearDown();
}


public class DataSeedingService : IDataSeedingService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly Random _random = new();
    private const int HotelsToSeed = 3;
    private const int RoomsPerHotel = 6;
    private const int BookingsToSeed = 4;


    public DataSeedingService(IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IBookingRepository bookingRepository)
    {
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task Initialize()
    {
        var hotels = GenerateHotels();
        await _hotelRepository.BulkCreateAsync(hotels);

        var hotelIds = hotels.Select(h => h.Id);
        var rooms = GenerateRooms(hotelIds);
        await _roomRepository.BulkCreateAsync(rooms);

        var roomIds = rooms.Select(r  => r.Id);
        var bookings = GenerateBookings(roomIds);
        await _bookingRepository.BulkCreateAsync(bookings);
    }

    public async Task TearDown()
    {
        await _hotelRepository.DeleteAllAsync();
        await _roomRepository.DeleteAllAsync();
        await _bookingRepository.DeleteAllAsync();
    }

    private List<Hotel> GenerateHotels()
    {
        var hotels = new List<Hotel>();

        for (int i = 0; i < HotelsToSeed; i++)
        {
            var hotel = new Hotel
            {
                Name = $"Test hotel {i + 1}",
            };

            hotels.Add(hotel);
        }

        return hotels;
    }

    private List<Room> GenerateRooms(IEnumerable<int> hotelIds)
    {
        var rooms = new List<Room>();
        var roomId = 0;
        var roomTypes = new [] { RoomType.Single, RoomType.Double, RoomType.Deluxe };

        foreach (var hotelId in hotelIds) 
        {
            for (int i = 0; i < RoomsPerHotel; i++)
            {
                var room =  new Room
                {
                    HotelId = hotelId,
                    Capacity = _random.Next(5),
                    RoomType = roomTypes[i % roomTypes.Length]
                };

                rooms.Add(room);
                roomId++;
            }
        }

        return rooms;
    }

    private List<Booking> GenerateBookings(IEnumerable<int> roomIds)
    {
        var bookings = new List<Booking>();
        var bookingDate = new DateOnly(2025, 01, 01);
        
        for (int i = 0; i < BookingsToSeed; i++)
        {
            var booking = new Booking
            {
                RoomId = roomIds.Take(1).Single(),
                From = bookingDate,
                To = bookingDate.AddDays(_random.Next(7)),
            };

            bookings.Add(booking);
        }

        return bookings;
    }
}