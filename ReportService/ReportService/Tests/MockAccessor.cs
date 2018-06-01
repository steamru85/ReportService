using System.Data.Common;
using ReportService.DbAccessor;

namespace ReportService.Tests
{
    public class MockAccessor : IDbAccessor
    {
        public MockAccessor()
        {
        }

        public void Connect()
        {
        }

        public void Close()
        {
        }

        public DbDataReader ExecuteCommand(string cmdText)
        {
            return null;
        }
    }
}
