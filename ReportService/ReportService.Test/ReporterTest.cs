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
        public void NoDepartments()
        {
            var employeeDB=new Mock<IEmployeeDB>();
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[0]);
            var empCodeResolver=new Mock<IEmpCodeResolver>();
            var salaryService=new Mock<ISalaryService>();
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object);
            var rep=ddd.MonthReport(2016,12);
            Assert.AreEqual("декабрь\n--------------------------------------------",rep.ReportString);
        }

        [TestMethod]
        public void OneEmptyDepartments()
        {
            var employeeDB=new Mock<IEmployeeDB>();
            employeeDB.Setup(p => p.GetDepartments()).Returns(() => new Department[]{new Department{Name="Test"}});
            var empCodeResolver=new Mock<IEmpCodeResolver>();
            var salaryService=new Mock<ISalaryService>();
            var ddd = new ReportService.Reports.Reporter(employeeDB.Object, empCodeResolver.Object, salaryService.Object);
            var rep=ddd.MonthReport(2016,12);
            Assert.AreEqual("декабрь\n--------------------------------------------\nTest\n--------------------------------------------",rep.ReportString);
        }
    }
}
