using SharpLabFive.Controllers.WorkshopControllers;
using SharpLabFive.Models.Workshops;
using System.Collections.Generic;
using System.Windows;

namespace SharpLabFive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Factory factory;
        public MainWindow()
        {
            InitializeComponent();
            factory = new Factory(new List<Workshop>() { new Workshop(10), new Workshop(5), new Workshop(3) });
            factory.SellGoodsAsParallel(1);
            factory.MakeGoodsAsParallel();
            factory.BuyResourcesAndPaySalariesAsParallel();
        }
    }
}