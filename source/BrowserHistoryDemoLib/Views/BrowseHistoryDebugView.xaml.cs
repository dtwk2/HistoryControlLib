using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrowserHistoryDemoLib.Views {
   /// <summary>
   /// Interaction logic for BrowseHistoryDebugView.xaml
   /// </summary>
   public partial class BrowseHistoryDebugView : UserControl {

      public static readonly DependencyProperty ForwardLocationCommandProperty =
      DependencyProperty.Register("ForwardLocationCommand", typeof(ICommand), typeof(BrowseHistoryDebugView), new PropertyMetadata(null));
      public static readonly DependencyProperty LocationsProperty =
      DependencyProperty.Register("Locations", typeof(IEnumerable), typeof(BrowseHistoryDebugView), new PropertyMetadata(null));
      public static readonly DependencyProperty SelectedLocationProperty =
      DependencyProperty.Register("SelectedLocation", typeof(object), typeof(BrowseHistoryDebugView), new PropertyMetadata(null));
      public static readonly DependencyProperty SelectedPathProperty =
      DependencyProperty.Register("SelectedPath", typeof(string), typeof(BrowseHistoryDebugView), new PropertyMetadata(null));


      public BrowseHistoryDebugView() {
         InitializeComponent();
           // MainGrid.DataContext = this;
      }

  
      public ICommand ForwardLocationCommand {
         get { return (ICommand)GetValue(ForwardLocationCommandProperty); }
         set { SetValue(ForwardLocationCommandProperty, value); }
      }
      public IEnumerable Locations {
         get { return (IEnumerable)GetValue(LocationsProperty); }
         set { SetValue(LocationsProperty, value); }
      }
      public object SelectedLocation {
         get { return (object)GetValue(SelectedLocationProperty); }
         set { SetValue(SelectedLocationProperty, value); }
      }
      public string SelectedPath {
         get { return (string)GetValue(SelectedPathProperty); }
         set { SetValue(SelectedPathProperty, value); }
      }




      //<TextBox
      //                 Text = "{Binding NaviHistory.SelectedItem.Path,Mode=OneWay,UpdateSourceTrigger=PropertyChanged}"
      //                 Name="NewPathInputBox"
      //                 VerticalAlignment="Center"
      //                 VerticalContentAlignment="Center"
      //                 Grid.Column="1"
      //                />
      //    <Button Grid.Column="2" Content= "Go" Padding= "3" Margin= "3" MinHeight= "23"
      //                Command= "{Binding ForwardLocationCommand}"
      //                CommandParameter= "{Binding ElementName=NewPathInputBox,Path=Text}"
      //                />

      //    < Label Grid.Row= "1" Grid.Column= "0" Content= "Locations:" HorizontalAlignment= "Right" />
      //    < ListBox Grid.Row= "1" Grid.Column= "1" Grid.ColumnSpan= "2"
      //                 ItemsSource= "{Binding NaviHistory.Locations, Mode=OneWay,UpdateSourceTrigger=PropertyChanged}"
      //                 IsEnabled= "False"
      //                 SelectedItem= "{Binding NaviHistory.SelectedItem,Mode=OneWay,UpdateSourceTrigger=PropertyChanged}"
      //                >
      //        < ListBox.ItemTemplate >
      //            < DataTemplate >
      //                < TextBlock Text= "{Binding Path}" />
      //            </ DataTemplate >
      //        </ ListBox.ItemTemplate >
   }
}
