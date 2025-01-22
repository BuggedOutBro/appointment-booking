using appointment_booking.Models.DTO;

namespace appointment_booking.Services.Interface;
public interface ICalendarService
{
    Task<List<CalendarResponse>> GetAvailableSlotsAsync(QueryRequest request);
}
