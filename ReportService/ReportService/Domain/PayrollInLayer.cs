using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BusinessLogic.Contracts.Payroll
{
    public class PayrollInLayer
    {
        public Guid? Id { get; set; }
        public int Number { get; set; }
        public Guid FromAccountId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EPaymentType PaymentType { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public EPayrollStatus Status { get; set; }
        public List<EmployeeInPayroll> Employees { get; set; }
        public DateTime Date { get; set; }
        public Guid? TaxTransactionId { get; set; }
        public bool Signed { get; set; }
        public decimal NdflPayments { get; set; }
        public decimal EmployeesPayments { get; set; }
        public DateTime ModDate { get; set; }
    }
}