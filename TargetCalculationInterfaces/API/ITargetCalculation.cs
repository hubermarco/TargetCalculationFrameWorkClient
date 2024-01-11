using System.Collections.Generic;

namespace TargetCalculationInterfaces
{
    public interface ITargetCalculation
    {
        IList<FreqCrv> Calculate(ITargetCalculationParameters targetCalculationParameters);
    }
}
