using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReportService.Domain;
using ReportService.EmpCode;
using ReportService.EmployeeDB;
using ReportService.Salary;

namespace ReportService.Test
{
    [TestClass]
    public class ReporterTest
    {
        [TestMethod]
        public async Task NoDepartments()
        {
            var employeeDB=new Mock<IEmployeeDB>();
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[0]);
            var empCodeResolver=new Mock<IEmpCodeResolver>();
            var salaryService=new Mock<ISalaryService>();
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object);
            var rep=await ddd.MonthReportAsync(2016,12);
            Assert.AreEqual(@"декабрь 2016
--------------------------------------------
Всего по предприятию         0р
",rep.ReportString);
        }

        [TestMethod]
        public async Task OneEmptyDepartment()
        {
            var employeeDB=new Mock<IEmployeeDB>();
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[]{new Department{Name="Test"}});
            var empCodeResolver=new Mock<IEmpCodeResolver>();
            var salaryService=new Mock<ISalaryService>();
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object);
            var rep=await ddd.MonthReportAsync(2016,12);
            Assert.AreEqual(@"декабрь 2016
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

            var employeeDB=new Mock<IEmployeeDB>();
            var dep=new Department{Name="Test"};
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[]{dep});
            employeeDB.Setup(p=>p.GetEmployeesFromDepartment(It.IsAny<Department>())).Callback(()=>{Console.WriteLine("++++++++++++++");}).Returns(()=>new Employee[]{new Employee{Name="Ivanov Ivan"}});
            var empCodeResolver=new Mock<IEmpCodeResolver>();
            var salaryService=new Mock<ISalaryService>();
            salaryService.Setup(p=>p.Salary(It.IsAny<Employee>())).Returns(async ()=>{return 500;});
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object);
            var rep=await ddd.MonthReportAsync(2016,12);
            Assert.AreEqual(@"декабрь 2016
--------------------------------------------
Test
Ivanov Ivan         500р
Всего по отделу         500р
--------------------------------------------
Всего по предприятию         500р
",rep.ReportString);
        }
    }
}
