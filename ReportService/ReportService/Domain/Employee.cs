namespace ReportService.Domain
{
    public class Employee
    {
        public Employee(string name, string department, string inn, decimal salary, string buhCode)
        {
            Name = name;
            Department = department;
            Inn = inn;
            Salary = salary;
            BuhCode = buhCode;
        }

        public string Name { get; }
        public string Department { get; }
        public string  Inn { get; }
        public decimal Salary { get; }
        public string BuhCode { get; }
    }
}
