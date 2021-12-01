namespace BrowserHistoryDemoLib.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Implements a sample location item that demos how a location
    /// can be recorded, handled, and logged in an application.
    /// </summary>
    public class PathItem : IEqualityComparer<PathItem>
    { 
        public PathItem(string path)            : this()
        {
            Path = path;
        }

        protected PathItem()
        {
        }
   

        /// <summary>
        /// Gets a path to a location that is indicated by this object.
        /// </summary>
        public string Path { get; }

        /// <remarks>
        /// Method is used in the <see cref="IBrowseHistory{T}"/>.Forward() method
        /// implementation to reduce redundancies in multiple requests for browsing
        /// to the same (current) loaction. The equality is used here too determine
        /// what the same location is and stop adding another location when the previously
        /// added location is the same as the current location.
        /// </remarks>
        public bool Equals(PathItem parx, PathItem pary) => string.Compare(parx.Path, pary.Path, true) == 0;

        public int GetHashCode(PathItem obj) => obj.GetHashCode();

        public new bool Equals(object x, object y) => Equals(x as PathItem, y as PathItem);

        public int GetHashCode(object obj) => GetHashCode(obj as PathItem);

    }
}
