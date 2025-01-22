using appointment_booking.Models.DTO;
using appointment_booking.Services;
using appointment_booking.Repositories.Interface;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

public class CalendarServiceTests
{
    [Fact]
    public async Task GetAvailableSlotsAsync_ReturnsExpectedSlots()
    {
        // Arrange
        var mockRepository = new Mock<ICalendarRepository>();

        var queryRequest = new QueryRequest
        {
            Language = "English",
            Products = new[] { "SolarPanels" },
            Rating = "Gold",
            Date = DateTime.UtcNow
        };

        var expectedSlots = new List<CalendarResponse>
        {
            new CalendarResponse { start_date = DateTime.UtcNow, available_count = 5 },
            new CalendarResponse { start_date = DateTime.UtcNow.AddHours(1), available_count = 3 }
        };

        mockRepository
            .Setup(repo => repo.GetAvailableSlotsAsync(queryRequest.Language, queryRequest.Products, queryRequest.Rating, queryRequest.Date))
            .ReturnsAsync(expectedSlots);

        var service = new CalendarService(mockRepository.Object, Mock.Of<ILogger<CalendarService>>());

        // Act
        var result = await service.GetAvailableSlotsAsync(queryRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedSlots.Count, result.Count);
        Assert.Equal(expectedSlots.First().start_date, result.First().start_date);
        Assert.Equal(expectedSlots.First().available_count, result.First().available_count);

        mockRepository.Verify(repo => repo.GetAvailableSlotsAsync(queryRequest.Language, queryRequest.Products, queryRequest.Rating, queryRequest.Date), Times.Once);
    }
}
