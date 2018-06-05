using System.Threading.Tasks;

namespace ReportService.Services
{
    public interface IHumanResourcesDepartment
    {
        Task<string> GetAccountCode(string inn);
    }
}