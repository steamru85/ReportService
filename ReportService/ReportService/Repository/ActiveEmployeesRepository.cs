using System.Collections.Generic;
using System.Threading.Tasks;
using ReportService.DbAccessor;
using ReportService.Domain;

namespace ReportService.Repository
{
    public class ActiveEmployeesRepository : IEmployeesRepository
    {
        private const string SqlCommand = "SELECT e.name, e.inn, d.name as department FROM emps e LEFT JOIN deps d ON e.departmentid = d.id WHERE d.active = true";
        private readonly IDbAccessor _dbAccessor;

        public ActiveEmployeesRepository(IDbAccessor dbAccessor)
        {
            _dbAccessor = dbAccessor;
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            IEnumerable<Employee> result = await _dbAccessor.QueryAsync<Employee>(SqlCommand);
            return result;
        }
    }
}
