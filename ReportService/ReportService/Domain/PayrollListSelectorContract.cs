using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Contracts.Payroll
{
    public class PayrollListSelectorContract
    {
        public int? PageSize { get; set; }
        public PayrollFilterContract FilterBy { get; set; }
    }

    

    public class PayrollFilterContract
    {
        public string Date { get; set; }
    }
}
