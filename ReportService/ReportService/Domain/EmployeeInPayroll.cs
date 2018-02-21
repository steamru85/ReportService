using System;

namespace BusinessLogic.Contracts.Payroll
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
