using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace CORE
{

    public class Database : IDisposable
    {
        protected readonly DbConnection DbConnection;

        public Database()
        {
            DbConnection = new Npgsql.NpgsqlConnection(ConfigurationManager.GetConnectionString(ConfigurationManager.Get("ConstrName")));

            if (DbConnection != null)
                DbConnection.ConnectionString = ConfigurationManager.GetConnectionString(ConfigurationManager.Get("ConstrName"));
            else
                throw new Exception("Error while creating connetion to DB");
        }

        public void Dispose()
        {
            DbConnection.Dispose();
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            return _Query<T>(sql, param).Result;
        }

        public IEnumerable<T> Query<T>(string sql)
        {
            return _Query<T>(sql).Result;
        }

        public int Execute(string sql, object param)
        {
            return _Execute(sql, param).Result;
        }

        public int Execute(string sql)
        {
            return _Execute(sql).Result;
        }

        private async Task<IEnumerable<T>> _Query<T>(string sql, object param)
        {
            IEnumerable<T> items;

            using (DbConnection)
            {
                await DbConnection.OpenAsync();

                items = SqlMapper.Query<T>(DbConnection, sql, param);

                DbConnection.Close();
            }

            return items;
        }

        private async Task<IEnumerable<T>> _Query<T>(string sql)
        {
            IEnumerable<T> items;

            using (DbConnection)
            {
                await DbConnection.OpenAsync();

                items = SqlMapper.QueryAsync<T>(DbConnection, sql).Result;

                DbConnection.Close();
            }

            return items;
        }

        private async Task<int> _Execute(string sql, object param)
        {
            int result;

            using (DbConnection)
            {
                await DbConnection.OpenAsync();

                result = SqlMapper.Execute(DbConnection, sql, param);

                DbConnection.Close();
            }

            return result;
        }

        private async Task<int> _Execute(string sql)
        {
            int result;

            using (DbConnection)
            {
                await DbConnection.OpenAsync();

                result = SqlMapper.Execute(DbConnection, sql);

                DbConnection.Close();
            }

            return result;
        }

        public async Task<int> ExecuteAsync(string sql, object param)
        {
            Task<int> result;

            using (DbConnection)
            {
                await DbConnection.OpenAsync();

                result = SqlMapper.ExecuteAsync(DbConnection, sql, param);

                DbConnection.Close();
            }

            return result.Result;
        }
    }
}
