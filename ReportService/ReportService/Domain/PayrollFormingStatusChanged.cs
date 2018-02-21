using System;

namespace BusinessLogic.Contracts.Payroll
{
    public class PayrollFormingStatusChanged
    {
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public Guid PayrollId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public string DefaultText { get; set; }
    }
}