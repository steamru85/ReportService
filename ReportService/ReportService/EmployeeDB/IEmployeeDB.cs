using System.Collections.Generic;
using Npgsql;
using ReportService.Domain;

namespace ReportService.EmployeeDB{
    public interface IEmployeeDB
    {
        IEnumerable<Department> GetDepartments();
        IEnumerable<Employee> GetEmployees();
    }

    public class Department
    {
        
        public Department(){}
        public Department(NpgsqlDataReader reader)
        {
            Name=reader.GetString(0);
        }

        public string Name { get; private set; }
    }
}