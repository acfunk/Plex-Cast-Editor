using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using WPFPlexCastEditor.Collections;
using System.Data;
using System.Windows.Media.Imaging;

namespace WPFPlexCastEditor
{
    public partial class MainWindow : Window
    {
        LibraryCollection _libraryCollection = new LibraryCollection();
        ThumbnailCollection _thumbnailCollection = new ThumbnailCollection();
        ActorCollection _actorCollection = new ActorCollection();
        public delegate Point GetPosition(IInputElement element);
        private int _rowIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            lvLibrarySections.SelectionChanged += lvLibrarySections_SelectionChanged;
            lvThumbnails.SelectionChanged += lvThumbnails_SelectionChanged;
            dgActors.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(actorsDataGrid_PreviewMouseLeftButtonDown);
            dgActors.Drop += new DragEventHandler(actorsDataGrid_Drop);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Database.DBFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases\\com.plexapp.plugins.library.db");
            LoadLibraryCollection();
            lvLibrarySections.ItemsSource = _libraryCollection;
            lvThumbnails.ItemsSource = _thumbnailCollection;
            dgActors.ItemsSource = _actorCollection;
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
            _actorCollection.Clear();

            foreach (DataRow row in Database.GetLibrarySections().Rows)
            {
                _libraryCollection.Add(new Library() { id = long.Parse(row["id"].ToString()), name = row["name"].ToString() });
            }
        }

        private void LoadThumbnailCollection(long library_id)
        {
            _thumbnailCollection.Clear();
            _actorCollection.Clear();

            foreach (DataRow row in Database.GetMetadataItems(library_id).Rows)
            {
                string thumbnailPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server/Media/localhost/" + row["user_thumb_url"].ToString().Replace("media://", ""));
                BitmapImage bitmapImage = new BitmapImage(new Uri(thumbnailPath));
                _thumbnailCollection.Add(new Thumbnail() { id = long.Parse(row["id"].ToString()), title = row["title"].ToString(), thumbnail = bitmapImage });
            }
        }

        private void LoadActorCollection(long item_id)
        {
            _actorCollection.Clear();

            foreach (DataRow row in Database.GetActors(item_id).Rows)
            {
                _actorCollection.Add(new Actor() { id = long.Parse(row["id"].ToString()), tag = row["tag"].ToString() });
            }
        }

        #region Drag and Drop Actors
        void actorsDataGrid_Drop(object sender, DragEventArgs e)
        {
            if (_rowIndex < 0)
                return;

            int index = this.GetCurrentRowIndex(e.GetPosition);

            if (index < 0)
                return;

            if (index == _rowIndex)
                return;

            ActorCollection actorCollection = Resources["ActorList"] as ActorCollection;
            Actor changedProduct = actorCollection[_rowIndex];
            actorCollection.RemoveAt(_rowIndex);
            actorCollection.Insert(index, changedProduct);
        }

        void actorsDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _rowIndex = GetCurrentRowIndex(e.GetPosition);

            if (_rowIndex < 0)
                return;

            dgActors.SelectedIndex = _rowIndex;
            Actor selectedEmp = dgActors.Items[_rowIndex] as Actor;

            if (selectedEmp == null)
                return;

            DragDropEffects dragdropeffects = DragDropEffects.Move;

            if (DragDrop.DoDragDrop(dgActors, selectedEmp, dragdropeffects) != DragDropEffects.None)
            {
                dgActors.SelectedItem = selectedEmp;
            }
        }

        private bool GetMouseTargetRow(Visual theTarget, GetPosition position)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point point = position((IInputElement)theTarget);
            return rect.Contains(point);
        }

        private DataGridRow GetRowItem(int index)
        {
            if (dgActors.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return dgActors.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }

        private int GetCurrentRowIndex(GetPosition pos)
        {
            int curIndex = -1;

            for (int i = 0; i < dgActors.Items.Count; i++)
            {
                DataGridRow itm = GetRowItem(i);

                if (GetMouseTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }

            return curIndex;
        }
        #endregion Drag and Drop Actors
    }
}
