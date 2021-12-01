namespace HistoryControlLib.ViewModels
{

    using HistoryControlLib.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;

    /// <summary>
    /// Implements a navigational list of recent locations which were recently
    /// visited and may be suggested for re-visitation.
    /// </summary>
    public class BrowseHistory<T> : Base.BaseViewModel, IBrowseHistory<T> where T : IEquatable<T>
    {
        #region fields
        const int ListLimit = 128;
        private readonly ThreadSafeObservableCollection<T> collection = new ThreadSafeObservableCollection<T>();
        //private int _SelectedIndex = -1;
        private int selectedIndex;
        #endregion fields


        #region properties
        /// <inheritdoc/>
        public IReadOnlyCollection<T> Collection => collection;

        /// <inheritdoc/>
        public int Count => collection.Count;

        public int? BackwardCount => collection.Count > 0 ? collection.Count - SelectedIndex - 1 : default;

        public int? ForwardCount => collection.Count > 0 ? SelectedIndex : default;

        /// <inheritdoc/>
        public T BackwardItem => CanBackward ? collection[SelectedIndex + 1] : default(T);

        /// <inheritdoc/>
        public T ForwardItem => CanForward ? collection[SelectedIndex - 1] : default(T);

        /// <inheritdoc/>
        public bool CanForward => SelectedIndex > 0;

        /// <inheritdoc/>
        public bool CanBackward => (SelectedIndex + 1) < collection.Count;

        /// <inheritdoc/>
        protected int SelectedIndex
        {
            get => selectedIndex;
            private set
            {
                selectedIndex = value;
                NotifyPropertyChanged(() => ForwardCount);
                NotifyPropertyChanged(() => BackwardCount);
                NotifyPropertyChanged(() => SelectedIndex);
                NotifyPropertyChanged(() => CurrentItem);
            }
        }

        /// <inheritdoc/>
        public T CurrentItem
        {
            get
            {
                return SelectedIndex >= 0 && SelectedIndex < collection.Count ? collection[SelectedIndex] : default;
            }
            set
            {
                if (collection.Contains(value) == false)
                    collection.Add(value);

                SelectedIndex = collection.IndexOf(value);
            }
        }
        #endregion properties


        /// <summary>
        /// Set the <seealso cref="SelectedIndex"/> property within the currently
        /// available collection of locations or throws an exception
        /// if the requested index is out of bounds.
        /// </summary>
        //public void SetSelectedIndex(int idx)
        //{
        //    if (idx < 0 || (idx + 1) > _Locations.Count)
        //        throw new System.ArgumentOutOfRangeException(string.Format("Index {0} out of bounds between 0 and {1}", idx, _Locations.Count));

        //    SelectedIndex = idx;
        //}

        /// <summary>
        /// Navigates forward in the list of currently available locations.
        ///
        /// The implemented behavior depends on the current set of locations
        /// and the selected element within that set of locations.
        ///
        /// The method adds the new item at index 0 if the currently selected item
        /// has the index 0 or if the current list of locations is empty (SelectedIndex = -1).
        ///
        /// The new item is inserted at SelectedIndex -1 and the SelectedIndex is set to that item.
        /// All items before the new item are removed.
        ///
        /// All items with an index greater limit n are removed,
        /// if the list gets larger than limit n.
        /// </summary>
        public void Navigate(T newLocation)
        {

            Forward(newLocation, ref selectedIndex, collection);
            SelectedIndex = selectedIndex;
            NotifyPropertyChanged(() => CanBackward);
            NotifyPropertyChanged(() => CanForward);
        }

        /// <summary>
        /// Navigates forward within the current set of locations (without adding a location)
        /// and returns true if this is possible - based on set of locations and SelectedIndex
        /// or false, if navigation forward is not possible.
        /// </summary>
        public bool Forward()
        {
            if (SelectedIndex > 0)
            {
                SelectedIndex--;

                NotifyPropertyChanged(() => CanBackward);
                NotifyPropertyChanged(() => CanForward);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Navigates backward in the list of currently available locations.
        ///
        /// Returns false if backward navigation is possible or true, otherwise.
        /// </summary>
        public bool Backward()
        {
            if ((SelectedIndex + 1) < collection.Count)
            {
                SelectedIndex++;

                NotifyPropertyChanged(() => CanBackward);
                NotifyPropertyChanged(() => CanForward);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Implements standard override for ToString() method.
        /// </summary>
        public override string ToString()
        {
            string ret = string.Empty;

            for (int i = 0; i < collection.Count; i++)
            {
                ret += string.Format("{0:00} ", i) + collection[i].ToString();

                if (i == SelectedIndex)
                    ret += " CURRENT POSITION";

                ret += '\n';
            }

            ret += "\n\n" + string.Format(" CurrentPosition : {0:00}", SelectedIndex) + '\n';

            return ret;
        }



        private static void Forward(T newLocation, ref int selectedIndex, ThreadSafeObservableCollection<T> _Locations)
        {
            if (_Locations.Count > 0) {

            //   // Do nothing if a forward on the current location appears
            //   // to describe the requested location
             if (newLocation.Equals(_Locations[selectedIndex]))
                  return;

            }

            if (selectedIndex > 0)
            {
                // Update this item to recycle spot in list
                _Locations.ReplaceAt(selectedIndex - 1, newLocation);

                for (int i = 0; i < (selectedIndex - 1); i++) // Remove all previous items
                    _Locations.RemoveAtSafe(0);

                selectedIndex = 0;                          // Reset current position
            }
            else // Just insert at beginning of list
            {
                _Locations.InsertAt(0, newLocation);     // SelectedIndex = 0;
                selectedIndex = 0;
            }

            if (_Locations.Count > ListLimit)        // Make sure list cannot grow beyond useful size
            {
                for (int i = 0; i < _Locations.Count - ListLimit; i++)
                {
                    _Locations.RemoveAtSafe(_Locations.Count - 1);  // Always remove last element
                }
            }
        }
    }

    class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        private readonly SynchronizationContext context;

        public ThreadSafeObservableCollection()
        {
            context = SynchronizationContext.Current;
        }

        /// <summary>
        /// Updates the item with index <paramref name="pos"/> to recycle spot
        /// in list with item <paramref name="item"/> (old item is replaced with new item).
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="item"></param>
        public void ReplaceAt(int pos, T item)
        {
            context.Send(a =>
            {
                this[pos] = item;   // Update this item to recycle spot in list
            }, null);
        }

        public void InsertAt(int pos, T item)
        {
            context.Send(a =>
            {
                this.Insert(pos, item);    // SelectedIndex = 0;
            }, null);
        }

        public void RemoveAtSafe(int pos)
        {
            context.Send(a =>
            {
                this.RemoveAt(pos);    // SelectedIndex = 0;
            }, null);
        }


        /// <summary>
        /// Removes all currently logged locations and sets:
        /// <seealso cref="SelectedIndex"/> = -1
        /// </summary>
        public void ClearSafe()
        {
            context.Send(a =>
            {
                this.Clear();

            }, null);
        }

    }
}