using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class PasswordStore
    {
        public string UserName { get => "admin"; }
        public string Password { get => "pa$$w0rd"; }

        public PasswordStore()
        {
            Employee employee = null;
            Console.WriteLine(employee.Name);
        }
    }
}
