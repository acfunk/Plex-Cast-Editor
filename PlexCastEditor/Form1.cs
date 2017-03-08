using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace PlexCastEditor
{
    public partial class Form1 : Form
    {
        private AutoCompleteStringCollection _actorsAutoComplete = new AutoCompleteStringCollection();
        private DataTable _actorsTable = null;
        private int _rowIndexFromMouseDown;
        private Rectangle _dragBoxFromMouseClick;
        private DataRow _oldRow;
        private DataRow _newRow;

        public long LibrarySectionId
        {
            get
            {
                DataRowView drv = (DataRowView)cmbLIbraries.SelectedItem;
                return long.Parse(drv["id"].ToString());
            }
        }

        public long MetadataItemId
        {
            get
            {
                long id = -1;

                if (dgvMetadataItems.SelectedRows.Count > 0)
                {
                    DataRowView drv = (DataRowView)dgvMetadataItems.SelectedRows[0].DataBoundItem;
                    id = long.Parse(drv["id"].ToString());
                }

                return id;
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ofdDatabase.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases");
            //ofdDatabase.Filter = "db files (*.db)|*.db|All files (*.*)|*.*";
            //ofdDatabase.Multiselect = false;
            Database.DBFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases\\com.plexapp.plugins.library.db");
            GetLibraries();
            GetActorAutoComplete();
        }

        private void cmbLibraries_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMetadataItems();
        }
        
        private void dgvMetadataItems_SelectionChanged(object sender, EventArgs e)
        {
            GetActors();
            gbActors.Enabled = true;
        }

        private void btnAddActor_Click(object sender, EventArgs e)
        {
            string actor = txtActor.Text.Trim();
            if (!_actorsAutoComplete.Contains(actor))
            {
                _actorsAutoComplete.Add(actor);
            }

            if (!_actorsTable.AsEnumerable().Any(r => r.Field<String>("tag") == actor))
            {
                DataRow row = _actorsTable.NewRow();
                row["id"] = -1;
                row["tag"] = actor;
                row["index"] = 0;
                _actorsTable.Rows.Add(row);
            }

            txtActor.Clear();
        }

        private void InitializeControls()
        {
            cmbLIbraries.ValueMember = "id";
            cmbLIbraries.DisplayMember = "name";

            dgvMetadataItems.AutoGenerateColumns = true;
            dgvMetadataItems.ReadOnly = true;
            dgvMetadataItems.AllowUserToAddRows = false;
            dgvMetadataItems.AllowUserToDeleteRows = false;
            dgvMetadataItems.MultiSelect = false;
            dgvMetadataItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvActors.AutoGenerateColumns = true;
            dgvActors.ReadOnly = true;
            dgvActors.AllowUserToAddRows = false;
            dgvActors.MultiSelect = false;
            dgvActors.AllowDrop = true;
            dgvActors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            gbActors.Enabled = false;
        }

        private void GetLibraries()
        {
            cmbLIbraries.SelectedIndexChanged -= new EventHandler(cmbLibraries_SelectedIndexChanged);
            cmbLIbraries.DataSource = Database.GetLibrarySections();
            cmbLIbraries.SelectedIndexChanged += new EventHandler(cmbLibraries_SelectedIndexChanged);
        }

        private void GetActorAutoComplete()
        {
            _actorsAutoComplete.AddRange(Database.GetAllActors());
            txtActor.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtActor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtActor.AutoCompleteCustomSource = _actorsAutoComplete;
        }

        private void GetMetadataItems()
        {
            dgvMetadataItems.SelectionChanged -= new EventHandler(dgvMetadataItems_SelectionChanged);
            dgvMetadataItems.DataSource = Database.GetMetadataItems(LibrarySectionId);
            dgvMetadataItems.ClearSelection();
            _actorsTable = null;
            dgvActors.DataSource = _actorsTable;
            gbActors.Enabled = false;
            dgvMetadataItems.SelectionChanged += new EventHandler(dgvMetadataItems_SelectionChanged);
        }

        private void GetActors()
        {
            _actorsTable = Database.GetActors(MetadataItemId);
            dgvActors.DataSource = _actorsTable;
            dgvActors.ClearSelection();
            foreach (DataGridViewColumn column in dgvActors.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dgvActors_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (_dragBoxFromMouseClick != Rectangle.Empty &&
                    !_dragBoxFromMouseClick.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dgvActors.DoDragDrop(
                    dgvActors.Rows[_rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dgvActors_MouseDown(object sender, MouseEventArgs e)
        {
            if (dgvActors.SelectedRows.Count == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _oldRow = ((DataRowView)dgvActors.SelectedRows[0].DataBoundItem).Row;
                    _newRow = _actorsTable.NewRow();
                    _newRow.ItemArray = _oldRow.ItemArray;
                    _rowIndexFromMouseDown = dgvActors.SelectedRows[0].Index;
                    dgvActors.DoDragDrop(_oldRow, DragDropEffects.Move);

                    if (_rowIndexFromMouseDown >= 0)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        _dragBoxFromMouseClick = new Rectangle(new Point(
                            e.X - (dragSize.Width / 2)
                            , e.Y - (dragSize.Height / 2))
                            , dragSize);
                    }
                    else
                    {
                        _dragBoxFromMouseClick = Rectangle.Empty;
                    }
                }
            }
        }
        
        private void dgvActors_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = dgvActors.PointToClient(new Point(e.X, e.Y));
            int rowIndexOfItemUnderMouseToDrop = dgvActors.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (rowIndexOfItemUnderMouseToDrop >= 0 && e.Effect == DragDropEffects.Move)
            {
                _actorsTable.Rows.Remove(_oldRow);
                _actorsTable.Rows.InsertAt(_newRow, rowIndexOfItemUnderMouseToDrop);
            }
        }

        private void dgvActors_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
