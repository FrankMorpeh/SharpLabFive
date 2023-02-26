using SharpLabFive.Converters.WorkshopConverters;
using SharpLabFive.Models.Workshops;
using SharpLabFive.Validators;
using SharpLabFour.Notification;
using System.Windows;
using System.Windows.Controls;

namespace SharpLabFive.Pages
{
    /// <summary>
    /// Interaction logic for WorkshopsPage.xaml
    /// </summary>
    public partial class WorkshopsPage : Page
    {
        private MainWindow itsContent;
        public WorkshopsPage(MainWindow content)
        {
            InitializeComponent();
            itsContent = content;
            DataContext = itsContent.factory;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            INotification warning = NumberOfGoodsPerDayValidator.CheckNumberOfGoodsPerDay(goodsPerDayTextBox.Text);
            if (warning is None)
                itsContent.factory.AddWorkshop(WorkshopConverter.ToWorkshop(goodsPerDayTextBox.Text));
            else
                NotificationView.ShowNotification(notificationStackPanel, notificationTextBlock, warning);
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            INotification warning = DataGridValidator.CheckSelectedItemInDataGrid(workshopsDataGrid);
            if (warning is None)
                itsContent.factory.RemoveWorkshop((Workshop)workshopsDataGrid.SelectedItem);
            else
                NotificationView.ShowNotification(notificationStackPanel, notificationTextBlock, warning);
        }
        private void StartWork_Click(object sender, RoutedEventArgs e)
        {
            itsContent.frame.Content = new FactoryWorkPage(itsContent);
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationView.HideNotifications(notificationStackPanel);
        }
    }
}