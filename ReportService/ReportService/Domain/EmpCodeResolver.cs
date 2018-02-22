using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class EmpCodeResolver
    {
        public static async Task<string> GetCode(string inn)
        {
            var client = new HttpClient();
            return await client.GetStringAsync("http://buh.local/api/inn/" + inn);
        }
    }
}
