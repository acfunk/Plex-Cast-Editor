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
            ContainerLibraries.Visibility = Visibility.Visible;
            MovieCollection.Clear();
            ContainerMovies.Visibility = Visibility.Collapsed;
            ActorCollection.Clear();
            ContainerCast.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(0);

            foreach (DataRow row in Database.GetLibrarySections().Rows)
            {
                LibraryCollection.Add(new Library() { id = long.Parse(row["id"].ToString()), name = row["name"].ToString() });
            }
        }

        private void LoadMovieCollection(long library_id)
        {
            MovieCollection.Clear();
            ContainerMovies.Visibility = Visibility.Visible;
            ActorCollection.Clear();
            ContainerCast.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(0);

            foreach (DataRow row in Database.GetMetadataItems(library_id).Rows)
            {   
                MovieCollection.Add(new Movie() { id = long.Parse(row["id"].ToString()), title = row["title"].ToString() });
            }
        }

        private void LoadActorCollection(long item_id)
        {
            ActorCollection.Clear();
            ContainerCast.Visibility = Visibility.Visible;
            MainGrid.RowDefinitions[1].Height = new GridLength(65);
            autoActors.Text = string.Empty;
            this.btnAddActor.IsEnabled = false;

            foreach (DataRow row in Database.GetActors(item_id).Rows)
            {
                ActorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }
        }

        private void LoadAutoActorCollection()
        {
            AutoActorCollection.Clear();

            foreach (DataRow row in Database.GetAllActors().Rows)
            {
                AutoActorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }
        }

        private void btnRemoveActor_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Actor actor = button.DataContext as Actor;
            ActorCollection.Remove(actor);
        }

        private void autoActors_TextChanged(object sender, RoutedEventArgs e)
        {
            btnAddActor.IsEnabled = autoActors.Text.Trim().Length > 0;
        }

        private void btnAddActor_Click(object sender, RoutedEventArgs e)
        {
            Actor actor_from_cast = ActorCollection.Where(x => x.tag.ToLower().Trim() == autoActors.Text.ToLower().Trim()).FirstOrDefault();
            
            if (actor_from_cast == null)
            {
                Actor actor_from_search = AutoActorCollection.Where(x => x.tag.Trim() == autoActors.Text.Trim()).FirstOrDefault();

                if (actor_from_search != null)
                {
                    ActorCollection.Add(actor_from_search);
                }
                else
                {
                    ActorCollection.Add(new Actor() { id = -1, tag = autoActors.Text.Trim() });
                }
            }

            autoActors.Text = string.Empty;
            btnAddActor.IsEnabled = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (lvLibrarySections.SelectedItem != null)
            {
                LoadMovieCollection(((Library)lvLibrarySections.SelectedItem).id);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
