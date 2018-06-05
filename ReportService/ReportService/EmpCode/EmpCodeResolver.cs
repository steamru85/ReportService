using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ReportService.EmpCode{
    public class EmpCodeResolver : IEmpCodeResolver
    {
        private readonly string serviceUri;

        public EmpCodeResolver(IConfiguration config)
        {
            serviceUri = config.GetValue<string>("salaryServiceUri");
        }
        public async Task<string> GetCode(string inn)
        {
            var client = new HttpClient();
            return await client.GetStringAsync(serviceUri + inn);
        }
    }
}