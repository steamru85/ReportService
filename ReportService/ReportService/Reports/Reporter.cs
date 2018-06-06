using System.Threading;
using System.Threading.Tasks;
using ReportService.Domain;
using ReportService.EmpCode;
using ReportService.EmployeeDB;
using ReportService.MonthResolver;
using ReportService.Salary;

namespace ReportService.Reports{
    public class Reporter : IReporter
    {
        private readonly IEmployeeDB employeeDB;
        private readonly IEmpCodeResolver empCodeResolver;
        private readonly ISalaryService salaryService;
        private readonly IMonthResolver monthResolver;

        public Reporter(IEmployeeDB employeeDB,IEmpCodeResolver empCodeResolver,ISalaryService salaryService,IMonthResolver monthResolver){
            this.employeeDB=employeeDB;
            this.empCodeResolver=empCodeResolver;
            this.salaryService=salaryService;
            this.monthResolver=monthResolver;
        }
        public async Task<Report> MonthReportAsync(int year, int month){
            return await MonthReportAsync(year,month,CancellationToken.None);
        }
        public async Task<Report> MonthReportAsync(int year,int month,CancellationToken cancel)
        {
            var totalSalary=new Department{
                Name="Всего по предприятию",
                Salary=0
            };
            var report = new Report();
            report.AddName($"{monthResolver.GetName(year, month)} {year}");            
            foreach(var dep in employeeDB.GetDepartments())
            {
                report.AddDelimiter();                
                report.AddName(dep.Name);
                foreach(var emp in employeeDB.GetEmployeesFromDepartment(dep))
                {
                    emp.BuhCode =await empCodeResolver.GetCodeAsync(emp.Inn,cancel);
                    emp.Salary =await salaryService.SalaryAsync(emp,cancel);                    
                    dep.Salary+=emp.Salary;
                    report.AddNameWithValue(emp.Name,emp.Salary);
                    cancel.ThrowIfCancellationRequested();
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