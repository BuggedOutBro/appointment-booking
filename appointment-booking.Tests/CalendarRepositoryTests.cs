using Dapper;
using Moq;
using System.Data;
using appointment_booking.Models.DTO;
using appointment_booking.Repositories;
using appointment_booking.DBExecuter.Interface;

public class CalendarRepositoryTests
{
    [Fact]
    public async Task GetAvailableSlotsAsync_ReturnsCorrectData()
    {
        // Arrange
        var mockQueryExecutor = new Mock<IDbQueryExecutor>();

        var language = "English";
        var products = new[] { "HeatPumps" };
        var rating = "Silver";
        var date = DateTime.UtcNow;

        var expectedResult = new List<CalendarResponse>
    {
        new CalendarResponse { start_date = date, available_count = 3 },
        new CalendarResponse { start_date = date.AddHours(1), available_count = 2 }
    };

        mockQueryExecutor
            .Setup(q => q.QueryAsync<CalendarResponse>(
                It.IsAny<string>(),
                It.IsAny<object>()
            ))
            .ReturnsAsync(expectedResult);

        var repository = new CalendarRepository(mockQueryExecutor.Object);

        // Act
        var result = await repository.GetAvailableSlotsAsync(language, products, rating, date);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult.Count, result.Count);
        Assert.Equal(expectedResult, result);

        mockQueryExecutor.Verify(q => q.QueryAsync<CalendarResponse>(
            It.IsAny<string>(),
            It.IsAny<object>()
        ), Times.Once);
    }
}
