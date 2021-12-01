
namespace BrowseHistory
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Implements a sample location item that demos how a location
    /// can be recorded, handled, and logged in an application.
    /// </summary>
    public class PathItem : IEquatable<PathItem>
    {
        public PathItem(string path) : this()
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


        //public int GetHashCode(PathItem? obj) => obj?.GetHashCode() ?? -1;

        //public new bool Equals(object x, object y) => Equals(x as PathItem, y as PathItem);

        //public int GetHashCode(object obj) => GetHashCode(obj as PathItem);

        public bool Equals(PathItem other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PathItem);
        }

        public override int GetHashCode() => Path?.Length ?? -1;


        public override string ToString()
        {
            return Path;
        }


        /// <remarks>
        /// Method is used in the <see cref="IBrowseHistory{T}"/>.Forward() method
        /// implementation to reduce redundancies in multiple requests for browsing
        /// to the same (current) loaction. The equality is used here too determine
        /// what the same location is and stop adding another location when the previously
        /// added location is the same as the current location.
        /// </remarks>
        bool Equals(PathItem x, PathItem y) => x != null && y != null ? string.Compare(new FileInfo(x.Path).FullName, new FileInfo(y.Path).FullName, true) == 0 : false;
    }
}
