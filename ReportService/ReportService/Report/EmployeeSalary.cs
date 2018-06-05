using System;
using ReportService.Domain;

namespace ReportService.Report
{
    public class EmployeeSalary : IEquatable<EmployeeSalary>
    {
        public EmployeeSalary(Employee employee, decimal salary)
        {
            Employee = employee;
            Salary = salary;
        }

        public Employee Employee { get; }

        public decimal Salary { get; }

        public bool Equals(EmployeeSalary other)
        {
            return Employee.Equals(other.Employee) &&
                Salary == other.Salary;
        }
    }
}