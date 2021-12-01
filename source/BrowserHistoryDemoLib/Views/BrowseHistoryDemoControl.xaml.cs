namespace BrowserHistoryDemoLib.Views {
   using System.Collections;
   using System.Windows;
   using System.Windows.Controls;
   using System.Windows.Input;

   /// <summary>
   /// Interaction logic for BrowseHistoryDemoView.xaml
   /// </summary>
   public partial class BrowseHistoryDemoControl : UserControl {

      public static readonly DependencyProperty UpCommandProperty =
         DependencyProperty.Register("UpCommand", typeof(ICommand), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));
      
      public static readonly DependencyProperty ForwardCommandProperty =
         DependencyProperty.Register("ForwardCommand", typeof(ICommand), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));

      public static readonly DependencyProperty BackwardCommandProperty =
         DependencyProperty.Register("BackwardCommand", typeof(ICommand), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));
    
      public static readonly DependencyProperty BackwardPathProperty =
         DependencyProperty.Register("BackwardPath", typeof(string), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));

      public static readonly DependencyProperty ForwardPathProperty =
          DependencyProperty.Register("ForwardPath", typeof(string), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));

      public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem", typeof(object), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));

      public static readonly DependencyProperty LocationsProperty =
         DependencyProperty.Register("Locations", typeof(IEnumerable), typeof(BrowseHistoryDemoControl), new PropertyMetadata(null));


      public BrowseHistoryDemoControl() {
         InitializeComponent();
            MainGrid.DataContext = this;


        }

        public ICommand UpCommand {
         get { return (ICommand)GetValue(UpCommandProperty); }
         set { SetValue(UpCommandProperty, value); }
      }

      public ICommand ForwardCommand {
         get { return (ICommand)GetValue(ForwardCommandProperty); }
         set { SetValue(ForwardCommandProperty, value); }
      }

      public ICommand BackwardCommand {
         get { return (ICommand)GetValue(BackwardCommandProperty); }
         set { SetValue(BackwardCommandProperty, value); }
      }

      public string BackwardPath {
         get { return (string)GetValue(BackwardPathProperty); }
         set { SetValue(BackwardPathProperty, value); }
      }

      public string ForwardPath {
         get { return (string)GetValue(ForwardPathProperty); }
         set { SetValue(ForwardPathProperty, value); }
      }

      public object SelectedItem {
         get { return (object)GetValue(SelectedItemProperty); }
         set { SetValue(SelectedItemProperty, value); }
      }

      public IEnumerable Locations {
         get { return (IEnumerable)GetValue(LocationsProperty); }
         set { SetValue(LocationsProperty, value); }
      }
   }



   //  <Button Grid.Column="0"
   //                Margin= "3,3,0,3"
   //                Command= "{Binding BackwardCommand}"
   //                HorizontalAlignment= "Right" VerticalAlignment= "Stretch"
   //                HorizontalContentAlignment= "Right" VerticalContentAlignment= "Stretch"
   //                Style= "{DynamicResource {x:Static BHreskeys:ResourceKeys.HistoryButtonStyleKey}}" >
   //        < Button.ToolTip >
   //            < TextBlock >
   //                    < TextBlock Text= "Back to: " />
   //                    < TextBlock Text= "{Binding NaviHistory.NextBackwardItem.Path}" />
   //                </ TextBlock >
   //        </ Button.ToolTip >
   //        < Button.Content >
   //            < Grid >
   //                < Path HorizontalAlignment= "Center"
   //                          Data= "{DynamicResource {x:Static BHreskeys:ResourceKeys.ArrowGeometryKey}}"
   //                          Fill= "{Binding Foreground,RelativeSource={RelativeSource AncestorType=Button}}"
   //                          RenderTransformOrigin= ".5,.5"
   //                          Stretch= "Uniform" >
   //                    < Path.RenderTransform >
   //                        < TransformGroup >
   //                            < RotateTransform Angle= "180" />
   //                            < ScaleTransform ScaleX= "0.75" ScaleY= "0.75" />
   //                        </ TransformGroup >
   //                    </ Path.RenderTransform >
   //                </ Path >
   //            </ Grid >
   //        </ Button.Content >
   //    </ Button >

   //    < Button Grid.Column= "1"
   //                Margin= "0,3,0,3"
   //                Command= "{Binding ForwardCommand}"
   //                HorizontalAlignment= "Left" VerticalAlignment= "Stretch"
   //                HorizontalContentAlignment= "Left" VerticalContentAlignment= "Stretch"
   //                Style= "{DynamicResource {x:Static BHreskeys:ResourceKeys.HistoryButtonStyleKey}}"
   //            >
   //        < Button.ToolTip >
   //            < TextBlock >
   //                    < TextBlock Text= "Forward to: " />
   //                    < TextBlock Text= "{Binding NaviHistory.NextForwardItem.Path}" />
   //                </ TextBlock >
   //        </ Button.ToolTip >
   //        < Button.Content >
   //            < Grid >
   //                < Path HorizontalAlignment= "Center"
   //                          Data= "{DynamicResource {x:Static BHreskeys:ResourceKeys.ArrowGeometryKey}}"
   //                          Fill= "{Binding Foreground,RelativeSource={RelativeSource AncestorType=Button}}"
   //                          RenderTransformOrigin= ".5,.5"
   //                          Stretch= "Uniform" >
   //                    < Path.RenderTransform >
   //                        < TransformGroup >
   //                            < RotateTransform Angle= "0" />
   //                            < ScaleTransform ScaleX= "0.7" ScaleY= "0.7" />
   //                        </ TransformGroup >
   //                    </ Path.RenderTransform >
   //                </ Path >
   //            </ Grid >
   //        </ Button.Content >
   //    </ Button >

   //    < controls:LocationsDropDown
   //            Grid.Column= "2"
   //            ItemsSource= "{Binding NaviHistory.Locations}"
   //            HorizontalAlignment= "Left"
   //            behav:SelectionChangedCommand.ChangedCommand= "{Binding SelectionChanged}"
   //            SelectedItem= "{Binding NaviHistory.SelectedItem,Mode=OneWay,UpdateSourceTrigger=PropertyChanged}"
   //            ToolTip= "Recent Locations"
   //        >
   //        < controls:LocationsDropDown.ItemTemplate>
   //            <DataTemplate>
   //                <TextBlock Text = "{Binding Path}" />
   //            </ DataTemplate >
   //        </ controls:LocationsDropDown.ItemTemplate>
   //    </controls:LocationsDropDown>

   //    <Button Grid.Column= "3" Grid.Row= "0"
   //                Margin= "3"
   //                Command= "{Binding UpCommand}"
   //                ToolTip= "Up"
   //                HorizontalAlignment= "Center" VerticalAlignment= "Stretch"
   //                HorizontalContentAlignment= "Center" VerticalContentAlignment= "Stretch"
   //                Style= "{DynamicResource {x:Static BHreskeys:ResourceKeys.HistoryButtonStyleKey}}"
   //            >
   //        < Button.Content >
   //            < Grid >
   //                < Path HorizontalAlignment= "Right"
   //                          Data= "{DynamicResource {x:Static BHreskeys:ResourceKeys.ArrowGeometryKey}}"
   //                          Fill= "{Binding Foreground,RelativeSource={RelativeSource AncestorType=Button}}"
   //                          RenderTransformOrigin= ".5,.5"
   //                          Stretch= "Uniform" >
   //                    < Path.RenderTransform >
   //                        < TransformGroup >
   //                            < RotateTransform Angle= "270" />
   //                            < ScaleTransform ScaleX= "0.7" ScaleY= "0.7" />
   //                        </ TransformGroup >
   //                    </ Path.RenderTransform >
   //                </ Path >
   //            </ Grid >
   //        </ Button.Content >
   //    </ Button >
   //</ Grid >
}
