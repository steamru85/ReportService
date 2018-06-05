using System.Collections.Generic;
using Npgsql;
using ReportService.Domain;

namespace ReportService.EmployeeDB{
    public interface IEmployeeDB
    {
        IEnumerable<Department> GetDepartments();
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesFromDepartment(Department department);
    }   
}