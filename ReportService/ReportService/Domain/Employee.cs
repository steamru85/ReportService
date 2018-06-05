using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace ReportService.Domain
{
    public class Employee
    {
        public Employee() { }

        public Employee(NpgsqlDataReader reader)
        {
            Name = reader.GetString(0);
            Inn = reader.GetString(1); Department = reader.GetString(2);
        }

        public string Name { get; set; }
        public string Department { get; set; }
        public string Inn { get; set; }
        public int Salary { get; set; }
        public string BuhCode { get; set; }
    }
}
