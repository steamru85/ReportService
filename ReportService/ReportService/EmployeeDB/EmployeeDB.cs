using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using ReportService.Domain;

namespace ReportService.EmployeeDB
{
    public class EmpDB : IEmployeeDB,IDisposable
    {
        private readonly string connectionString;

        public EmpDB(IConfiguration config)
        {
            connectionString = config.GetValue<string>("employeeConnectionString");
        }
        NpgsqlConnection _connection;
        NpgsqlConnection Connection{
            get{
                if(_connection==null){
                    _connection=new NpgsqlConnection(connectionString);
                    _connection.Open();
                }
                return _connection;
            }
        }
        public IEnumerable<Department> GetDepartments()
        {
            using(var cmd = new NpgsqlCommand("SELECT d.name ,d.id from deps d where d.active = true", Connection))
            {
                using(var reader = cmd.ExecuteReader())                    
                    while (reader.Read())
                        yield return new Department(reader);
            }            
        }

       
        public IEnumerable<Employee> GetEmployeesFromDepartment(Department department)
        {
            var cmd = new NpgsqlCommand("SELECT e.name, e.inn, d.name from emps e left join deps d on e.departmentid = d.id where d.id=:depId", Connection);
            NpgsqlParameter par=new NpgsqlParameter("depId",NpgsqlDbType.Varchar);//к сожалению тип столбца в таблице не известен, но вставлять значение напрямую в SQL- запрос плохая идея.
            par.Direction=ParameterDirection.Input;
            cmd.Parameters.Add(cmd);
            par.Value=department.Id;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return new Employee(reader);
            }

        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection?.Dispose();
                }
                disposedValue = true;
            }
        }       

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}