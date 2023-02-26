using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates
{
    internal class FactoryWorkPageProductionWorkingButtonState : IFactoryWorkPageProductionButtonState
    {
        public void StopOrContinueProduction(Factory factory, Button stopOrContinueProductionButton
            , ref IFactoryWorkPageProductionButtonState factoryWorkPageProductionButtonState)
        {
            factory.StopMakingGoods();
            stopOrContinueProductionButton.Content = "CONTINUE PRODUCTION";
            factoryWorkPageProductionButtonState = new FactoryWorkPageProductionStoppedButtonState();
        }
    }
}