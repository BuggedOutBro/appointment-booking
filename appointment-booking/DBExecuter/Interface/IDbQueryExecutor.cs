using System;

namespace appointment_booking.DBExecuter.Interface;

public interface IDbQueryExecutor
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
}
