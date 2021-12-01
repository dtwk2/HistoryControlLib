using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BrowseHistory.Controls {

   public delegate void HistoryNavigationEventHandler(object source, HistoryNavigationEventArgs eventArgs);

   public class HistoryNavigationEventArgs : RoutedEventArgs {
      public HistoryNavigationEventArgs(RoutedEvent routedEvent, object source, int movement, int backwardCount, int forwardCount) : base(routedEvent, source) {
         Movement = movement;
      }

      public int Movement { get; }
   }

   public class HistoryNavigationControl : Control {

      private LocationsDropDown locationsDropDown;
      private Button? forwardButton;
      private Button? backwardButton;

      public static readonly DependencyProperty UpCommandProperty =
      DependencyProperty.Register("UpCommand", typeof(ICommand), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty ForwardCommandProperty =
         DependencyProperty.Register("ForwardCommand", typeof(ICommand), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty BackwardCommandProperty =
         DependencyProperty.Register("BackwardCommand", typeof(ICommand), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty BackwardPathProperty =
         DependencyProperty.Register("BackwardPath", typeof(string), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty ForwardPathProperty =
          DependencyProperty.Register("ForwardPath", typeof(string), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty BackwardCountProperty =
          DependencyProperty.Register("BackwardCount", typeof(int), typeof(HistoryNavigationControl), new PropertyMetadata(0, Changed));

      public static readonly DependencyProperty ForwardCountProperty =
        DependencyProperty.Register("ForwardCount", typeof(int), typeof(HistoryNavigationControl), new PropertyMetadata(0));

      public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem", typeof(object), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty LocationsProperty =
         DependencyProperty.Register("Locations", typeof(IEnumerable), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty SelectionChangedProperty =
          DependencyProperty.Register("SelectionChanged", typeof(ICommand), typeof(HistoryNavigationControl), new PropertyMetadata(null));

      public static readonly DependencyProperty ShowIndicesProperty =
          DependencyProperty.Register("ShowIndices", typeof(bool), typeof(HistoryNavigationControl), new PropertyMetadata(true));

      public static readonly RoutedEvent NavigationEvent = EventManager.RegisterRoutedEvent(nameof(Complete), RoutingStrategy.Bubble, typeof(HistoryNavigationEventHandler), typeof(HistoryNavigationControl));



      private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
         if (d is HistoryNavigationControl cee) {

         }
      }


      static HistoryNavigationControl() {
         DefaultStyleKeyProperty.OverrideMetadata(typeof(HistoryNavigationControl), new FrameworkPropertyMetadata(typeof(HistoryNavigationControl)));
      }

      public override void OnApplyTemplate() {
         locationsDropDown = GetTemplateChild("LocationsDropDown") as LocationsDropDown;
         forwardButton = GetTemplateChild("ForwardButton") as Button;
         if (forwardButton is Button)
            forwardButton.Click += ForwardButton_Click;
         backwardButton = GetTemplateChild("ForwardButton") as Button;
         if (backwardButton is Button)
            backwardButton.Click += BackwardButton_Click;    
  

         locationsDropDown.SelectionChanged += LocationsDropDown_SelectionChanged;
         base.OnApplyTemplate();
      }

      private void ForwardButton_Click(object sender, RoutedEventArgs e) {
         this.RaiseEvent(new HistoryNavigationEventArgs(NavigationEvent, this, 1, BackwardCount, ForwardCount));
      }

      private void BackwardButton_Click(object sender, RoutedEventArgs e) {
         this.RaiseEvent(new HistoryNavigationEventArgs(NavigationEvent, this, 1, BackwardCount, ForwardCount));
      }

      private void LocationsDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e) {
         SelectionChanged.Execute(e.AddedItems);
      }

      #region properties
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
      public int ForwardCount {
         get { return (int)GetValue(ForwardCountProperty); }
         set { SetValue(ForwardCountProperty, value); }
      }
      public int BackwardCount {
         get { return (int)GetValue(BackwardCountProperty); }
         set { SetValue(BackwardCountProperty, value); }
      }

      public bool ShowIndices {
         get { return (bool)GetValue(ShowIndicesProperty); }
         set { SetValue(ShowIndicesProperty, value); }
      }

      public object SelectedItem {
         get { return GetValue(SelectedItemProperty); }
         set { SetValue(SelectedItemProperty, value); }
      }

      public IEnumerable Locations {
         get { return (IEnumerable)GetValue(LocationsProperty); }
         set { SetValue(LocationsProperty, value); }
      }
      public ICommand SelectionChanged {
         get { return (ICommand)GetValue(SelectionChangedProperty); }
         set { SetValue(SelectionChangedProperty, value); }
      }

      public event HistoryNavigationEventHandler Complete {
         add => this.AddHandler(HistoryNavigationControl.NavigationEvent, value);
         remove => this.RemoveHandler(HistoryNavigationControl.NavigationEvent, value);
      }

      #endregion properties

   }
}
