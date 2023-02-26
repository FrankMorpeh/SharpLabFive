using System.Windows.Controls;

namespace SharpLabFive.Pages
{
    /// <summary>
    /// Interaction logic for FactoryWorkPage.xaml
    /// </summary>
    public partial class FactoryWorkPage : Page
    {
        private MainWindow itsContent;
        public FactoryWorkPage(MainWindow content)
        {
            InitializeComponent();
            itsContent = content;
        }
    }
}