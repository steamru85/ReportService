using System.Data.Common;

namespace ReportService.DbAccessor
{
    public interface IDbAccessor
    {
        void Connect();
        void Close();
        DbDataReader ExecuteCommand(string cmdText);
    }
}
