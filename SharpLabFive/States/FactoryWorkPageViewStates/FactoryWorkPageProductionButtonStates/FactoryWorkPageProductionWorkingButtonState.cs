using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates
{
    internal class FactoryWorkPageProductionWorkingButtonState : IFactoryWorkPageProductionButtonState
    {
        public void PauseOrContinueProduction(Factory factory, Button pauseOrContinueProductionButton
            , ref IFactoryWorkPageProductionButtonState factoryWorkPageProductionButtonState)
        {
            factory.PauseMakingGoods();
            pauseOrContinueProductionButton.Content = "CONTINUE PRODUCTION";
            factoryWorkPageProductionButtonState = new FactoryWorkPageProductionPausedButtonState();
        }
    }
}