

namespace TargetCalculationInterfaces
{
    public interface ITargetCalculationFactory
    {
        TargetFormula GetTargetFormula();

        ITargetCalculationParameters CreateTargetCalculationParameters();

        ITargetCalculation Create();
    }
}
