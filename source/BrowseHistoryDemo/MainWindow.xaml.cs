﻿namespace BrowseHistoryDemo
{
    using BrowserHistoryDemoLib.ViewModels;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var destinations = new[] {
               @"C:\",
               @"F:\Program Files\My Program\My  Dir 1\My  Dir 2",
               @"F:\",
               @"F:\Temp",
               @"F:\Temp\More Files",
               @"G:\",
               @"H:\" };

            if (this.DataContext is AppViewModel viewmodel)
            {
                foreach (var item in destinations)
                    viewmodel.NaviHistory.Forward(new PathItem(item));
            }
        }
    }
}
