using System;
using System.Collections.Generic;
using System.Text;

namespace Module.A
{
    public class TestClassA : ITestClassA
    {
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }
}
