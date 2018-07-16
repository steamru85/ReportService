using System;

namespace CORE.Entities
{
    public class PayrollAndTransactions
    {
        public Guid Id { get; set; }
        public Guid CommissionId { get; set; }
        public string[] EmployeesTransactions { get; set; }
        public string[] CommissionTransactions { get; set; }
    }
}