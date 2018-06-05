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

        public Action<Employee, Report> NL = (e, s) => s.ReportString = s.ReportString + Environment.NewLine;
        public Action<Employee, Report> WL = (e, s) => s.ReportString = s.ReportString + "--------------------------------------------";
        public Action<Employee, Report> WT = (e, s) => s.ReportString = s.ReportString + "         ";
        public Action<Employee, Report> WE = (e, s) => s.ReportString = s.ReportString + e.Name;
        public Action<Employee, Report> WS = (e, s) => s.ReportString = s.ReportString + e.Salary + "р";
        public Action<Employee, Report> WD = (e, s) => s.ReportString = s.ReportString + e.Department;
        public Employee Employee { get; }
    }
}
