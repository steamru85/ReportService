using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CORE.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<IActionResult> Download(int year, int month)
        {
            var report = new Report(year, month);
            if (!string.IsNullOrEmpty(report.Content))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content("Вы указали неверный месяц или год для формирования отчета !");
            }

            List<Employee> employees = Employee.GetEmployeesFromActiveDepartments();
            Dictionary<string, List<Employee>> employeesByDepartment = new Dictionary<string, List<Employee>>();

            foreach (var employee in employees)
            {
                employee.BuhCode = await EmpCodeResolver.GetCode(employee.Inn);
                employee.Salary = employee.Salary();
                if (!employeesByDepartment.ContainsKey(employee.Department))
                {
                    employeesByDepartment.Add(employee.Department, new List<Employee>());
                }
                employeesByDepartment[employee.Department].Add(employee);
            }

            report.FillReport(employeesByDepartment);

            await report.Save();

            var response = PhysicalFile("D:\\report.txt", "application/octet-stream", "report.txt");
            return response;
        }
    }
}
