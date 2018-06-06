using System.Net.Http;
using System.Threading;
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
        public Task<string> GetCodeAsync(string inn)
        {            
            return  GetCodeAsync(inn,CancellationToken.None);
        }

        public async Task<string> GetCodeAsync(string inn, CancellationToken cancel)
        {
            return await client.GetStringAsync(serviceUri + inn);
        }
    }
}