using System.Collections.Generic;
using System.Linq;

namespace ReportService.Report
{
    public class EmployeeSalaryReport
    {
        public IEnumerable<DepartmentReportItem> Departments { get; }
        public int Year { get; }
        public string Month { get; }

        public EmployeeSalaryReport(int year, int month, IEnumerable<DepartmentReportItem> departmentReportItems)
        {
            Year = year;
            Month = Common.MonthNameResolver.GetName(year, month);
            Departments = departmentReportItems;
        }

        public decimal TotalAmount => Departments?.Sum(i => i.DepartmentAmount) ?? decimal.Zero;

        
    }
}
