using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ReportService.Service
{
    /// <summary>
    /// Bookkeeping department service
    /// </summary>
    public class BookkeepingDepartment
    {
        /// <summary>
        /// Get the salary of an employee
        /// </summary>
        /// <param name="inn">emloyee INN</param>
        /// <param name="employeeCode">employee code from HR department service</param>
        /// <returns>salary value</returns>
        public static decimal GetSalary(string inn, string employeeCode)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://salary.local/api/empcode/{inn}");
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
