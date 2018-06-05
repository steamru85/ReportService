using System.Collections.Generic;

namespace ReportService.Domain
{
    public class Department
    {
        public Department(string name, bool active, IEnumerable<Employee> employees)
        {
            Name = name;
            Active = active;
            Employees = employees;
        }

        public string Name { get; }

        public bool Active { get; }

        public IEnumerable<Employee> Employees { get; }
    }
}
