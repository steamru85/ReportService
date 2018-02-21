using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts.Payroll
{
    public class PayrollAndTransactions
    {
        public Guid Id { get; set; }
        public Guid CommissionId { get; set; }
        public string[] EmployeesTransactions { get; set; }
        public string[] CommissionTransactions { get; set; }
    }
}