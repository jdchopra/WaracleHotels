using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WaracleHotels.Controllers;
using WaracleHotels.Models;
using WaracleHotels.Services;
using Xunit;

namespace WaracleHotelsTests.ControllersTests;

public class HotelControllerTests
{
    [Theory, AutoData]
    public static async Task GivenAValidRequest_WhenGettingTheHotel_ThenTheHotelIsReturned(
        [Frozen] Mock<IHotelService> hotelServiceMock,
        string testHotelName)
    {
        var testHotel = new Hotel
        {
            Name = testHotelName
        };

        hotelServiceMock.Setup(m => m.GetHotelAsync(testHotelName)).Returns(Task.FromResult(testHotel)!);

        var controller = new HotelController(hotelServiceMock.Object);

        var response = await controller.Get(testHotelName);
        
        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var actualHotel = ((OkObjectResult)response.Result).Value as Hotel;

        Assert.NotNull(actualHotel);
        Assert.Equal(testHotelName, actualHotel.Name);

        hotelServiceMock.Verify(m => m.GetHotelAsync(testHotelName), Times.Once);
    }

    [Theory, AutoData]
    public static async Task GivenAHotelThatDoesNotExist_WhenGettingTheHotel_ThenNotFoundIsReturned(
        [Frozen] Mock<IHotelService> hotelServiceMock,
        string testHotelName)
    {
        var controller = new HotelController(hotelServiceMock.Object);

        var response = await controller.Get(testHotelName);

        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.IsType<NotFoundResult>(response.Result);

        hotelServiceMock.Verify(m => m.GetHotelAsync(testHotelName), Times.Once);
    }

    [Theory, AutoData]
    public static async Task GivenAValidHotelName_WhenCreated_ThenCreatedIsReturned(
        [Frozen] Mock<IHotelService> hotelServiceMock,
        string testHotelName)
    {
        var controller = new HotelController(hotelServiceMock.Object);

        var response = await controller.Post(testHotelName);

        Assert.NotNull(response);
        Assert.IsType<CreatedResult>(response);

        hotelServiceMock.Verify(m => m.AddHotel(testHotelName), Times.Once);
    }
}
