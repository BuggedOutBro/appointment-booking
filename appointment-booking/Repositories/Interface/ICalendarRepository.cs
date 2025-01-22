using appointment_booking.Models.DTO;

namespace appointment_booking.Repositories.Interface;

public interface ICalendarRepository
{
    Task<List<CalendarResponse>> GetAvailableSlotsAsync(string language, string[] products, string rating, DateTime date);
}
