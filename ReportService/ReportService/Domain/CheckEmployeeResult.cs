using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts.Payroll
{
    public class CheckPayrollResult
    {
        public string ErrorCode { get; set; }
        public List<CheckEmployeeInPayrollResult> RecipientErrors { get; set; }
    }

    public class CheckEmployeeInPayrollResult
    {
        public Guid Id { get; set; }
        public string ErrorCode { get; set; }
    }
}
