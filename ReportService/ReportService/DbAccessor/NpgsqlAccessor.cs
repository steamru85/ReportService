using System.Data.Common;
using Npgsql;

namespace ReportService.DbAccessor
{
    public class NpgsqlAccessor : IDbAccessor
    {
        private readonly NpgsqlConnection _connection;

        public NpgsqlAccessor(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        public void Connect()
        {
            _connection.Open();
        }

        public void Close()
        {
           _connection.Close();
        }

        public DbDataReader ExecuteCommand(string cmdText)
        {
            var command = new NpgsqlCommand(cmdText, _connection);
            return command.ExecuteReader();
        }
    }
}
