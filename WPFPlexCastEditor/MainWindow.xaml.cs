using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFPlexCastEditor.Collections;

namespace WPFPlexCastEditor
{
    public partial class MainWindow : Window
    {
        public LibraryCollection LibraryCollection { get; set; }
        public MovieCollection MovieCollection { get; set; }
        public ActorCollection ActorCollection { get; set; }
        public ActorCollection AutoActorCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LibraryCollection = new LibraryCollection();
            MovieCollection = new MovieCollection();
            ActorCollection = new ActorCollection();
            AutoActorCollection = new ActorCollection();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            Database.DBFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases\\com.plexapp.plugins.library.db");
            lvLibrarySections.ItemsSource = LibraryCollection;
            lvMovies.ItemsSource = MovieCollection;
            lvActors.ItemsSource = ActorCollection;
            autoActors.ItemsSource = AutoActorCollection;
            LoadLibraryCollection();
            LoadAutoActorCollection();
        }

        private void lvLibrarySections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLibrarySections.SelectedItem != null)
            {
                LoadMovieCollection(((Library)lvLibrarySections.SelectedItem).id);
            }
        }

        private void lvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMovies.SelectedItem != null)
            {
                LoadActorCollection(((Movie)lvMovies.SelectedItem).id);
            }
        }

        private void LoadLibraryCollection()
        {
            LibraryCollection.Clear();
            spLibraries.Visibility = Visibility.Visible;
            MovieCollection.Clear();
            spMovies.Visibility = Visibility.Collapsed;
            ActorCollection.Clear();
            spCast.Visibility = Visibility.Collapsed;

            foreach (DataRow row in Database.GetLibrarySections().Rows)
            {
                LibraryCollection.Add(new Library() { id = long.Parse(row["id"].ToString()), name = row["name"].ToString() });
            }
        }

        private void LoadMovieCollection(long library_id)
        {
            MovieCollection.Clear();
            spMovies.Visibility = Visibility.Visible;
            ActorCollection.Clear();
            spCast.Visibility = Visibility.Collapsed;

            foreach (DataRow row in Database.GetMetadataItems(library_id).Rows)
            {   
                MovieCollection.Add(new Movie() { id = long.Parse(row["id"].ToString()), title = row["title"].ToString() });
            }
        }

        private void LoadActorCollection(long item_id)
        {
            ActorCollection.Clear();
            spCast.Visibility = Visibility.Visible;

            foreach (DataRow row in Database.GetActors(item_id).Rows)
            {
                ActorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }
        }

        private void LoadAutoActorCollection()
        {
            foreach (DataRow row in Database.GetAllActors().Rows)
            {
                if (!ActorCollection.AsEnumerable().Any(x => x.tag == row.Field<string>("tag")))
                {
                    AutoActorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
                }
            }
        }

        private void btnRemoveActor_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void autoActors_TextChanged(object sender, RoutedEventArgs e)
        {
            string text = autoActors.Text;
            Actor selectedActor = (Actor)autoActors.SelectedItem;

            if (selectedActor != null && text.Trim().Equals(selectedActor.tag, StringComparison.InvariantCultureIgnoreCase))
            {
                btnAddActor.Content = "Add: " + selectedActor.tag;
            }
            else if (!string.IsNullOrWhiteSpace(text))
            {
                btnAddActor.Content = "Create: " + text.Trim();
            }
        }
    }
}
