

namespace TargetCalculationInterfaces
{
    public interface ITargetCalculationFactory
    {
        TargetFormula GetTargetFormula();

        ITargetCalculation Create();
    }
}
