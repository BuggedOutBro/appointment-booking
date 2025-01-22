using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using appointment_booking.Controllers;
using appointment_booking.Models.DTO;
using appointment_booking.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace appointment_booking.Tests
{
    public class CalendarControllerTests
    {
        [Fact]
        public async Task QueryAvailableSlots_ReturnsOkResult()
        {
            // Arrange
            var mockService = new Mock<ICalendarService>();
            var mockLogger = new Mock<ILogger<CalendarController>>();
            var controller = new CalendarController(mockService.Object, mockLogger.Object);

            var request = new QueryRequest
            {
                Date = DateTime.UtcNow,
                Products = new[] { "SolarPanels" },
                Language = "English",
                Rating = "Gold"
            };

            var slots = new List<CalendarResponse>
            {
                new CalendarResponse { start_date = DateTime.UtcNow, available_count = 5 }
            };

            mockService.Setup(s => s.GetAvailableSlotsAsync(request)).ReturnsAsync(slots);

            // Act
            var result = await controller.QueryAvailableSlots(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(slots, okResult.Value);
        }

        [Fact]
        public async Task QueryAvailableSlots_LogsAppropriateMessages()
        {
            // Arrange
            var mockService = new Mock<ICalendarService>();
            var mockLogger = new Mock<ILogger<CalendarController>>();
            var controller = new CalendarController(mockService.Object, mockLogger.Object);

            var request = new QueryRequest
            {
                Date = DateTime.UtcNow,
                Products = new[] { "SolarPanels" },
                Language = "English",
                Rating = "Gold"
            };

            mockService
                .Setup(service => service.GetAvailableSlotsAsync(request))
                .ReturnsAsync(new List<CalendarResponse>());

            // Act
            await controller.QueryAvailableSlots(request);

            // Assert
            mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => Convert.ToString(v).Contains("Processing request for available slots")),
                    null,
                    It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
                Times.Once);

            mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => Convert.ToString(v).Contains("Successfully retrieved")),
                    null,
                    It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
                Times.Once);
        }
    }
}
