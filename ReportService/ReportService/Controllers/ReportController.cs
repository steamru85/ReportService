using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportService.DbAccessor;
using ReportService.Domain;
using ReportService.Repository;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IDbAccessor _dbAccessor;
        private readonly ILogger<ReportController> _logger;
        private readonly IReportRepository<Employee> _employeeRepository;

        public ReportController(IDbAccessor dbAccessor, IReportRepository<Employee> employeeRepository, ILogger<ReportController> logger)
        {
            _dbAccessor = dbAccessor;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public IActionResult Download(int year, int month)
        {
            _logger.LogInformation("Download report {0} {1}", year, month);
            _dbAccessor.Connect();
            List<Employee> employeeList = _employeeRepository.Get().ToList();
            _dbAccessor.Close();

            var depEmpList = employeeList.GroupBy(i => i.Department);

            object data = new
            {
                departments = depEmpList.Select(i => new
                {
                    name = i.Key,
                    amount = i.Sum(e => e.Salary),
                    employees = i.Select(e => new {
                        name = e.Name,
                        salary = e.Salary
                    })
                }).ToList(),
                month = Common.MonthNameResolver.GetName(year, month),
                year = year,
                totalamount = employeeList.Sum(i => i.Salary)
            };

            string report = Nustache.Core.Render.StringToString(
@"{{{month}}} {{year}}
{{#departments}}

---
{{{name}}}

{{#employees}}
{{{name}}}         {{salary}}

{{/employees}}
Всего по отделу     {{amount}}p
{{/departments}}

---

Всего по предприятию    {{totalamount}}p", data);

            //var response = Content(report, "text/plan");
            var response = File(new MemoryStream(Encoding.UTF8.GetBytes(report)), "application/octet-stream", "report.txt");
            return response;
        }
    }
}
