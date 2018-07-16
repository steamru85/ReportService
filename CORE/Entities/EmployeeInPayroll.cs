using System;

namespace CORE.Entities
{
    public class EmployeeInPayroll : PayrollEmployee
    {
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public Guid? TransactionId { get; set; }
        public string Status { get; set; }
        public bool OuterSyncDone { get; set; }
        public string PaymentPurpose { get; set; }
    }
}
