using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ReportService.Domain;
using ReportService.Service;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        [HttpGet]
        [Route("{year}/{month}")]
        public IActionResult Download(int year, int month)
        {
            var actions = new List<(Action<Employee, Report>, Employee)>();
            var report = new Report() { S = Common.MonthNameResolver.GetName(year, month) };
            var connString = "Host=192.168.99.100;Username=postgres;Password=1;Database=employee";

            var conn = new NpgsqlConnection(connString);
            conn.Open();

            var empList = new List<Employee>();
            var cmd = new NpgsqlCommand("SELECT e.name, e.inn, d.name FROM emps e LEFT JOIN deps d ON e.departmentid = d.id WHERE d.active = true", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string inn = reader.GetString(1);
                string employeeCode = HumanResourcesDepartment.GetAccountCode(inn).Result;
                var emp = new Employee(
                    reader.GetString(0),
                    reader.GetString(2),
                    inn,
                    BookkeepingDepartment.GetSalary(inn, employeeCode),
                    employeeCode);

                empList.Add(emp);
            }

            conn.Close();

            var depEmpList = empList.GroupBy(i => i.Department);
            foreach (var departmentGroup in depEmpList)
            {
                actions.Add((new ReportFormatter(null).NL, null));
                actions.Add((new ReportFormatter(null).WL, null));
                actions.Add((new ReportFormatter(null).NL, null));
                actions.Add((new ReportFormatter(null).WD, departmentGroup.FirstOrDefault()));

                foreach (var employee in departmentGroup)
                {
                    actions.Add((new ReportFormatter(employee).NL, employee));
                    actions.Add((new ReportFormatter(employee).WE, employee));
                    actions.Add((new ReportFormatter(employee).WT, employee));
                    actions.Add((new ReportFormatter(employee).WS, employee));
                }
            }

            actions.Add((new ReportFormatter(null).NL, null));
            actions.Add((new ReportFormatter(null).WL, null));

            foreach (var act in actions)
            {
                act.Item1(act.Item2, report);
            }
            
            var response = File(report.Stream(), "application/octet-stream", "report.txt");
            return response;
        }
    }
}
