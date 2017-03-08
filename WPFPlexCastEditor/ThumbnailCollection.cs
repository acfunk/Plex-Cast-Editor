using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace WPFPlexCastEditor
{
    public class Thumbnail
    {
        public int id { get; set; }
        public string title { get; set; }
        public BitmapImage thumbnail { get; set; }
    }

    public class ThumbnailCollection : ObservableCollection<Thumbnail>
    {
        public ThumbnailCollection()
        {
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 123, title = "Cat", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\4\7395211d6bac4fa998e7a512396f8ec271430b45873.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 234, title = "Dog", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\1\29884d7c15b26ff911b45f86cc8238a71740f4e9647b.bundle\Contents\Thumbnails\thumb1.jpg")) });
            Add(new Thumbnail() { id = 456, title = "Fish", thumbnail = new BitmapImage(new Uri(@"C:\Users\kmo19\AppData\Local\Plex Media Server\Media\localhost\3\1bc1023b5a561a61767aaa453b7cb758666f62c.bundle\Contents\Thumbnails\thumb1.jpg")) });
        }
    }
}
