using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.EmpCode{
    public class EmpCodeResolver : IEmpCodeResolver
    {
        public async Task<string> GetCode(string inn)
        {
            var client = new HttpClient();
            return await client.GetStringAsync("http://buh.local/api/inn/" + inn);
        }
    }
}