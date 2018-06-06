using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportService.Domain;

namespace ReportService.Salary{
    public class SalaryService : ISalaryService
    {
        private readonly string serviceUri;

        public SalaryService(IConfiguration config)
        {
            serviceUri = config.GetValue<string>("empCodeUri");
        }
        static JsonSerializer ser=new JsonSerializer();
        public async Task<int> Salary(Employee employee)
        {
            var httpWebRequest = WebRequest.CreateHttp(serviceUri+employee.Inn);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            
            
            using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                ser.Serialize(streamWriter,new { employee.BuhCode });                
                await streamWriter.FlushAsync();                
            }
            string responseText;
            using(var httpResponse = await httpWebRequest.GetResponseAsync())
            {
                using(var reader = new StreamReader(httpResponse.GetResponseStream(), true))
                     responseText=await reader.ReadToEndAsync();
            }
            return (int)Decimal.Parse(responseText);//Из условия не понятно является ли отбрасывание дробной части для формирования отчета - так что оставлю как было изначально

        }
    }
}