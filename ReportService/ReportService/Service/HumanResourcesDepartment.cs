using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.Service
{
    /// <summary>
    /// HumanResourcesDepartment service
    /// </summary>
    public class HumanResourcesDepartment
    {
        /// <summary>
        /// Get internal account code
        /// </summary>
        /// <param name="inn">employee INN</param>
        /// <returns>string value of employee code</returns>
        public static async Task<string> GetAccountCode(string inn)
        {
            var client = new HttpClient();
            return await client.GetStringAsync($"http://buh.local/api/inn/{inn}");
        }
    }
}
