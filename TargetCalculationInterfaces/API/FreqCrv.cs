using System;
using System.Collections.Generic;
using System.Linq;

namespace TargetCalculationInterfaces
{
    public class FreqCrv : List<FreqPt>
    {
        public CurveType CurveType { get; }

        public FreqCrv(CurveType curveType = CurveType.NotDefined)
        {
            CurveType = curveType;
        }

        public FreqCrv(IEnumerable<FreqPt> freqPts, CurveType curveType = CurveType.NotDefined)
        {
            CurveType = curveType;

            if (freqPts != null)
                AddRange(freqPts);
        }

        public FreqCrv(IList<double> frequencies, IList<double> values, CurveType curveType = CurveType.NotDefined)
        {
            if(frequencies.Count != values.Count) 
                throw new ArgumentException($"Number of frequencies and values is not identical for curveType {curveType}");

            CurveType = curveType;

            for (var i = 0; i < frequencies.Count; i++)
                Add(new FreqPt(frequencies[i], values[i]));
        }

        public static FreqCrv operator +(FreqCrv a, double b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select(pt => pt.Y + b).ToList(), a.CurveType);
        }

        public static FreqCrv operator +(FreqCrv a, FreqCrv b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select((pt, index) => pt.Y + b[index].Y).ToList(), a.CurveType);
        }

        public static FreqCrv operator -(FreqCrv a, double b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select((pt, index) => pt.Y - b).ToList(), a.CurveType);
        }

        public static FreqCrv operator -(double a, FreqCrv b)
        {
            return new FreqCrv(b.Select(pt => pt.X).ToList(), b.Select((pt, index) => a- pt.Y).ToList(), b.CurveType);
        }

        public static FreqCrv operator -(FreqCrv a, FreqCrv b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select((pt, index) => pt.Y - b[index].Y).ToList(), a.CurveType);
        }

        public static FreqCrv operator *(FreqCrv a, double b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select(pt => pt.Y * b).ToList(), a.CurveType);
        }

        public static FreqCrv operator *(FreqCrv a, FreqCrv b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select((pt, index) => pt.Y * b[index].Y).ToList(), a.CurveType);
        }

        public static FreqCrv operator /(FreqCrv a, double b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select(pt => pt.Y / b).ToList(), a.CurveType);
        }

        public static FreqCrv operator /(FreqCrv a, FreqCrv b)
        {
            return new FreqCrv(a.Select(pt => pt.X).ToList(), a.Select((pt, index) => pt.Y / b[index].Y).ToList(), a.CurveType);
        }

        public double GetValue(double frequency)
        {
            return this.First(pt => Math.Abs(pt.X - frequency) < 0.001).Y;
        }

        public void SetValue(double frequency, double value)
        {
            var index = FindIndex(pt => Math.Abs(pt.X - frequency) < 0.001);

            if (index >= 0)
                this[index] = new FreqPt(frequency, value);
            else
            {
                Add(new FreqPt(frequency, value));
                Sort((pt1, pt2) => pt1.X.CompareTo(pt2.X));
            }
        }

        public double PTA()
        {
            return (GetValue(500) + GetValue(1000) + GetValue(2000)) / 3;
        }

        public FreqCrv THTL(double xHTL, double xPTA)
        {
            return (this * xHTL) + PTA() * xPTA;
        }

        public FreqCrv THTL_Limited(double xHTL, double xPTA)
        {
            var tHTL = THTL(xHTL, xPTA);
            var tHTLLimitLow = tHTL.Select(pt => new FreqPt(pt.X, Math.Max(pt.Y, 20))).ToList();
            var tHTLLimitedLowHigh = tHTLLimitLow.Select(pt => new FreqPt(pt.X, Math.Min(pt.Y, 100))).ToList();
            
            return new FreqCrv(tHTLLimitedLowHigh, CurveType);
        }
    }
}
