using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates
{
    public class FactoryWorkPageProductionStoppedButtonState : IFactoryWorkPageProductionButtonState
    {
        public void StopOrContinueProduction(Factory factory, Button stopOrContinueProductionButton
            , ref IFactoryWorkPageProductionButtonState factoryWorkPageProductionButtonState)
        {
            factory.ContinueMakingGoods();
            stopOrContinueProductionButton.Content = "STOP PRODUCTION";
            factoryWorkPageProductionButtonState = new FactoryWorkPageProductionWorkingButtonState();
        }
    }
}