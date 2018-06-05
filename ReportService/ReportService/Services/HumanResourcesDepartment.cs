using System.Net.Http;
using System.Threading.Tasks;

namespace ReportService.Services
{
    /// <summary>
    /// HumanResourcesDepartment service
    /// </summary>
    public class HumanResourcesDepartment : IHumanResourcesDepartment
    {
        private readonly string _endPoint;

        public HumanResourcesDepartment(string endPoint)
        {
            _endPoint = endPoint;
        }

        /// <summary>
        /// Get internal account code
        /// </summary>
        /// <param name="inn">employee INN</param>
        /// <returns>string value of employee code</returns>
        public async Task<string> GetAccountCode(string inn)
        {
            var client = new HttpClient();
            return await client.GetStringAsync($"{_endPoint}{inn}");
        }
    }
}
