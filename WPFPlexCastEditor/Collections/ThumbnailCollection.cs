using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace WPFPlexCastEditor.Collections
{
    public class Thumbnail
    {
        public long id { get; set; }
        public string title { get; set; }
        public BitmapImage thumbnail { get; set; }
    }

    public class ThumbnailCollection : ObservableCollection<Thumbnail>
    { }
}
