using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ReportService.Services
{
    /// <summary>
    /// Bookkeeping department service
    /// </summary>
    public class BookkeepingDepartment : IBookkeepingDepartment
    {
        private readonly string _endPoint;

        public BookkeepingDepartment(string endPoint)
        {
            _endPoint = endPoint;
        }

        /// <summary>
        /// Get the salary of an employee
        /// </summary>
        /// <param name="inn">emloyee INN</param>
        /// <param name="employeeCode">employee code from HR department service</param>
        /// <returns>salary value</returns>
        public decimal GetSalary(string inn, string employeeCode)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{_endPoint}{inn}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { employeeCode });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var reader = new System.IO.StreamReader(httpResponse.GetResponseStream(), true);
            string responseText = reader.ReadToEnd();
            return decimal.Parse(responseText);
        }

    }
}
