namespace CORE.Entities
{
    public class PayrollWidgetData
    {
        public PaidForPeriod Month { get; set; }
        public PaidForPeriod Year { get; set; }
        public PaidForPeriod Total { get; set; }
    }

    public class PaidForPeriod
    {
        public decimal ToEmployees { get; set; }
        public decimal Ndfl { get; set; }
        public decimal Count { get; set; }
    }
}
