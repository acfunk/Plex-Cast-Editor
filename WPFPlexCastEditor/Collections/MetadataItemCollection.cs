using System.Collections.ObjectModel;

namespace WPFPlexCastEditor.Collections
{
    public class MetadataItem
    {
        public long id { get; set; }
        public string title { get; set; }
    }

    public class MetadataItemCollection : ObservableCollection<MetadataItem>
    { }
}
