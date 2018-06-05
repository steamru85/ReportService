using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class Report
    {
        public string ReportString { get{return body.ToString();} }
//        public string Header { get; internal set; }
        private StringBuilder body=new StringBuilder();
        public void Save()
        {
            System.IO.File.WriteAllText("D:\\report.txt", ReportString);
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
