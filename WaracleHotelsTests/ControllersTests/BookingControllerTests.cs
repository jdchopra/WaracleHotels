using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WaracleHotels.Controllers;
using WaracleHotels.Models;
using WaracleHotels.Models.Requests;
using WaracleHotels.Services;
using Xunit;

namespace WaracleHotelsTests.ControllersTests;
public class BookingControllerTests
{
    [Theory, AutoData]
    public static async Task GivenAValidRequest_WhenGettingTheBooking_ThenTheBookingIsReturned(
       [Frozen] Mock<IBookingService> bookingServiceMock,
       int bookingReference,
       int testHotelId,
       int testRoomId)
    {
        var from = new DateOnly();
        var to = from.AddDays(1);
        var testBooking = new Booking
        {
            HotelId = testHotelId,
            RoomId = testRoomId,
            From = from,
            To = to
        };

        bookingServiceMock.Setup(m => m.GetBookingAsync(bookingReference)).Returns(Task.FromResult(testBooking)!);

        var controller = new BookingController(bookingServiceMock.Object);

        var response = await controller.Get(bookingReference);

        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var actualBooking = ((OkObjectResult)response.Result).Value as Booking;

        Assert.NotNull(actualBooking);
        Assert.Equal(testHotelId, actualBooking.HotelId);
        Assert.Equal(testRoomId, actualBooking.RoomId);
        Assert.Equal(from, actualBooking.From);
        Assert.Equal(to, actualBooking.To);

        bookingServiceMock.Verify(m => m.GetBookingAsync(bookingReference), Times.Once);
    }

    [Theory, AutoData]
    public static async Task GivenABookingThatDoesNotExist_WhenGettingTheBooking_ThenNotFoundIsReturned(
       [Frozen] Mock<IBookingService> bookingServiceMock,
       int bookingReference)
    {
        var controller = new BookingController(bookingServiceMock.Object);

        var response = await controller.Get(bookingReference);

        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.IsType<NotFoundResult>(response.Result);

        bookingServiceMock.Verify(m => m.GetBookingAsync(bookingReference), Times.Once);
    }

    [Theory, AutoData]
    public static async Task GivenAValidBookingRequest_WhenCreated_ThenBookingReferenceIsReturned(
        [Frozen] Mock<IBookingService> bookingServiceMock)
    {
        var expectedBookingReference = 123;

        var bookingRequest = new BookingRequest
        {
            RoomId = 1,
            From = new DateOnly(),
            To = new DateOnly(),
        };

        bookingServiceMock.Setup(m => m.AddBooking(bookingRequest)).Returns(Task.FromResult(expectedBookingReference)!);

        var controller = new BookingController(bookingServiceMock.Object);

        var response = await controller.Post(bookingRequest);
        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var actualBookingReference = (int)((OkObjectResult)response.Result).Value!;
        Assert.Equal(expectedBookingReference, actualBookingReference);

        bookingServiceMock.Verify(m => m.AddBooking(bookingRequest), Times.Once);
    }
}
