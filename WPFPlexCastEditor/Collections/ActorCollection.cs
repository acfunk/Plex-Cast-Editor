using System.Collections.ObjectModel;

namespace WPFPlexCastEditor.Collections
{
    public class Actor
    {
        public long id { get; set; }
        public string tag { get; set; }
    }

    public class ActorCollection : ObservableCollection<Actor>
    { }
}
