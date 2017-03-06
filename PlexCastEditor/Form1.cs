using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PlexCastEditor
{
    public partial class Form1 : Form
    {    
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
            UpdateLibraries();
            UpdateActorAutoComplete();
        }

        private void cmbLibraries_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMetadataItems();
        }
        
        private void dgvMetadataItems_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActors();
        }

        private void btnAddActor_Click(object sender, EventArgs e)
        {
            long actor_id = Database.CreateActor(txtActor.Text.Trim());
            txtActor.Clear();
            UpdateActorAutoComplete();
            UpdateActors();
        }

        private void InitializeControls()
        {
            cmbLIbraries.ValueMember = "id";
            cmbLIbraries.DisplayMember = "name";

            dgvMetadataItems.AutoGenerateColumns = true;
            dgvMetadataItems.ReadOnly = true;
            dgvMetadataItems.AllowUserToAddRows = false;
            dgvMetadataItems.MultiSelect = false;
            dgvMetadataItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvActors.AutoGenerateColumns = true;
            dgvActors.ReadOnly = true;
            dgvActors.AllowUserToAddRows = false;
            dgvActors.MultiSelect = false;
            dgvActors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void UpdateLibraries()
        {
            cmbLIbraries.SelectedIndexChanged -= new EventHandler(cmbLibraries_SelectedIndexChanged);
            cmbLIbraries.DataSource = Database.GetLibrarySections();
            cmbLIbraries.SelectedIndexChanged += new EventHandler(cmbLibraries_SelectedIndexChanged);
        }

        private void UpdateMetadataItems()
        {
            DataRowView drv = (DataRowView)cmbLIbraries.SelectedItem;
            int metadata_item_id = int.Parse(drv["id"].ToString());
            dgvMetadataItems.DataSource = Database.GetMetadataItems(metadata_item_id);
            dgvMetadataItems.ClearSelection();
        }

        private void UpdateActorAutoComplete()
        {
            AutoCompleteStringCollection actorsAutoComplete = new AutoCompleteStringCollection();
            actorsAutoComplete.AddRange(Database.GetAllActors());
            txtActor.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtActor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtActor.AutoCompleteCustomSource = actorsAutoComplete;
        }

        private void UpdateActors()
        {
            if (dgvMetadataItems.SelectedRows.Count > 0)
            {
                DataRowView drv = (DataRowView)dgvMetadataItems.SelectedRows[0].DataBoundItem;
                int metadata_item_id = int.Parse(drv["id"].ToString());
                dgvActors.DataSource = Database.GetActors(metadata_item_id);
                dgvActors.ClearSelection();
            }
        }
    }
}
