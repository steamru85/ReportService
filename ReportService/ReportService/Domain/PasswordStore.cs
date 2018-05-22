using System;
using System.Collections.Generic;
using System.IO;
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
            Employee employee2 = null;
            Console.WriteLine(employee.Name);
            Console.WriteLine(employee2.Name);
            File.OpenRead("C:\\1.txt");
        }

        public int SomeMetdod(bool add, int c1, int c2)
        {
            if (add)
                return c1 + c2;
            else return c1 - c2;
        }
    }
}
