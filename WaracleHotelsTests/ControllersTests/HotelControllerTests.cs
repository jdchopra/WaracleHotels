using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WaracleHotels.Controllers;
using WaracleHotels.Models;
using Xunit;

namespace WaracleHotelsTests.ControllersTests;

public class HotelControllerTests
{
    [Theory, AutoData]
    public static async Task GivenAValidRequest_WhenGettingTheHotel_ThenTheHotelIsReturned(
        [Frozen] Mock<ILogger<HotelController>> loggerMock,
        string testHotelName)
    {
        var controller = new HotelController(loggerMock.Object);

        var response = await controller.Get(testHotelName);
        
        Assert.NotNull(response);
        Assert.NotNull(response.Result);

        var actualHotel = ((OkObjectResult)response.Result).Value as Hotel;

        Assert.NotNull(actualHotel);
        Assert.Equal(testHotelName, actualHotel.Name);
    }
}
