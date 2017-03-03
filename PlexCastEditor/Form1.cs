using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlexCastEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ofdDatabase.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Plex Media Server\\Plug-in Support\\Databases");
            this.ofdDatabase.Filter = "db files (*.db)|*.db|All files (*.*)|*.*";
            this.ofdDatabase.Multiselect = false;

            if (this.ofdDatabase.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.ofdDatabase.FileName;
                try
                {
                    using (var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", fileName)))
                    {
                        using (SQLiteCommand command = new SQLiteCommand("select id, name from library_sections;", connection))
                        {
                            connection.Open();
                            using (SQLiteDataReader read = command.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    this.dgvLibraries.Rows.Add(new object[] {
                                    read.GetValue(read.GetOrdinal("id")),
                                    read.GetValue(read.GetOrdinal("name")),
                                });
                                }
                            }
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
