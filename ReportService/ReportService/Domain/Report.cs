using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Dapper;

namespace ReportService.Domain
{
    public class Report
    {
        public string S { get; set; }
        public byte[] Make(int year, int month)
        {
            S = MonthNameResolver.MonthName.GetName(year, month);
            var actions = new List<(Action<Employee, Report>, Employee)>();
            var emplist = new List<Employee>();
            
            using (var conn = new NpgsqlConnection(Startup.DBConnectionString))
            {
                conn.Open();
                emplist = conn.Query<Employee>("SELECT e.name as Name, e.inn as Inn, d.name as Department from emps e left join deps d on e.departmentid = d.id").ToList();
                conn.Close();
            }
            foreach(var item in emplist)
            {
                item.BuhCode = EmpCodeResolver.GetCode(item.Inn).Result;
                item.Salary = item.Salary();
            }

            var empDic = emplist.GroupBy(i => i.Department).ToDictionary(i => i.Key, i => i);        
            foreach (var depName in empDic.Keys.OrderBy(i => i))
            {
                actions.Add((new ReportFormatter(null).NL, new Employee()));
                actions.Add((new ReportFormatter(null).WL, new Employee()));
                actions.Add((new ReportFormatter(null).NL, new Employee()));
                actions.Add((new ReportFormatter(null).WD, new Employee() { Department = depName }));

                var depEmps = empDic[depName];
                foreach (var emp in depEmps.OrderBy(i => i.Name))
                {
                    actions.Add((new ReportFormatter(emp).NL, emp));
                    actions.Add((new ReportFormatter(emp).WE, emp));
                    actions.Add((new ReportFormatter(emp).WT, emp));
                    actions.Add((new ReportFormatter(emp).WS, emp));
                }
                var totalDep = new Employee() { Name = "Всего по отделу", Salary = depEmps.Sum(i => i.Salary) };
                actions.Add((new ReportFormatter(totalDep).NL, totalDep));
                actions.Add((new ReportFormatter(totalDep).WE, totalDep));
                actions.Add((new ReportFormatter(totalDep).WT, totalDep));
                actions.Add((new ReportFormatter(totalDep).WS, totalDep));
            }
            var total = new Employee() { Name = "Всего по предприятию", Salary = emplist.Sum(i => i.Salary) };
            actions.Add((new ReportFormatter(total).NL, total));
            actions.Add((new ReportFormatter(total).WL, total));
            actions.Add((new ReportFormatter(total).NL, total));
            actions.Add((new ReportFormatter(total).WE, total));
            actions.Add((new ReportFormatter(total).WT, total));
            actions.Add((new ReportFormatter(total).WS, total));

            foreach (var act in actions)
            {
                act.Item1(act.Item2, this);
            }
            return System.Text.UTF8Encoding.UTF8.GetBytes(S);
        }
    }
}
