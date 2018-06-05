namespace ReportService.Services
{
    public interface IBookkeepingDepartment
    {
        decimal GetSalary(string inn, string employeeCode);
    }
}