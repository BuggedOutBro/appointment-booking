using System;
using System.Data;
using appointment_booking.DBExecuter.Interface;
using Dapper;

namespace appointment_booking.DBExecuter;

public class DapperQueryExecutor : IDbQueryExecutor
{
    private readonly IDbConnection _connection;

    public DapperQueryExecutor(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
    {
        return await _connection.QueryAsync<T>(sql, parameters);
    }
}

