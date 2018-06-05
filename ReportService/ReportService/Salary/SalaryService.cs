using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ReportService.Domain;

namespace ReportService.Salary{
    public class SalaryService : ISalaryService
    {
        public int Salary(Employee employee)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://salary.local/api/empcode/"+employee.Inn);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new { employee.BuhCode });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var reader = new System.IO.StreamReader(httpResponse.GetResponseStream(), true);
            string responseText = reader.ReadToEnd();
            return (int)Decimal.Parse(responseText);

        }
    }
}