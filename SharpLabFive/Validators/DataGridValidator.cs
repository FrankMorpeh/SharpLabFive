using SharpLabFour.Notification;
using System.Windows.Controls;

namespace SharpLabFive.Validators
{
    public static class DataGridValidator
    {
        public static INotification CheckSelectedItemInDataGrid(DataGrid dataGrid)
        {
            if (dataGrid.SelectedIndex == -1)
                return new RecordNotChosen();
            else
                return new None();
        }
    }
}