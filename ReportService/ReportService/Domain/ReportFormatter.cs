using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class ReportFormatter
    {
        public Action<string> NL = (s) => s = s +Environment.NewLine;
        public  Action<string> WL = (s) =>  s = s + "--------------------------------------------";
        public Action<string> WT = (s) => s = s + "         ";
        public Action<Employee,string> WE = (e, s) => s = s + e.Name;
        public Action<Employee, string> WS = (e, s) => s = s + e.Salary + "р";
    }
}
