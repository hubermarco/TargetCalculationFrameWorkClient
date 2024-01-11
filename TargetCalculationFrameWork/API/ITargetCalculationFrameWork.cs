
using System.Collections.Generic;
using TargetCalculationInterfaces;

namespace TargetCalculationFrameWork
{
    public interface ITargetCalculationFrameWork
    {
        IList<TargetFormula> GetAvailableTargetFormulas();

        ITargetCalculation GetTargetCalculation(TargetFormula targetFormula);
    }
}
