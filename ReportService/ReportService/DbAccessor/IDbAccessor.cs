using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.DbAccessor
{
    public interface IDbAccessor
    {
        void Connect();
        void Close();
        Task<IEnumerable<T>> QueryAsync<T>(string cmdText);
    }
}
