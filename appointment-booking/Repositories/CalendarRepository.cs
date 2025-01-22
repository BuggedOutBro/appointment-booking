using appointment_booking.DBExecuter.Interface;
using appointment_booking.Models.DTO;
using appointment_booking.Repositories.Interface;

namespace appointment_booking.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly IDbQueryExecutor _queryExecutor;

        public CalendarRepository(IDbQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public async Task<List<CalendarResponse>> GetAvailableSlotsAsync(string language, string[] products, string rating, DateTime date)
        {
            // Convert the date to UTC
            var utcDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
            var nextUtcDate = DateTime.SpecifyKind(date.Date.AddDays(1), DateTimeKind.Utc);

            // Query parameters
            var parameters = new
            {
                Language = language,
                Rating = rating,
                Products = products,
                UtcDate = utcDate,
                NextUtcDate = nextUtcDate
            };

            // Execute query using Dapper
            var results = await _queryExecutor.QueryAsync<CalendarResponse>(QueryConstants.availableSlotsQuery, parameters);
            return results.ToList();
        }

    }


}
