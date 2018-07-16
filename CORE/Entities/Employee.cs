using System.Collections.Generic;
using System.Linq;

namespace CORE.Entities
{
    public class Employee
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("departmentid")]
        public string Department { get; set; }

        [Column("inn")]
        public string  Inn { get; set; }

        public int Salary { get; set; }

        public string BuhCode { get; set; }

        public static List<Employee> GetEmployeesFromActiveDepartments()
        {
            List<Employee> employees;
            using (Database database = new Database())
            {
                string sql = "SELECT e.name, e.inn, d.name as departmentid  from emps e left join deps d on e.departmentid = d.id where d.active = true;";

                employees = database.Query<Employee>(sql).ToList();
            }
            return employees;
        }

    }
}
