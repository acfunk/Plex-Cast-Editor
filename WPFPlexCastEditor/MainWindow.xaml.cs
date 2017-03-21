using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WPFPlexCastEditor.Collections;

namespace WPFPlexCastEditor
{
    public partial class MainWindow : Window
    {
        LibraryCollection _libraryCollection = new LibraryCollection();
        ThumbnailCollection _thumbnailCollection = new ThumbnailCollection();
        public ActorCollection ActorCollection { get; set; }
        ActorCollection _addActorCollection = new ActorCollection();
        public string SelectedActor { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MainWindow_Loaded;
            lvLibrarySections.SelectionChanged += lvLibrarySections_SelectionChanged;
            lvThumbnails.SelectionChanged += lvThumbnails_SelectionChanged;
            ActorCollection = new ActorCollection();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Database.DBFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases\\com.plexapp.plugins.library.db");
            LoadLibraryCollection();
            lvLibrarySections.ItemsSource = _libraryCollection;
            lvThumbnails.ItemsSource = _thumbnailCollection;
            lvActors.ItemsSource = ActorCollection;
            autoActors.ItemsSource = _addActorCollection;
        }

        private void lvLibrarySections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLibrarySections.SelectedItem != null)
            {
                LoadThumbnailCollection(((Library)lvLibrarySections.SelectedItem).id);
            }
        }

        private void lvThumbnails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvThumbnails.SelectedItem != null)
            {
                LoadActorCollection(((Thumbnail)lvThumbnails.SelectedItem).id);
            }
        }

        private void LoadLibraryCollection()
        {
            _libraryCollection.Clear();
            _thumbnailCollection.Clear();
            ActorCollection.Clear();
            _addActorCollection.Clear();

            foreach (DataRow row in Database.GetLibrarySections().Rows)
            {
                _libraryCollection.Add(new Library() { id = long.Parse(row["id"].ToString()), name = row["name"].ToString() });
            }
        }

        private void LoadThumbnailCollection(long library_id)
        {
            _thumbnailCollection.Clear();
            ActorCollection.Clear();
            _addActorCollection.Clear();

            foreach (DataRow row in Database.GetMetadataItems(library_id).Rows)
            {
                string thumbnailPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server/Media/localhost/" + row["user_thumb_url"].ToString().Replace("media://", ""));
                BitmapImage bitmapImage = new BitmapImage(new Uri(thumbnailPath));
                _thumbnailCollection.Add(new Thumbnail() { id = long.Parse(row["id"].ToString()), title = row["title"].ToString(), thumbnail = bitmapImage });
            }
        }

        private void LoadActorCollection(long item_id)
        {
            ActorCollection.Clear();
            _addActorCollection.Clear();

            foreach (DataRow row in Database.GetActors(item_id).Rows)
            {
                ActorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }

            LoadAddActorCollection();
        }

        private void LoadAddActorCollection()
        {
            _addActorCollection.Clear();

            foreach (DataRow row in Database.GetAllActors().Rows)
            {
                if (!ActorCollection.AsEnumerable().Any(x => x.tag == row.Field<string>("tag")))
                {
                    _addActorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
                }
            }
        }

        private void autoActors_DropDownClosed(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            Actor actor = (Actor)autoActors.SelectedItem;
            string searchText = autoActors.SearchText;
            
            if (autoActors.SelectedItem != null)
            {
                btnAddActor.Content = "Add: " + actor.tag;
            }
            else if (string.IsNullOrWhiteSpace(searchText))
            {
                btnAddActor.Content = "Add: " + searchText.Trim();
            }
        }

        private void btnRemoveActor_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
