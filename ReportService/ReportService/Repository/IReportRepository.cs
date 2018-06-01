using System.Collections.Generic;

namespace ReportService.Repository
{
    public interface IReportRepository<T>
    {
        IEnumerable<T> Get();
    }
}
