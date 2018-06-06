using System.Threading;
using System.Threading.Tasks;
using ReportService.Domain;

namespace ReportService.Salary{
    public interface ISalaryService{
        Task<int> SalaryAsync(Employee employee,CancellationToken cancel);
        Task<int> SalaryAsync(Employee employee);
    }
}