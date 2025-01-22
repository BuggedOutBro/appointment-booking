using appointment_booking.Attributes;
using appointment_booking.Models.DTO;
using appointment_booking.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace appointment_booking.Controllers;
//we can add authorization here, as it was not in scpoe of the task, I have not added it
[ApiController]
[Route("calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;
    private readonly ILogger<CalendarController> _logger;

    public CalendarController(ICalendarService calendarService, ILogger<CalendarController> logger)
    {
        _calendarService = calendarService ?? throw new ArgumentNullException(nameof(calendarService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("query")]
    [ValidateModel]
    public async Task<IActionResult> QueryAvailableSlots([FromBody] QueryRequest request)
    {
        _logger.LogInformation("Processing request for available slots on {Date} with products: {Products}, language: {Language}, rating: {Rating}.",
         request.Date, string.Join(", ", request.Products), request.Language, request.Rating);

        var slots = await _calendarService.GetAvailableSlotsAsync(request);

        _logger.LogInformation("Successfully retrieved {Count} available slots.", slots.Count);
        return Ok(slots);
    }
}