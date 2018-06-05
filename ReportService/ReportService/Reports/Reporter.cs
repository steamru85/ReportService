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
                actions.Add((new ReportFormatter(null).NL, new Employee()));
                actions.Add((new ReportFormatter(null).WL, new Employee()));
                actions.Add((new ReportFormatter(null).NL, new Employee()));
                actions.Add((new ReportFormatter(null).WD, new Employee() { Department = dep.Name } ));
                
                foreach(var emp in employeeDB.GetEmployeesFromDepartment(dep))
                {
                    emp.BuhCode = empCodeResolver.GetCode(emp.Inn).Result;
                    emp.Salary = salaryService.Salary(emp);                    
                    dep.Salary+=emp.Salary;
                    actions.Add((new ReportFormatter(emp).NL, emp));
                    actions.Add((new ReportFormatter(emp).WE, emp));
                    actions.Add((new ReportFormatter(emp).WT, emp));
                    actions.Add((new ReportFormatter(emp).WS, emp));

                }
                
                totalSalary.Salary+=dep.Salary;
                var dEmp=new Employee{
                    Name="Всего по отделу",
                    Salary=dep.Salary
                };
                actions.Add((new ReportFormatter(dEmp).NL, dEmp));
                actions.Add((new ReportFormatter(dEmp).WE, dEmp));
                actions.Add((new ReportFormatter(dEmp).WT, dEmp));
                actions.Add((new ReportFormatter(dEmp).WS, dEmp));
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