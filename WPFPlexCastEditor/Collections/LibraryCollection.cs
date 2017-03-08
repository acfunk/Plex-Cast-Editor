using System.Collections.ObjectModel;

namespace WPFPlexCastEditor.Collections
{
    public class Library
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class LibraryCollection : ObservableCollection<Library>
    { }
}
