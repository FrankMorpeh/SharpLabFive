using SharpLabFive.Controllers.WorkshopControllers;
using System.Windows.Controls;

namespace SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageSellingButtonStates
{
    public interface IFactoryWorkPageSellingButtonState
    {
        void PauseOrContinueSelling(Factory factory, Button pauseOrContinueSellingButton
            , ref IFactoryWorkPageSellingButtonState factoryWorkPageSellingButtonState);
    }
}