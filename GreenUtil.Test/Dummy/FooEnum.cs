using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Dummy
{
    public enum FooEnum
    {
        [Description("Description for Value 1")]
        Value1,

        [Description("Description for Value 2")]
        Value2,

        [Description("Description for Value 3")]
        Value3
    }
}
