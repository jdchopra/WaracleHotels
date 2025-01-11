using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WaracleHotels.Models;
using WaracleHotels.Models.Requests;
using WaracleHotels.Services;
using WaracleRooms.Controllers;
using Xunit;

namespace WaracleHotelsTests.ControllersTests;
public class RoomControllerTests
{
    [Theory, AutoData]
    public static async Task GivenAValidAvailableRoomsRequestRequest_WhenCalled_ThenAvailableRoomsAreReturned(
        [Frozen] Mock<IRoomService> roomServiceMock,
        Room[] expectedAvailableRooms)
    {
        var availableRoomsRequest = new AvailableRoomsRequest
        {
            NumberOfGuests = 2,
            From = new DateOnly(),
            To = new DateOnly()
        };

        roomServiceMock.Setup(m => m.FindAvailableRooms(availableRoomsRequest)).Returns(Task.FromResult(expectedAvailableRooms)!);

        var controller = new RoomController(roomServiceMock.Object);

        var response = await controller.QueryAvailableRooms(availableRoomsRequest);
        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var actualBookingReference = ((OkObjectResult)response.Result).Value! as Room[];
        Assert.Equal(expectedAvailableRooms, actualBookingReference);

        roomServiceMock.Verify(m => m.FindAvailableRooms(availableRoomsRequest), Times.Once);
    }

    [Theory, AutoData]
    public static async Task GivenAValidHotelName_WhenCreated_ThenCreatedIsReturned(
        [Frozen] Mock<IRoomService> roomServiceMock,
        CreateRoomRequest createRoomRequest)
    {
        var controller = new RoomController(roomServiceMock.Object);

        var response = await controller.Post(createRoomRequest);

        Assert.NotNull(response);
        Assert.IsType<CreatedResult>(response);

        roomServiceMock.Verify(m => m.AddRoom(createRoomRequest), Times.Once);
    }
}
