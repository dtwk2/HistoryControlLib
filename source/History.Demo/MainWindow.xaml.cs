using BrowserHistoryDemoLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace History.Demo
{
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
