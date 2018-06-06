using System.Threading;
using System.Threading.Tasks;

namespace ReportService.EmpCode{
    public interface IEmpCodeResolver{
        Task<string> GetCodeAsync(string inn);
        Task<string> GetCodeAsync(string inn,CancellationToken cancel);
    }
}