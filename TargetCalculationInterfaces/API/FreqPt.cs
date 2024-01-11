
using System;

namespace TargetCalculationInterfaces
{
    public class FreqPt
    {
        public FreqPt(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Y { get; }

        public double X { get; }
    }
}
