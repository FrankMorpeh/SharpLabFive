using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates
{
    public class FactoryWorkPageProductionPausedButtonState : IFactoryWorkPageProductionButtonState
    {
        public void PauseOrContinueProduction(Factory factory, Button pauseOrContinueProductionButton
            , ref IFactoryWorkPageProductionButtonState factoryWorkPageProductionButtonState)
        {
            factory.ContinueMakingGoods();
            pauseOrContinueProductionButton.Content = "PAUSE PRODUCTION";
            factoryWorkPageProductionButtonState = new FactoryWorkPageProductionWorkingButtonState();
        }
    }
}