using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFPlexCastEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate Point GetPosition(IInputElement element);
        int rowIndex = -1;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            cmbLibrarySections.SelectionChanged += cmbLibrarySections_SelectionChanged;
            actorsDataGrid.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(actorsDataGrid_PreviewMouseLeftButtonDown);
            actorsDataGrid.Drop += new DragEventHandler(actorsDataGrid_Drop);
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Database.DBFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases\\com.plexapp.plugins.library.db");
            cmbLibrarySections.ItemsSource = Database.GetLibrarySections().DefaultView;
        }

        private void cmbLibrarySections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvThumbnails.ItemsSource = new ThumbnailCollection();
        }



        void actorsDataGrid_Drop(object sender, DragEventArgs e)
        {
            if (rowIndex < 0)
                return;
            int index = this.GetCurrentRowIndex(e.GetPosition);
            if (index < 0)
                return;
            if (index == rowIndex)
                return;
            if (index == actorsDataGrid.Items.Count - 1)
            {
                MessageBox.Show("This row-index cannot be drop");
                return;
            }
            ActorCollection productCollection = Resources["ActorList"] as ActorCollection;
            Actor changedProduct = productCollection[rowIndex];
            productCollection.RemoveAt(rowIndex);
            productCollection.Insert(index, changedProduct);
        }
        void actorsDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rowIndex = GetCurrentRowIndex(e.GetPosition);
            if (rowIndex < 0)
                return;
            actorsDataGrid.SelectedIndex = rowIndex;
            Actor selectedEmp = actorsDataGrid.Items[rowIndex] as Actor;
            if (selectedEmp == null)
                return;
            DragDropEffects dragdropeffects = DragDropEffects.Move;
            if (DragDrop.DoDragDrop(actorsDataGrid, selectedEmp, dragdropeffects)
                                != DragDropEffects.None)
            {
                actorsDataGrid.SelectedItem = selectedEmp;
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
            if (actorsDataGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;
            return actorsDataGrid.ItemContainerGenerator.ContainerFromIndex(index)
                                                            as DataGridRow;
        }
        private int GetCurrentRowIndex(GetPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < actorsDataGrid.Items.Count; i++)
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

        
    }
}
