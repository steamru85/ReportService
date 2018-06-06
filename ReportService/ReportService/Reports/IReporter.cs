using System.Threading.Tasks;
using ReportService.Domain;

namespace ReportService.Reports{
    public interface IReporter
    {        
        Task<Report> MonthReportAsync(int year,int month);
    }
}