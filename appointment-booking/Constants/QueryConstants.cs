public static class QueryConstants
{
    public const string availableSlotsQuery = @"
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
}