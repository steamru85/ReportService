using System;
using System.Collections.Generic;
using System.Linq;
using ReportService.Domain;
using ReportService.EmpCode;
using ReportService.EmployeeDB;
using ReportService.Salary;

namespace ReportService.Reports{
    public class Reporter : IReporter
    {
        private readonly IEmployeeDB employeeDB;
        private readonly IEmpCodeResolver empCodeResolver;
        private readonly ISalaryService salaryService;

        public Reporter(IEmployeeDB employeeDB,IEmpCodeResolver empCodeResolver,ISalaryService salaryService){
            this.employeeDB=employeeDB;
            this.empCodeResolver=empCodeResolver;
            this.salaryService=salaryService;
        }
        public Report MonthReport(int year, int month)
        {
            var totalSalary=new Employee{
                Name="Всего по предприятию",
                Salary=0
            };
            var actions = new List<(Action<Employee, Report>, Employee)>();
            var report = new Report() { ReportString = $"{MonthNameResolver.MonthName.GetName(year, month)} {year}" };           
                       
            foreach(var dep in employeeDB.GetDepartments())
            {
                List<Employee> emplist = new List<Employee>();
                var depName = dep.Name;
                
                
                foreach(var emp in employeeDB.GetEmployeesFromDepartment(dep))
                {                
                    emp.BuhCode = empCodeResolver.GetCode(emp.Inn).Result;
                    emp.Salary = salaryService.Salary(emp);                    
                    totalSalary.Salary+=emp.Salary;
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
            actions.Add((new ReportFormatter(null).NL, null));
            actions.Add((new ReportFormatter(totalSalary).WE,totalSalary));
            actions.Add((new ReportFormatter(totalSalary).WT,totalSalary));
            actions.Add((new ReportFormatter(totalSalary).WS,totalSalary));
            foreach (var act in actions)
            {
                act.Item1(act.Item2, report);
            }
            return report;
        }
    }
}