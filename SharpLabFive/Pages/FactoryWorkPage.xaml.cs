using SharpLabFive.Converters.TimeConverters;
using SharpLabFive.States.FactoryWorkPageViewStates.FactoryWorkPageProductionButtonStates;
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
        public FactoryWorkPage(MainWindow content)
        {
            InitializeComponent();
            InitializeExtraComponents();
            itsContent = content;
            DataContext = itsContent.factory;
            itsContent.factory.MakeGoodsAsParallel();
            itsContent.factory.SellGoodsAsParallel();
            itsContent.factory.BuyResourcesAndPaySalariesAsParallel();
            itsProductionButtonState = new FactoryWorkPageProductionWorkingButtonState();
        }
        private void InitializeExtraComponents()
        {
            sellingTimeUpDown.Minimum = 1; sellingTimeUpDown.Value = 1;
        }

        private void StopOrContinueProduction_Click(object sender, RoutedEventArgs e)
        {
            itsProductionButtonState.StopOrContinueProduction(itsContent.factory, stopOrContinueProductionButton
                , ref itsProductionButtonState);
        }
        private void ChangeTime_Click(object sender, RoutedEventArgs e)
        {
            itsContent.factory.SellingTime = TimeConverter.ToMillisecondsFromSeconds(sellingTimeUpDown.Value);
        }
    }
}