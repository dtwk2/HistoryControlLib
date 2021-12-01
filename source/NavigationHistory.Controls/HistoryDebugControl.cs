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
    public class HistoryDebugControl : Control
    {

        public static readonly DependencyProperty ForwardLocationCommandProperty =
            DependencyProperty.Register("ForwardLocationCommand", typeof(ICommand), typeof(HistoryDebugControl), new PropertyMetadata(null));
        public static readonly DependencyProperty LocationsProperty =
            DependencyProperty.Register("Locations", typeof(IEnumerable), typeof(HistoryDebugControl), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedLocationProperty =
            DependencyProperty.Register("SelectedLocation", typeof(object), typeof(HistoryDebugControl), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register("SelectedPath", typeof(string), typeof(HistoryDebugControl), new PropertyMetadata(null));


        static HistoryDebugControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HistoryDebugControl), new FrameworkPropertyMetadata(typeof(HistoryDebugControl)));
        }

        public ICommand ForwardLocationCommand
        {
            get { return (ICommand)GetValue(ForwardLocationCommandProperty); }
            set { SetValue(ForwardLocationCommandProperty, value); }
        }
        public IEnumerable Locations
        {
            get { return (IEnumerable)GetValue(LocationsProperty); }
            set { SetValue(LocationsProperty, value); }
        }
        public object SelectedLocation
        {
            get { return (object)GetValue(SelectedLocationProperty); }
            set { SetValue(SelectedLocationProperty, value); }
        }
        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

    }
}
