using System;

namespace BusinessLogic.Contracts.Payroll
{
    public class PayrollEmployee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Guid CompanyId { get; set; }
        public string BankName { get; set; }
        public string BankBik { get; set; }
        public string BankInn { get; set; }
        public Guid BankId { get; set; }
        public bool CardNumberWasDeleted { get; set; }
        public string AccountNumber { get; set; }
        public string CurrentAccountNumber { get; set; }
        public DateTime Date { get; set; }
        public bool CommissionExecuted { get; set; }
    }
}
