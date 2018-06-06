using System.Threading.Tasks;
using ReportService.Domain;

namespace ReportService.Salary{
    public interface ISalaryService{
        Task<int> Salary(Employee employee);
    }
}