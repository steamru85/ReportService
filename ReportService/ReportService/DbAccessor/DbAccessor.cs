using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

namespace ReportService.DbAccessor
{
    public class DbAccessor : IDbAccessor
    {
        private readonly DbConnection _connection;

        public DbAccessor(DbConnection connection)
        {
            _connection = connection;
        }

        public void Connect()
        {
            _connection.Open();
        }

        public void Close()
        {
           _connection.Close();
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string cmdText)
        {
            return await _connection.QueryAsync<T>(cmdText);
        }
    }
}
