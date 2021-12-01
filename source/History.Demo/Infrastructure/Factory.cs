namespace HistoryControlLib
{
    using BrowseHistory;
    using HistoryControlLib.Interfaces;
    using HistoryControlLib.ViewModels;

    public static class Factory
    {
        /// <summary>
        /// Creates a browse history object that keeps track of a users browse
        /// hostory based on the items defined through {T}.
        /// </summary>
        /// <returns></returns>
        public static IBrowseHistory<PathItem> CreateBrowseNavigator()
        {
            var browseHistory = new BrowseHistory<PathItem>();
         var destinations = new[] {
               @"C:\",
               @"F:\Program Files\My Program\My  Dir 1\My  Dir 2",
               @"F:\",
               @"F:\Temp",
               @"F:\Temp\More Files",
               @"G:\",
               @"H:\" };

         //browseHistory.CurrentItem = new PathItem(@"C:\");
            foreach (var item in destinations)
            browseHistory.Navigate(new PathItem(item));

         return browseHistory;
         }
      
    }
}
