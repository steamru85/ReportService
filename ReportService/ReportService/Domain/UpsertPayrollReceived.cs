using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts.Payroll
{
    public class UpsertPayrollReceived
    {
        public bool IsNew { get; set; }
        public PayrollInLayer Payroll { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}
