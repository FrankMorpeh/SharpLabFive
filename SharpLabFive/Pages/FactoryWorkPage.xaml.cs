using SharpLabFive.Converters.TimeConverters;
using SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates;
using SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageSellingButtonStates;
using System.Windows;
using System.Windows.Controls;

namespace SharpLabFive.Pages
{
    /// <summary>
    /// Interaction logic for FactoryWorkPage.xaml
    /// </summary>
    public partial class FactoryWorkPage : Page
    {
        private MainWindow itsContent;
        private IFactoryWorkPageProductionButtonState itsProductionButtonState;
        private IFactoryWorkPageSellingButtonState itsSellingButtonState;
        public FactoryWorkPage(MainWindow content)
        {
            InitializeComponent();
            InitializeExtraComponents();
            itsContent = content;
            DataContext = itsContent.factory;

            itsProductionButtonState = new FactoryWorkPageProductionWorkingButtonState();
            itsSellingButtonState = new FactoryWorkPageSellingWorkingButtonState();

            itsContent.factory.MakeGoodsAsParallel();
            itsContent.factory.SellGoodsAsParallel();
            itsContent.factory.BuyResourcesAndPaySalariesAsParallel();
        }
        private void InitializeExtraComponents()
        {
            sellingTimeUpDown.Minimum = 1; sellingTimeUpDown.Value = 1;
        }

        private void StopOrContinueProduction_Click(object sender, RoutedEventArgs e)
        {
            itsProductionButtonState.PauseOrContinueProduction(itsContent.factory, pauseOrContinueProductionButton
                , ref itsProductionButtonState);
        }
        private void StopOrContinueSelling_Click(object sender, RoutedEventArgs e)
        {
            itsSellingButtonState.PauseOrContinueSelling(itsContent.factory, pauseOrContinueSellingButton, ref itsSellingButtonState);
        }
        private void ChangeTime_Click(object sender, RoutedEventArgs e)
        {
            itsContent.factory.SellingTime = TimeConverter.ToMillisecondsFromSeconds(sellingTimeUpDown.Value);
        }
        private void FinishWork_Click(object sender, RoutedEventArgs e)
        {
            itsContent.factory.StopMakingGoodsAsParallel();
            itsContent.factory.StopSellingGoodsAsParallel();
            itsContent.factory.StopBuyingResourcesAndPaySalariesAsParallel();
        }
    }
}