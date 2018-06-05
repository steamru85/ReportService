using ReportService.Domain;

namespace ReportService.Reports{
    public interface IReporter
    {
        Report MonthReport(int year,int month);
    }
}