using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Data
{
    /// <summary>
    /// Logic relate do DateTime
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// Determines whether a DateTime is a valid SQL DateTime
        /// </summary>
        /// <param name="date">The date to be evaluated</param>
        /// <returns>True if valid SQL DateTime, false otherwise</returns>
        public static bool IsValidSQLDateTime(this DateTime date)
        {
            return date >= SqlDateTime.MinValue.Value && date <= SqlDateTime.MaxValue.Value;
        }
    }
}
