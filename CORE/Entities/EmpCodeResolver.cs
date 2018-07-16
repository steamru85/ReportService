using System.Net.Http;
using System.Threading.Tasks;

namespace CORE.Entities
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
