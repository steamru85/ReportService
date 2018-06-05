using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class Report
    {
        public string ReportString { get; set; }
        public void Save()
        {
            System.IO.File.WriteAllText("D:\\report.txt", ReportString);
        }
    }
}
