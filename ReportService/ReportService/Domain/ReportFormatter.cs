using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class ReportFormatter
    {
        public ReportFormatter(Employee e)
        {
            Employee = e;
        }

        public Action<Employee, string> NL = (e, s) => s = s + Environment.NewLine;
        public Action<Employee, string> WL = (e, s) => s = s + "--------------------------------------------";
        public Action<Employee, string> WT = (e, s) => s = s + "         ";
        public Action<Employee, string> WE = (e, s) => s = s + e.Name;
        public Action<Employee, string> WS = (e, s) => s = s + e.Salary + "р";
        public Action<Employee, string> WD = (e, s) => s = s + e.Department;
        public Employee Employee { get; }
    }
}
