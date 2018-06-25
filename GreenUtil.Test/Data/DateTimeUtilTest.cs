using GreenUtil.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Data
{
    [TestClass]
    public class DateTimeUtilTest
    {
        [TestMethod]
        public void WhenDateTimeIsMinDateThenIsValidSQLDateTimeShouldReturnFalse()
        {
            Assert.IsFalse(DateTimeUtil.IsValidSQLDateTime(DateTime.MinValue));
        }

        [TestMethod]
        public void WhenDateTimeIsLessThanSqlMinValueThenIsValidSQLDateTimeShouldReturnFalse()
        {
            Assert.IsFalse(DateTimeUtil.IsValidSQLDateTime(SqlDateTime.MinValue.Value.AddTicks(-1)));
        }

        [TestMethod]
        public void WhenDateTimeIsEqualThanSqlMinValueThenIsValidSQLDateTimeShouldReturnTrue()
        {
            Assert.IsTrue(DateTimeUtil.IsValidSQLDateTime(SqlDateTime.MinValue.Value));
        }

        [TestMethod]
        public void WhenDateTimeIsMaxDateThenIsValidSQLDateTimeShouldReturnFalse()
        {
            Assert.IsFalse(DateTimeUtil.IsValidSQLDateTime(DateTime.MaxValue));
        }

        [TestMethod]
        public void WhenDateTimeIsGreaterThanSqlMaxValueThenIsValidSQLDateTimeShouldReturnFalse()
        {
            Assert.IsFalse(DateTimeUtil.IsValidSQLDateTime(SqlDateTime.MaxValue.Value.AddTicks(1)));
        }

        [TestMethod]
        public void WhenDateTimeIsEqualThanSqlMaxValueThenIsValidSQLDateTimeShouldReturnTrue()
        {
            Assert.IsTrue(DateTimeUtil.IsValidSQLDateTime(SqlDateTime.MaxValue.Value));
        }
    }
}
