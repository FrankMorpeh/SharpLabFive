using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates
{
    public interface IFactoryWorkPageProductionButtonState
    {
        void PauseOrContinueProduction(Factory factory, Button oauseOrContinueProductionButton
            , ref IFactoryWorkPageProductionButtonState factoryWorkPageProductionButtonState);
    }
}