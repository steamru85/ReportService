using System.Collections.Generic;
using System.Threading.Tasks;
using ReportService.Domain;

namespace ReportService.Report
{
    public interface IEmployeeSalaryReportBuilder
    {
        EmployeeSalaryReport ReportData { get; }
        Task<IEmployeeSalaryReportBuilder> BuildData(int year, int month, IEnumerable<Employee> employees);
        string GenerateReport();
    }
}