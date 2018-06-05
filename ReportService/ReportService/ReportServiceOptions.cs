namespace ReportService
{
    public class ReportServiceOptions
    {
        public ReportServiceOptions() {
        }

        public string SalaryReportTemplateFile { get; set; }
        public string DbConnectionString { get; set; }
        public string HumanResourcesDepartment { get; set; }
        public string BookkeepingDepartment { get; set; }
    }
}