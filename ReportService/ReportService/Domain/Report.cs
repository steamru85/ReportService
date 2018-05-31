using System.IO;
using System.Text;

namespace ReportService.Domain
{
    public class Report
    {
        public string S { get; set; }
        public Stream Stream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(S ?? ""));
        }
    }
}
