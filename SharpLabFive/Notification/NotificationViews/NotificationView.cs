using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;

namespace SharpLabFour.Notification
{
    public static class NotificationView
    {
        public static void ShowNotification(StackPanel notificationPanel, TextBlock notificationTextBlock, INotification notification)
        {
            notificationTextBlock.Text = notification.Text;
            notificationPanel.Visibility = Visibility.Visible;
        }
        public static void ShowNotifications(StackPanel notificationPanel, TextBlock notificationTextBlock
            , List<INotification> notifications)
        {
            notificationTextBlock.Text = string.Empty; // clear previous text if there is some
            notificationTextBlock.Text += "SOME ERRORS OCCURED:\r\n\r\n";
            foreach (INotification notification in notifications)
                notificationTextBlock.Text += "- " + notification.Text + "\r\n";
            notificationPanel.Visibility = Visibility.Visible;
        }
        public static void HideNotifications(StackPanel notificationPanel)
        {
            notificationPanel.Visibility = Visibility.Hidden;
        }
    }
}