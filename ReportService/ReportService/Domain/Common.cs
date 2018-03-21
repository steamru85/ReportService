using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public static class EmployeeCommonMethods
    {
        public static int Salary(this Employee employee)
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
            var responseText = "";
            using (var reader = new StreamReader(httpResponse.GetResponseStream(), true))
            {
                responseText = reader.ReadToEnd();
            }
            return (int)Decimal.Parse(responseText);
        }
    }
}
