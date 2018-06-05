using Npgsql;

namespace ReportService.Domain
{
    public class Department
    {

        public Department() { }
        public Department(NpgsqlDataReader reader)
        {
            Name = reader.GetString(0);
            Id = reader.GetString(1);
        }

        public string Name { get; private set; }
        public string Id { get; private set; }
    }
}