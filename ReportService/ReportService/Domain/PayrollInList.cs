using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BusinessLogic.Contracts.Payroll
{
    public class PayrollInList
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EPayrollStatus Status { get; set; }
        public decimal EmployeesPayments { get; set; }
        public decimal NdflPayments { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EPaymentType PaymentType { get; set; }
        public DateTime ModDate { get; set; }
    }
}
