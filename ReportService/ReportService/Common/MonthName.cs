using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace ReportService.Common
{
    public class MonthNameResolver
    {
        public static string GetName(int year, int monthNum)
        {
            return new DateTime(year, monthNum, 1).ToString("MMMMMM", CultureInfo.CurrentCulture);
        }
    }
}
