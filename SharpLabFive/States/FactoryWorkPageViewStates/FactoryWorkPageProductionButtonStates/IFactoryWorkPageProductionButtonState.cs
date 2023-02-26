using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates
{
    public interface IFactoryWorkPageProductionButtonState
    {
        void StopOrContinueProduction(Factory factory, Button stopOrContinueProductionButton
            , ref IFactoryWorkPageProductionButtonState factoryWorkPageProductionButtonState);
    }
}