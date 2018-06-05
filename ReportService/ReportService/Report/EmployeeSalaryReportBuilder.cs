using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReportService.Domain;
using ReportService.Services;

namespace ReportService.Report
{
    public class EmployeeSalaryReportBuilder : IEmployeeSalaryReportBuilder
    {
        private readonly IBookkeepingDepartment _buhDepartment;
        private readonly IHumanResourcesDepartment _hrDepartment;
        private readonly string _template;
        
        public EmployeeSalaryReport ReportData { get; }

        public EmployeeSalaryReportBuilder(IBookkeepingDepartment buhDepartment, IHumanResourcesDepartment hrDepartment, string template)
        {
            _buhDepartment = buhDepartment;
            _hrDepartment = hrDepartment;
            _template = template;
        }

        private EmployeeSalaryReportBuilder(IBookkeepingDepartment buhDepartment, IHumanResourcesDepartment hrDepartment, 
            string template, EmployeeSalaryReport reportData)
        {
            _buhDepartment = buhDepartment;
            _hrDepartment = hrDepartment;
            _template = template;
            ReportData = reportData;
        }

        public async Task<IEmployeeSalaryReportBuilder> BuildData(int year, int month, IEnumerable<Employee> employees)
        {
            EmployeeSalary[] employeesSalary = await Task.WhenAll(employees.Select(async i => await GetSalary(i)));
            List<DepartmentReportItem> departments = employeesSalary
                .GroupBy(i => i.Employee.Department)
                .Select(i => new DepartmentReportItem(i.Key, i.ToList()))
                .ToList();

            var reportData = new EmployeeSalaryReport(year, month, departments);
            return new EmployeeSalaryReportBuilder(_buhDepartment, _hrDepartment, _template, reportData);
        }

        public string GenerateReport()
        {
            if (ReportData == null)
            {
                throw new NullReferenceException("run BuildData before");
            }

            string report = new Mustache.FormatCompiler().Compile(_template).Render(ReportData);
            return report;

        }

        private async Task<EmployeeSalary> GetSalary(Employee employee)
        {
            string buhCode = await _hrDepartment.GetAccountCode(employee.Inn);
            decimal salary = _buhDepartment.GetSalary(employee.Inn, buhCode);
            return new EmployeeSalary(employee, salary);
        }
    }
}
