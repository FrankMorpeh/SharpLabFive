using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageSellingButtonStates
{
    public class FactoryWorkPageSellingStoppedButtonState : IFactoryWorkPageSellingButtonState
    {
        public void PauseOrContinueSelling(Factory factory, Button pauseOrContinueSellingButton
            , ref IFactoryWorkPageSellingButtonState factoryWorkPageSellingButtonState)
        {
            factory.ContinueSellingGoods();
            pauseOrContinueSellingButton.Content = "PAUSE SELLING";
            factoryWorkPageSellingButtonState = new FactoryWorkPageSellingWorkingButtonState();
        }
    }
}