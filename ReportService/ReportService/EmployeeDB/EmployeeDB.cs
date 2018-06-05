using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ReportService.Domain;

namespace ReportService.EmployeeDB
{
    public class EmpDB : IEmployeeDB
    {
        private readonly string connectionString;

        public EmpDB(IConfiguration config)
        {
            connectionString = config.GetValue<string>("employeeConnectionString");
        }

        public IEnumerable<Department> GetDepartments()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand("SELECT d.name from deps d where d.active = true", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Department(reader);
                }
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using(var conn1 = new NpgsqlConnection(connectionString))
            {
                conn1.Open();
                var cmd1 = new NpgsqlCommand("SELECT e.name, e.inn, d.name from emps e left join deps d on e.departmentid = d.id", conn1);
                var reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    yield return new Employee() { Name = reader1.GetString(0), Inn = reader1.GetString(1), Department = reader1.GetString(2) };
                }
            }
        }
    }
}