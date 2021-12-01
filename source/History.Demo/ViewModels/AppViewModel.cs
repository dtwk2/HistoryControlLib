namespace BrowserHistoryDemoLib.ViewModels
{
    using BrowseHistory;
    using BrowserHistoryDemoLib.ViewModels.Base;
    using HistoryControlLib;
    using HistoryControlLib.Interfaces;
    using HistoryControlLib.ViewModels.Base;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;

    public class AppViewModel : BaseViewModel {

      private readonly IBrowseHistory<PathItem> NaviHistory;

      /// <summary>
      /// Class constructor.
      /// </summary>
      public AppViewModel() {

         NaviHistory = Factory.CreateBrowseNavigator();

         SelectionCommand = CreateSelectionChanged(NaviHistory);

         ForwardCommand = new RelayCommand<object>(_ => {
            if (NaviHistory.CanForward == false)
               throw new Exception("fse333d");
            NaviHistory.Forward();
         }, p => NaviHistory.CanForward);

         BackwardCommand = new RelayCommand<object>(_ => {
            if (NaviHistory.CanBackward == false)
               throw new Exception("fs3e33333d");
            NaviHistory.Backward();
         }, p => NaviHistory.CanBackward);

         UpCommand = CreateUpCommand();

         NaviHistory.PropertyChanged += NaviHistory_PropertyChanged;
      }

      private void NaviHistory_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {

         this.OnPropertyChanged(null);
      }

      public object CurrentItem => NaviHistory.CurrentItem;

      public int? ForwardCount => NaviHistory.ForwardCount;

      public int? BackwardCount => NaviHistory.BackwardCount;

      /// <summary>
      /// Command executes when the user has selected
      /// a different item in the displayed list of items.
      /// </summary>
      public ICommand SelectionCommand { get; }

      /// <summary>
      /// Gets a command to browse forward in the available collection of items.
      /// </summary>
      public ICommand ForwardCommand { get; }

      /// <summary>
      /// Gets a command to browse backward in the available collection of items.
      /// </summary>
      public ICommand BackwardCommand { get; }

      /// <summary>
      /// Gets a command to browse to the parent (if any) of the current location.
      /// </summary>
      public ICommand UpCommand { get; }

      public IEnumerable Collection => NaviHistory.Collection;

      public string? BackwardText => NaviHistory.BackwardItem?.ToString();

      public string? ForwardText => NaviHistory.ForwardItem?.ToString();


      private ICommand CreateUpCommand() {
         return new RelayCommand<object>(_ => {
            try {
               if (Directory.GetParent(NaviHistory.CurrentItem.Path) is DirectoryInfo parent)
                  NaviHistory.Navigate(new PathItem(parent.FullName));
            }
            catch {
            }
         },
           _ => {
              if (NaviHistory.CurrentItem == null)
                 return false;
              try {
                 return Directory.GetParent(NaviHistory.CurrentItem.Path) != null;
              }
              catch {
              }
              return false;
           });
      }

      private static ICommand CreateSelectionChanged(IBrowseHistory<PathItem> NaviHistory) {
         return new RelayCommand<object>(p => {

            if (p is object[] paths && paths[0] is PathItem pss &&
            CreateSelectionChanged(NaviHistory.Collection, pss) is PathItem pathItem1)
               NaviHistory.CurrentItem = pathItem1;

            else if (p is string pi &&
               CreateSelectionChanged(NaviHistory.Collection, new PathItem(pi)) is PathItem pathItem)
               NaviHistory.CurrentItem = pathItem;
         });

         static IEquatable<T>? CreateSelectionChanged<T>(IReadOnlyCollection<IEquatable<T>> collection, IEquatable<T> item) {

            return collection
              .Select(histItem => Tuple.Create(histItem, Equals(histItem, item)))
              .FirstOrDefault(a => a.Item2)?.Item1;
         }
      }
   }
}

