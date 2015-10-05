using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public class MSAsserter : IAsserter
    {
        public void IsEqual(double expected, double actual, double delta)
        {
            if (Math.Abs(expected - actual) > delta)
                throw new Exception("Assert failed");
        }
        public void IsEqual(bool expected, bool actual)
        {
            if (expected ^ actual)
                throw new Exception("Assert failed");
        }
    }
}
