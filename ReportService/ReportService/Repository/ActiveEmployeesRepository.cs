using System.Collections.Generic;
using System.Data.Common;
using ReportService.DbAccessor;
using ReportService.Domain;
using ReportService.Services;

namespace ReportService.Repository
{
    public class ActiveEmployeesRepository : IReportRepository<Employee>
    {
        private const string SqlCommand = "SELECT e.name, e.inn, d.name FROM emps e LEFT JOIN deps d ON e.departmentid = d.id";
        private readonly HumanResourcesDepartment _hrDepartment;
        private readonly BookkeepingDepartment _buhDepartment;
        private readonly IDbAccessor _dbAccessor;

        public ActiveEmployeesRepository(IDbAccessor dbAccessor, HumanResourcesDepartment hrDepartment, BookkeepingDepartment buhDepartment)
        {
            _dbAccessor = dbAccessor;
            _hrDepartment = hrDepartment;
            _buhDepartment = buhDepartment;
        }

        public IEnumerable<Employee> Get()
        {
            var result = new List<Employee>();
            DbDataReader reader = _dbAccessor.ExecuteCommand($"{SqlCommand} WHERE d.active = true");
            foreach (DbDataRecord rec in reader)
            {
                string name = rec.GetString(0);
                string inn = rec.GetString(1);
                string department = rec.GetString(2);
                string employeeCode = _hrDepartment.GetAccountCode(inn).Result;
                decimal salary = _buhDepartment.GetSalary(inn, employeeCode);

                var emp = new Employee(name, department, inn, salary, employeeCode);
                result.Add(emp);
            }

            return result;
        }
    }
}
