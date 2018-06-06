using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class Report
    {
        public string ReportString { get { return body.ToString(); } }
        private StringBuilder body = new StringBuilder();        
        public async Task SaveToAsync(Stream stream)
        {
            using (var writer = new StreamWriter(stream))
                await writer.WriteAsync(ReportString);
        }
        internal void AddDelimiter()
        {
            body.AppendLine("--------------------------------------------");
        }
        internal void AddName(string name)
        {
            body.AppendLine(name);
        }
        internal void AddNameWithValue(string name, int value)
        {
            body.AppendLine($"{name}         {value}р");
        }
    }
}
