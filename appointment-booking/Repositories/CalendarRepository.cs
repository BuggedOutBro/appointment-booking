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

            // SQL query
            string query = @"
            SELECT 
                sl.start_date AS start_date,
                COUNT(DISTINCT sm.id) AS available_count
            FROM slots sl
            JOIN sales_managers sm 
            ON sm.id = sl.sales_manager_id
            WHERE 
                sl.booked = FALSE
                AND NOT EXISTS (
                    SELECT 1
                    FROM slots s2
                    WHERE 
                        s2.sales_manager_id = sl.sales_manager_id 
                        AND s2.booked = TRUE
                        AND s2.start_date < sl.end_date 
                        AND s2.end_date > sl.start_date
                )
                AND sm.languages @> ARRAY[@Language]::varchar[]
                AND sm.customer_ratings @> ARRAY[@Rating]::varchar[]
                AND sm.products @> ARRAY[@Products]::varchar[]
                AND sl.start_date >= @UtcDate
                AND sl.start_date < @NextUtcDate
            GROUP BY sl.start_date
            ORDER BY sl.start_date";

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
            var results = await _queryExecutor.QueryAsync<CalendarResponse>(query, parameters);
            return results.ToList();
        }

    }


}
