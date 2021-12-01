namespace BrowserHistoryDemoLib.ViewModels
{
    using BrowserHistoryDemoLib.ViewModels.Base;
    using HistoryControlLib.Interfaces;
    using HistoryControlLib.ViewModels.Base;
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;

    public class AppViewModel : BaseViewModel
    {
        #region fields
        private ICommand _SelectionChanged;
        private ICommand _ForwardCommand;
        private ICommand _BackwardCommand;
        private ICommand _UpCommand;
        private RelayCommand<object> _ForwardLocationCommand;
        #endregion fields

        /// <summary>
        /// Class constructor.
        /// </summary>
        public AppViewModel()
        {
            NaviHistory = HistoryControlLib.Factory<PathItem>.CreateBrowseNavigator();
        }

        /// <summary>
        /// Gets an object that can manage recently visited locations and supports:
        /// 
        /// - adding more recently visited locations
        /// - forward and backward navigation, and
        /// - selection of any position within the given set of locations.
        /// </summary>
        public IBrowseHistory<PathItem> NaviHistory { get; }

        /// <summary>
        /// Command executes when the user has selected
        /// a different item in the displayed list of items.
        /// </summary>
        public ICommand SelectionChanged
        {
            get
            {
                if (_SelectionChanged != null)
                    return SelectionChanged;

                return _SelectionChanged = new RelayCommand<object>((p) =>
                 {
                     if (!(p is object[] parameters) ||
                     parameters.Length == 0 ||
                     !(parameters[0] is PathItem item)
                     || !(NaviHistory.Locations
                     .Select((histItem, i) => Tuple.Create(i, Equals(histItem, item)))
                     .SingleOrDefault(a => a.Item2) is Tuple<int, bool> tuple))
                         return;

                     NaviHistory.SetSelectedIndex(tuple.Item1);
                 });
            }

        }


        /// <summary>
        /// Gets a command to browse forward in the available collection of items.
        /// </summary>
        public ICommand ForwardCommand
        {
            get
            {
                if (_ForwardCommand != null)
                {
                    return _ForwardCommand;
                }
                return _ForwardCommand = new RelayCommand<object>((p) =>
                {
                    if (NaviHistory.CanForward)
                        NaviHistory.Forward();
                },
                (p) => NaviHistory.CanForward);                
            }
        }

        /// <summary>
        /// Gets a command that simulates browsing to a new loaction. The command expects
        /// a string (e.g. 'C:\Program Files') that points to a path that is visited at this
        /// point in time.
        /// </summary>
        public ICommand ForwardLocationCommand
        {
            get
            {
                if (_ForwardLocationCommand != null)
                {
                    return _ForwardLocationCommand;
                }
                return _ForwardLocationCommand = new RelayCommand<object>((p) =>
                {
                    var param = p as string;

                    NaviHistory.Forward(new PathItem(param));
                });             
            }
        }

        /// <summary>
        /// Gets a command to browse backward in the available collection of items.
        /// </summary>
        public ICommand BackwardCommand
        {
            get
            {
                if (_BackwardCommand != null)
                {
                    return _BackwardCommand;
                }
                return _BackwardCommand = new RelayCommand<object>((p) =>
                {
                    if (NaviHistory.CanBackward == true)
                        NaviHistory.Backward();
                },
                (p) => NaviHistory.CanBackward);           
            }
        }

        /// <summary>
        /// Gets a command to browse to the parent (if any) of the current location.
        /// </summary>
        public ICommand UpCommand
        {
            get
            {
                if (_UpCommand == null)
                {
                    _UpCommand = new RelayCommand<object>((p) =>
                    {
                        try
                        {
                            if (System.IO.Directory.GetParent(NaviHistory.SelectedItem.Path) is DirectoryInfo parent)
                                NaviHistory.Forward(new PathItem(parent.FullName));
                        }
                        catch
                        {
                        }

                    },
                    (p) =>
                    {
                        if (NaviHistory.SelectedItem == null)
                            return false;
                        try
                        {
                            return Directory.GetParent(NaviHistory.SelectedItem.Path) != null;          
                        }
                        catch
                        {
                        }
                        return false;
                    });
                }

                return _UpCommand;
            }
        }
    }
}
