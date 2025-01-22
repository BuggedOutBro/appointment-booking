using appointment_booking.Models.DTO;
using appointment_booking.Repositories.Interface;
using appointment_booking.Services.Interface;

namespace appointment_booking.Services;
public class CalendarService : ICalendarService
{
    private readonly ICalendarRepository _calendarRepository;
    private readonly ILogger<CalendarService> _logger;

    public CalendarService(
        ICalendarRepository calendarRepository,
        ILogger<CalendarService> logger)
    {
        _calendarRepository = calendarRepository;
        _logger = logger;
    }

    public async Task<List<CalendarResponse>> GetAvailableSlotsAsync(QueryRequest request)
    {
        return await _calendarRepository.GetAvailableSlotsAsync(
            request.Language,
            request.Products,
            request.Rating,
            request.Date);
    }
}
