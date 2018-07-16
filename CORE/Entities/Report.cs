using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Entities
{
    public class Report
    {
        public string FileName { get; private set; }

        #region Constructors
        public Report()
        {

        }

        public Report(int year, int month)
        {
            if (year > 0 && month > 0 && month <= 12)
            {
                var createdDate = new DateTime(year, month, 0);
                FileName = $"{ConfigurationManager.Get("FilePath")}report_{createdDate.ToString("MMMM_yyyy")}.txt";
                Content = createdDate.ToString("MMMM yyyy");
            }
        }
        #endregion
        /// <summary>
        /// Содержимое отчета
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Вся сумма по предприятию
        /// All sum by company
        /// </summary>
        private int allSum { get; set; }

        public async Task Save()
        {
           await System.IO.File.WriteAllBytesAsync($"{FileName}", Encoding.UTF8.GetBytes(Content));
        }

        public void FillReport(Dictionary<string, List<Employee>> employeesByDepartment)
        {
            StringBuilder reportBuilder = new StringBuilder();
            
            foreach (var departmentEmployees in employeesByDepartment)
            {
                reportBuilder.Append(CreateReportForDepartment(departmentEmployees.Key, departmentEmployees.Value));
            }
            reportBuilder.AppendLine($"Всего по Предприятию {allSum} р");
            this.Content = reportBuilder.ToString();
        }

        private string CreateReportForDepartment(string departmentName, List<Employee> employees)
        {
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine();
            reportBuilder.AppendLine("--------------------------------------------");
            reportBuilder.AppendLine(departmentName);
            int sum = 0;
            foreach (var employee in employees)
            {
                reportBuilder.AppendLine($"{employee.Name} {employee.Salary}р");
                sum += employee.Salary;
                allSum += sum;
            }
            reportBuilder.AppendLine($"Всего по отделу {sum}р");
            return reportBuilder.ToString();
        }
    }
}
