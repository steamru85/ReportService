using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportService.DbAccessor;
using ReportService.Domain;
using ReportService.Report;
using ReportService.Repository;
using ReportService.Services;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IDbAccessor _dbAccessor;
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IEmployeeSalaryReportBuilder _reportBuilder;

        public ReportController(IDbAccessor dbAccessor, IEmployeesRepository employeeRepository, IEmployeeSalaryReportBuilder reportBuilder)
        {
            _dbAccessor = dbAccessor;
            _employeeRepository = employeeRepository;
            _reportBuilder = reportBuilder;
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<IActionResult> Download(int year, int month)
        {
            _dbAccessor.Connect();

            IEnumerable<Employee> employees = await _employeeRepository.GetAsync();
            string report = (await _reportBuilder.BuildData(year, month, employees)).GenerateReport();

            _dbAccessor.Close();

            var response = File(new MemoryStream(Encoding.UTF8.GetBytes(report)), "application/octet-stream", "report.txt");
            return response;
        }
    }
}
