using System.Collections.Generic;
using ReportService.DbAccessor;
using ReportService.Domain;
using ReportService.Repository;
using ReportService.Services;

namespace ReportService.Tests
{
    public class ActiveEmployeesRepositoryMock : IReportRepository<Employee>
    {
        private readonly HumanResourcesDepartment _hrDepartment;
        private readonly BookkeepingDepartment _buhDepartment;
        private readonly IDbAccessor _dbAccessor;

        public ActiveEmployeesRepositoryMock(IDbAccessor dbAccessor, HumanResourcesDepartment hrDepartment, BookkeepingDepartment buhDepartment)
        {
            _dbAccessor = dbAccessor;
            _hrDepartment = hrDepartment;
            _buhDepartment = buhDepartment;
        }

        public IEnumerable<Employee> Get()
        {
            var employeeList = new List<Employee>()
            {
                new Employee("Андрей Сергеевич Бубнов", "ФинОтдел", "1", 70000, "1"),
                new Employee("Григорий Евсеевич Зиновьев", "ФинОтдел", "1", 65000, "1"),
                new Employee("Яков Михайлович Свердлов", "ФинОтдел", "1", 80000, "1"),
                new Employee("Алексей Иванович Рыков", "ФинОтдел", "1", 90000, "1"),

                new Employee("Василий Васильевич Кузнецов", "Бухгалтерия", "1", 50000, "1"),
                new Employee("Демьян Сергеевич Коротченко", "Бухгалтерия", "1", 55000, "1"),
                new Employee("Михаил Андреевич Суслов", "Бухгалтерия", "1", 35000, "1"),

                new Employee("Фрол Романович Козлов", "ИТ", "1", 90000, "1"),
                new Employee("Дмитрий Степанович Полянски", "ИТ", "1", 120000, "1"),
                new Employee("Андрей Павлович Кириленко", "ИТ", "1", 110000, "1"),
                new Employee("Арвид Янович Пельше", "ИТ", "1", 120000, "1"),
            };

            return employeeList;
        }
    }
}
