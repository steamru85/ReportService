using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;
using ReportService.Domain;

namespace ReportService.EmployeeDB{
    public interface IEmployeeDB
    {
        IEnumerable<Department> GetDepartments();        
        IEnumerable<Employee> GetEmployeesFromDepartment(Department department);
    }   
}