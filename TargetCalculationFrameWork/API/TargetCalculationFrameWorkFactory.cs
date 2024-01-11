
namespace TargetCalculationFrameWork
{
    public class TargetCalculationFrameWorkFactory
    {
        public static ITargetCalculationFrameWork Create()
        {
            return new TargetCalculationFrameWork();
        }
    }
}
