namespace ReportService.MonthResolver{
    public class Resolver:IMonthResolver
    {
        public string GetName(int year,int month){
            return MonthNameResolver.MonthName.GetName(year,month);
        }
    }
}