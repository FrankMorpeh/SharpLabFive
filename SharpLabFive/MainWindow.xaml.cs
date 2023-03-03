using SharpLabFive.Controllers.WorkshopControllers;
using SharpLabFive.Models.Workshops;
using SharpLabFive.Pages;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SharpLabFive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string initialLocation;
        public Factory factory;

        static MainWindow()
        {
            initialLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            initialLocation = System.IO.Path.GetDirectoryName(initialLocation);
        }
        public MainWindow()
        {
            InitializeComponent();
            Directories.DirectoriesChecker.CreateDirectoriesIfNeeded();
            factory = new Factory();
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            frame.Content = new WorkshopsPage(this);
        }
    }
}