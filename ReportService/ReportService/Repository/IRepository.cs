using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
    }
}
