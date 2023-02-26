using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageSellingButtonStates
{
    public class FactoryWorkPageSellingWorkingButtonState : IFactoryWorkPageSellingButtonState
    {
        public void PauseOrContinueSelling(Factory factory, Button pauseOrContinueSellingButton
            , ref IFactoryWorkPageSellingButtonState factoryWorkPageSellingButtonState)
        {
            factory.PauseSellingGoods();
            pauseOrContinueSellingButton.Content = "CONTINUE SELLING";
            factoryWorkPageSellingButtonState = new FactoryWorkPageSellingStoppedButtonState();
        }
    }
}