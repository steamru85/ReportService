using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ReportService.Domain;
using ReportService.EmployeeDB;
using ReportService.EmpCode;
using ReportService.Salary;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IEmployeeDB employeeDB;
        private readonly IEmpCodeResolver empCodeResolver;
        private readonly ISalaryService salaryService;

        public ReportController(IEmployeeDB employeeDB,IEmpCodeResolver empCodeResolver,ISalaryService salaryService){
            this.employeeDB=employeeDB;
            this.empCodeResolver=empCodeResolver;
            this.salaryService=salaryService;
        }
        [HttpGet]
        [Route("{year}/{month}")]
        public IActionResult Download(int year, int month)
        {
            var actions = new List<(Action<Employee, Report>, Employee)>();
            var report = new Report() { S = MonthNameResolver.MonthName.GetName(year, month) };           
                       
            foreach(var dep in employeeDB.GetDepartments())
            {
                List<Employee> emplist = new List<Employee>();
                var depName = dep.Name;
                
                
                foreach(var emp in employeeDB.GetEmployees())
                {                
                    emp.BuhCode = empCodeResolver.GetCode(emp.Inn).Result;
                    emp.Salary = salaryService.Salary(emp);
                    if (emp.Department != depName)
                        continue;
                    emplist.Add(emp);
                }

                actions.Add((new ReportFormatter(null).NL, new Employee()));
                actions.Add((new ReportFormatter(null).WL, new Employee()));
                actions.Add((new ReportFormatter(null).NL, new Employee()));
                actions.Add((new ReportFormatter(null).WD, new Employee() { Department = depName } ));
                for (int i = 1; i < emplist.Count(); i ++)
                {
                    actions.Add((new ReportFormatter(emplist[i]).NL, emplist[i]));
                    actions.Add((new ReportFormatter(emplist[i]).WE, emplist[i]));
                    actions.Add((new ReportFormatter(emplist[i]).WT, emplist[i]));
                    actions.Add((new ReportFormatter(emplist[i]).WS, emplist[i]));
                }  

            }
            actions.Add((new ReportFormatter(null).NL, null));
            actions.Add((new ReportFormatter(null).WL, null));

            foreach (var act in actions)
            {
                act.Item1(act.Item2, report);
            }
            report.Save();
            var file = System.IO.File.ReadAllBytes("D:\\report.txt");
            var response = File(file, "application/octet-stream", "report.txt");
            return response;
        }
    }
}
