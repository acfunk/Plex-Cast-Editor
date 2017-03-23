using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace WPFPlexCastEditor.Collections
{
    public class Movie
    {
        public long id { get; set; }
        public string title { get; set; }
    }

    public class MovieCollection : ObservableCollection<Movie>
    { }
}
