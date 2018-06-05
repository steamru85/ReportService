using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ReportService.DbAccessor;
using ReportService.Domain;
using ReportService.Report;
using ReportService.Repository;
using ReportService.Services;
using Xunit;

namespace ReportService.Test
{
    public class ReportBuildingTest : IDisposable
    {
        [Theory]
        [InlineData(2018, 1)]
        public async void TestGenerateReport(int year, int month)
        {
            var hrDepartment = new Mock<IHumanResourcesDepartment>();
            hrDepartment
                .Setup(i => i.GetAccountCode(It.IsAny<string>()))
                .Returns<string>(hrGetAccountCode);

            var buhDepartment = new Mock<IBookkeepingDepartment>();
            buhDepartment
                .Setup(i => i.GetSalary(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(buhGetSalary);

            var dbAccessor = new Mock<IDbAccessor>();
            dbAccessor.Setup(i => i.Connect());
            dbAccessor.Setup(i => i.Close());
            dbAccessor
                .Setup(i => i.QueryAsync<Employee>(It.IsAny<string>()))
                .Returns<string>(cmd => Task.FromResult(GetTestEmployeeCollection()));

            var employeeRepo = new ActiveEmployeesRepository(dbAccessor.Object);

            dbAccessor.Object.Connect();
            IEnumerable<Employee> employees = await employeeRepo.GetAsync();
            IEmployeeSalaryReportBuilder reportBuilder = await
                new EmployeeSalaryReportBuilder(buhDepartment.Object, hrDepartment.Object, GetTestReportTemplate())
                .BuildData(year, month, employees);
            
            dbAccessor.Object.Close();

            string report = reportBuilder.GenerateReport();

            Assert.True(string.Equals("Январь", reportBuilder.ReportData.Month), "Month format");
            Assert.True(2018 == reportBuilder.ReportData.Year, "Year value");
            Assert.True(33000m == reportBuilder.ReportData.TotalAmount, "Total amount value");

            Assert.Equal(GetTestReportResult(), reportBuilder.ReportData.Departments);
            Assert.Equal(GetTestReportRender(), report);
        }

        private decimal buhGetSalary(string inn, string employeeCode)
        {
            return GetTestBuhCollection()[employeeCode];
        }

        private Task<string> hrGetAccountCode(string inn)
        {
            return Task.FromResult(inn);
        }

        public static IEnumerable<DepartmentReportItem> GetTestReportResult()
        {
            return new List<DepartmentReportItem>
            {
                new DepartmentReportItem("dep1", new List<EmployeeSalary> {
                    new EmployeeSalary(new Employee("employee1", "inn1", "dep1"), 1000m),
                    new EmployeeSalary(new Employee("employee2", "inn2", "dep1"), 1000m)
                }),
                new DepartmentReportItem("dep2", new List<EmployeeSalary> {
                    new EmployeeSalary(new Employee("employee3", "inn3", "dep2"), 2000m),
                    new EmployeeSalary(new Employee("employee4", "inn4", "dep2"), 3000m),
                    new EmployeeSalary(new Employee("employee5", "inn5", "dep2"), 5000m)
                }),
                new DepartmentReportItem("dep3", new List<EmployeeSalary> {
                    new EmployeeSalary(new Employee("employee6", "inn6", "dep3"), 8000m),
                    new EmployeeSalary(new Employee("employee7", "inn7", "dep3"), 13000m)
                }),
            };
        }

        // <empCode, salary> dictionary
        public static Dictionary<string, decimal> GetTestBuhCollection()
        {
            return new Dictionary<string, decimal>()
            {
                {"inn1", 1000},
                {"inn2", 1000},

                {"inn3", 2000},
                {"inn4", 3000},
                {"inn5", 5000},

                {"inn6", 8000},
                {"inn7", 13000}
            };
        }

        public static IEnumerable<Employee> GetTestEmployeeCollection()
        {
            return new List<Employee>()
            {
                new Employee("employee1", "inn1", "dep1"),
                new Employee("employee2", "inn2", "dep1"),

                new Employee("employee3", "inn3", "dep2"),
                new Employee("employee4", "inn4", "dep2"),
                new Employee("employee5", "inn5", "dep2"),

                new Employee("employee6", "inn6", "dep3"),
                new Employee("employee7", "inn7", "dep3")
            };
        }

        public static string GetTestReportTemplate()
        {
            return @"{{Month}} {{Year}}
{{#each Departments}}
{{#newline}}
{{#newline}}
---
{{#newline}}
{{#newline}}
{{Department}}
{{#newline}}
{{#newline}}
{{#each Employees}}
{{Employee.Name}}         {{Salary}}р
{{#newline}}
{{#newline}}
{{/each}}
Всего по отделу     {{DepartmentAmount}}р
{{/each}}
{{#newline}}
{{#newline}}
---
{{#newline}}
{{#newline}}
Всего по предприятию    {{TotalAmount}}р";
        }

        public string GetTestReportRender()
        {
            return
                @"Январь 2018

---

dep1

employee1         1000р

employee2         1000р

Всего по отделу     2000р

---

dep2

employee3         2000р

employee4         3000р

employee5         5000р

Всего по отделу     10000р

---

dep3

employee6         8000р

employee7         13000р

Всего по отделу     21000р

---

Всего по предприятию    33000р";
        }

        public void Dispose()
        {
            
        }
    }
}
