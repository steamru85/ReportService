using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportService.Domain;
using ReportService.EmpCode;
using ReportService.EmployeeDB;
using ReportService.MonthResolver;
using ReportService.Salary;

namespace ReportService.Test
{
    [TestClass]
    public class ReporterTest
    {
        private int year;
        private int month;
        private string monthName;
        private Mock<IEmployeeDB> employeeDB;
        private Mock<IEmpCodeResolver> empCodeResolver;
        private Mock<ISalaryService> salaryService;
        private Mock<IMonthResolver> monthResolver;

        [TestInitialize]
        public void Init(){
            year=2016;
            month=12;
            monthName="декабрь";
            employeeDB=new Mock<IEmployeeDB>();
            empCodeResolver=new Mock<IEmpCodeResolver>();
            salaryService=new Mock<ISalaryService>();
            monthResolver=new Mock<IMonthResolver>();
            monthResolver.Setup(p=>p.GetName(It.IsAny<int>(),It.IsAny<int>())).Returns(monthName);
        }
        [TestMethod]
        public async Task NoDepartments()
        {   
            employeeDB.Reset();empCodeResolver.Reset();salaryService.Reset();            
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[0]);            
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object,monthResolver.Object);
            var rep=await ddd.MonthReportAsync(year,month);
            Assert.AreEqual($@"{monthName} 2016
--------------------------------------------
Всего по предприятию         0р
",rep.ReportString);
        }

        [TestMethod]
        public async Task OneEmptyDepartment()
        {
            employeeDB.Reset();empCodeResolver.Reset();salaryService.Reset();                        
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[]{new Department{Name="Test"}});            
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object,monthResolver.Object);
            var rep=await ddd.MonthReportAsync(year,month);
            Assert.AreEqual($@"{monthName} 2016
--------------------------------------------
Test
Всего по отделу         0р
--------------------------------------------
Всего по предприятию         0р
",rep.ReportString);
        }
        
        [TestMethod]
        public async Task OneNotEmptyDepartment()
        {
            employeeDB.Reset();empCodeResolver.Reset();salaryService.Reset();                        
            var dep=new Department{Name="Test"};
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[]{dep});
            employeeDB.Setup(p=>p.GetEmployeesFromDepartment(It.IsAny<Department>())).Returns(()=>new Employee[]{new Employee{Name="Ivanov Ivan"}});                        
            salaryService.Setup(p => p.SalaryAsync(It.IsAny<Employee>(),It.IsAny<CancellationToken>())).Returns(Task.FromResult(500));
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object,monthResolver.Object);
            var rep=await ddd.MonthReportAsync(year,month);
            Assert.AreEqual($@"{monthName} 2016
--------------------------------------------
Test
Ivanov Ivan         500р
Всего по отделу         500р
--------------------------------------------
Всего по предприятию         500р
",rep.ReportString);
        }


        [TestMethod]
        public async Task MultiNotEmptyDepartment()
        {
            var deps =new[] { new Department { Name = "Test" }, new Department { Name = "Test1" } };
            var emps=new[]{ new Employee { Name = "Ivanov Ivan" }, new Employee { Name = "Ivanov1 Ivan1" }};
            employeeDB.Reset();empCodeResolver.Reset();salaryService.Reset();                        
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => deps);
            employeeDB.Setup(p => p.GetEmployeesFromDepartment(It.IsAny<Department>())).Returns(emps  );
            salaryService.Setup(p => p.SalaryAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(500));

            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object,monthResolver.Object);
            var rep=await ddd.MonthReportAsync(year,month);
            Assert.AreEqual($@"{monthName} 2016
--------------------------------------------
Test
Ivanov Ivan         500р
Ivanov1 Ivan1         500р
Всего по отделу         1000р
--------------------------------------------
Test1
Ivanov Ivan         500р
Ivanov1 Ivan1         500р
Всего по отделу         1000р
--------------------------------------------
Всего по предприятию         2000р
", rep.ReportString);
        }

    }
}
