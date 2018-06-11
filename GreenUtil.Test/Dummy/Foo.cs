using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Dummy
{
    public class Foo
    {
        private int intField;

        private string stringField;

        private decimal decimalField;

        public int IntProp { get { return intField; } set{ intField = value; } }

        public string StringProp { get { return stringField; } set { stringField = value; } }

        public decimal DecimalProp { get { return decimalField; } set { decimalField = value; } }
        
    }
}
