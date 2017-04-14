using System;
using System.Collections.ObjectModel;

namespace WPFPlexCastEditor.Collections
{
    public class MetadataItem
    {
        public long id { get; set; }
        public string title { get; set; }
        public DateTime release_date { get; set; }
        public DateTime date_added { get; set; }
        public int actor_count { get; set; }
        public string user_fields { get; set; }
    }

    public class MetadataItemCollection : ObservableCollection<MetadataItem>
    { }
}
