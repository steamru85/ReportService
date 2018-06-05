using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportService.Report
{
    public class DepartmentReportItem : IEquatable<DepartmentReportItem>
    {
        public DepartmentReportItem(string department, IEnumerable<EmployeeSalary> employees)
        {
            Department = department;
            Employees = employees;
        }

        public string Department { get; }
        public IEnumerable<EmployeeSalary> Employees { get; }
        public decimal DepartmentAmount => Employees.Sum(i => i.Salary);

        public bool Equals(DepartmentReportItem other)
        {
            return string.Equals(Department, other.Department) &&
                   DepartmentAmount == other.DepartmentAmount &&
                   Employees.SequenceEqual(other.Employees);
        }
    }
}