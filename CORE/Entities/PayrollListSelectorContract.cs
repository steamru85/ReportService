namespace CORE.Entities
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
