using System.Threading.Tasks;
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
        public async Task<Report> MonthReportAsync(int year, int month)
        {
            var totalSalary=new Department{
                Name="Всего по предприятию",
                Salary=0
            };
            var report = new Report();
            report.AddName($"{MonthNameResolver.MonthName.GetName(year, month)} {year}");            
            foreach(var dep in employeeDB.GetDepartments())
            {
                report.AddDelimiter();                
                report.AddName(dep.Name);
                foreach(var emp in employeeDB.GetEmployeesFromDepartment(dep))
                {
                    emp.BuhCode =await empCodeResolver.GetCode(emp.Inn);
                    emp.Salary =await salaryService.Salary(emp);                    
                    dep.Salary+=emp.Salary;
                    report.AddNameWithValue(emp.Name,emp.Salary);
                }
                
                totalSalary.Salary+=dep.Salary;                
                report.AddNameWithValue("Всего по отделу",dep.Salary);
            }
            report.AddDelimiter();
            report.AddNameWithValue(totalSalary.Name,totalSalary.Salary);
            return report;
        }        
    }
}