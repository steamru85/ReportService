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
using ReportService.Reports;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReporter reporter;

        public ReportController(IReporter reporter){
            this.reporter=reporter;            
        }
        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<IActionResult> Download(int year, int month)
        {
            var report=await reporter.MonthReportAsync(year,month);
            MemoryStream mem=new MemoryStream();
            await report.SaveToAsync(mem);            
            return File(mem.ToArray(), "application/octet-stream", "report.txt");
        }
    }
}