using System.Data.Common;
using Npgsql;

namespace ReportService.Domain
{
    public class Department
    {

        public Department() { }
        public Department(DbDataReader reader)
        {
            Name = reader.GetString(0);
            Id = reader.GetString(1);
        }
        public int Salary{get;set;}

        public string Name { get;  set; }
        public string Id { get;  set; }
    }
}