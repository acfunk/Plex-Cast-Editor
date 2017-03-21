using System.Collections.ObjectModel;

namespace WPFPlexCastEditor.Collections
{
    public class Actor
    {
        public long id { get; set; }
        public string tag { get; set; }
    }

    public class ActorCollection : ObservableCollection<Actor>
    {
        public ActorCollection()
        {
            Add(new Actor() { id = 346, tag = "Anne Hathaway" });
            Add(new Actor() { id = 111, tag = "Brad Pitt" });
            Add(new Actor() { id = 677, tag = "Emm Stone" });
            Add(new Actor() { id = 222, tag = "Johny Depp" });
            Add(new Actor() { id = 777, tag = "Christian Baile" });
            Add(new Actor() { id = 888, tag = "Natalie Portman" });
            Add(new Actor() { id = 333, tag = "Tom Hanks" });
            Add(new Actor() { id = 444, tag = "Harrison Ford" });
            Add(new Actor() { id = 332, tag = "Scarlett Johansson" });
            Add(new Actor() { id = 555, tag = "Leonardo DiCaprio" });
            Add(new Actor() { id = 666, tag = "Will Smith" });
            Add(new Actor() { id = 999, tag = "Meryl Streep" });
            Add(new Actor() { id = 564, tag = "Jennifer Aniston" });
        }
    }
}
