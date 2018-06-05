using System.Threading.Tasks;

namespace ReportService.EmpCode{
    public interface IEmpCodeResolver{
        Task<string> GetCode(string inn);
    }
}