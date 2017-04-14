using Microsoft.Win32;
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
        private string _defaultDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases\\com.plexapp.plugins.library.db");
        public LibraryCollection LibraryCollection { get; set; }
        public MetadataItemCollection ItemCollection { get; set; }
        public ActorCollection CastCollection { get; set; }
        public ActorCollection AllActorsCollection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LibraryCollection = new LibraryCollection();
            ItemCollection = new MetadataItemCollection();
            CastCollection = new ActorCollection();
            AllActorsCollection = new ActorCollection();
        }

        #region Events

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            this.ChangeToDatabase(_defaultDatabase);
        }

        private void btnChangeFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database files (*.db)|*.db|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                this.ChangeToDatabase(openFileDialog.FileName);
            }
        }

        private void btnUseDefault_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeToDatabase(_defaultDatabase);
        }
        
        private void lvLibrarySections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLibrarySections.SelectedItem != null)
            {
                LoadItemCollection(((Library)lvLibrarySections.SelectedItem).id);
            }
        }

        private void lvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMovies.SelectedItem != null)
            {
                LoadCastCollection(((MetadataItem)lvMovies.SelectedItem).id);
            }
        }
        
        private void btnRemoveActor_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Actor actor = button.DataContext as Actor;
            CastCollection.Remove(actor);
        }

        private void autoActors_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TryAddActor();
            }
        }

        private void autoActors_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TryAddActor();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            lvMovies.SelectedItem = null;
            CastCollection.Clear();
            ContainerCast.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[2].Height = new GridLength(0);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Database.ResetTaggings(((MetadataItem)lvMovies.SelectedItem).id, CastCollection);
            lvMovies.SelectedItem = null;
            CastCollection.Clear();
            ContainerCast.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[2].Height = new GridLength(0);
        }

        #endregion Events

        #region Methods

        private void ChangeToDatabase(string databaseFile)
        {
            try
            {
                Database.DBFile = databaseFile;
                LoadAllActorsCollection();
                LoadLibraryCollection();
                this.lblMessaging.Content = string.Format("Connected to {0}", Database.DBFile);
            }
            catch (Exception ex)
            {
                this.lblMessaging.Content = string.Format("ERROR: {0}", ex.Message);
                MessageBox.Show(string.Format("Unable to connect to {0}", databaseFile), "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Database.DBFile != _defaultDatabase)
            {
                this.btnUseDefault.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnUseDefault.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadAllActorsCollection()
        {
            AllActorsCollection.Clear();

            autoActors.ItemsSource = null;

            foreach (DataRow row in Database.GetAllActors().Rows)
            {
                AllActorsCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }

            autoActors.ItemsSource = AllActorsCollection;
        }

        private void LoadLibraryCollection()
        {
            LibraryCollection.Clear();
            ContainerLibraries.Visibility = Visibility.Visible;
            ItemCollection.Clear();
            ContainerMovies.Visibility = Visibility.Collapsed;
            CastCollection.Clear();
            ContainerCast.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[2].Height = new GridLength(0);

            lvLibrarySections.ItemsSource = null;

            foreach (DataRow row in Database.GetLibrarySections().Rows)
            {
                LibraryCollection.Add(new Library() { id = long.Parse(row["id"].ToString()), name = row["name"].ToString() });
            }

            lvLibrarySections.ItemsSource = LibraryCollection;
        }

        private void LoadItemCollection(long library_id)
        {
            ItemCollection.Clear();
            ContainerMovies.Visibility = Visibility.Visible;
            CastCollection.Clear();
            ContainerCast.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[2].Height = new GridLength(0);

            lvMovies.ItemsSource = null;

            try
            {

                foreach (DataRow row in Database.GetMetadataItems(library_id).Rows)
                {
                    ItemCollection.Add(new MetadataItem()
                    {
                        id = long.Parse(row["id"].ToString()),
                        title = row["title"].ToString(),
                        release_date = DateTime.Parse(row["release_date"].ToString()),
                        date_added = DateTime.Parse(row["date_added"].ToString()),
                        actor_count = int.Parse(row["actor_count"].ToString()),
                        user_fields = row["user_fields"].ToString()
                    });
                }

            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }

            lvMovies.ItemsSource = ItemCollection;
        }

        private void LoadCastCollection(long item_id)
        {
            CastCollection.Clear();
            ContainerCast.Visibility = Visibility.Visible;
            MainGrid.RowDefinitions[2].Height = new GridLength(65);
            autoActors.Text = string.Empty;

            lvActors.ItemsSource = null;

            foreach (DataRow row in Database.GetActors(item_id).Rows)
            {
                CastCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }

            lvActors.ItemsSource = CastCollection;
        }

        private void TryAddActor()
        {
            if (!string.IsNullOrWhiteSpace(autoActors.Text))
            {
                Actor actor_from_cast = CastCollection.Where(x => x.tag.ToLower().Trim() == autoActors.Text.ToLower().Trim()).FirstOrDefault();

                if (actor_from_cast == null)
                {
                    Actor actor_from_search = AllActorsCollection.Where(x => x.tag.Trim() == autoActors.Text.Trim()).FirstOrDefault();

                    if (actor_from_search != null)
                    {
                        CastCollection.Add(actor_from_search);
                    }
                    else
                    {
                        CastCollection.Add(new Actor() { id = -1, tag = autoActors.Text.Trim() });
                    }
                }
            }

            autoActors.Text = string.Empty;
            autoActors.SelectedItem = null;
        }

        #endregion Methods
    }
}
