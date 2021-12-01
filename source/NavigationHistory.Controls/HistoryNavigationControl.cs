using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HistoryControlLib.Controls
{
    public class HistoryNavigationControl : Control
    {

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

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(HistoryNavigationControl), new PropertyMetadata(null));

        public static readonly DependencyProperty LocationsProperty =
           DependencyProperty.Register("Locations", typeof(IEnumerable), typeof(HistoryNavigationControl), new PropertyMetadata(null));
       
        public static readonly DependencyProperty SelectionChangedProperty =
            DependencyProperty.Register("SelectionChanged", typeof(ICommand), typeof(HistoryNavigationControl), new PropertyMetadata(null));
        private LocationsDropDown locationsDropDown;

        static HistoryNavigationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HistoryNavigationControl), new FrameworkPropertyMetadata(typeof(HistoryNavigationControl)));
        }

        public override void OnApplyTemplate()
        {
            locationsDropDown = this.GetTemplateChild("LocationsDropDown") as LocationsDropDown;
            locationsDropDown.SelectionChanged += LocationsDropDown_SelectionChanged;
            base.OnApplyTemplate();
        }

        private void LocationsDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanged.Execute(e.AddedItems);
        }

        public ICommand UpCommand
        {
            get { return (ICommand)GetValue(UpCommandProperty); }
            set { SetValue(UpCommandProperty, value); }
        }

        public ICommand ForwardCommand
        {
            get { return (ICommand)GetValue(ForwardCommandProperty); }
            set { SetValue(ForwardCommandProperty, value); }
        }

        public ICommand BackwardCommand
        {
            get { return (ICommand)GetValue(BackwardCommandProperty); }
            set { SetValue(BackwardCommandProperty, value); }
        }

        public string BackwardPath
        {
            get { return (string)GetValue(BackwardPathProperty); }
            set { SetValue(BackwardPathProperty, value); }
        }

        public string ForwardPath
        {
            get { return (string)GetValue(ForwardPathProperty); }
            set { SetValue(ForwardPathProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public IEnumerable Locations
        {
            get { return (IEnumerable)GetValue(LocationsProperty); }
            set { SetValue(LocationsProperty, value); }
        }
        public ICommand SelectionChanged
        {
            get { return (ICommand)GetValue(SelectionChangedProperty); }
            set { SetValue(SelectionChangedProperty, value); }
        }


    }
}
