using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ReportService.EmpCode{
    public class EmpCodeResolver : IEmpCodeResolver
    {
        HttpClient client = new HttpClient();
        private readonly string serviceUri;

        public EmpCodeResolver(IConfiguration config)
        {
            serviceUri = config.GetValue<string>("salaryServiceUri");
        }
        public async Task<string> GetCode(string inn)
        {            
            return await client.GetStringAsync(serviceUri + inn);
        }
    }
}