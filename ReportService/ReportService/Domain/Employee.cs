using System;

namespace ReportService.Domain
{
    public class Employee : IEquatable<Employee>
    {
        public Employee(string name, string inn, string department)
        {
            Name = name;
            Inn = inn;
            Department = department;
        }

        public string Name { get; }
        public string Inn { get; }
        public string Department { get; }

        public bool Equals(Employee other)
        {
            return string.Equals(Name, other.Name) &&
                   string.Equals(Inn, other.Inn) &&
                   string.Equals(Department, other.Department);
        }
    }
}
