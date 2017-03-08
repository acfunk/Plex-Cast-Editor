namespace PlexCastEditor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdDatabase = new System.Windows.Forms.OpenFileDialog();
            this.cmbLIbraries = new System.Windows.Forms.ComboBox();
            this.lblLibraries = new System.Windows.Forms.Label();
            this.dgvMetadataItems = new System.Windows.Forms.DataGridView();
            this.dgvActors = new System.Windows.Forms.DataGridView();
            this.txtActor = new System.Windows.Forms.TextBox();
            this.btnAddActor = new System.Windows.Forms.Button();
            this.gbActors = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbItems = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetadataItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActors)).BeginInit();
            this.gbActors.SuspendLayout();
            this.gbItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLIbraries
            // 
            this.cmbLIbraries.FormattingEnabled = true;
            this.cmbLIbraries.Location = new System.Drawing.Point(23, 49);
            this.cmbLIbraries.Name = "cmbLIbraries";
            this.cmbLIbraries.Size = new System.Drawing.Size(521, 32);
            this.cmbLIbraries.TabIndex = 1;
            // 
            // lblLibraries
            // 
            this.lblLibraries.AutoSize = true;
            this.lblLibraries.Location = new System.Drawing.Point(22, 21);
            this.lblLibraries.Name = "lblLibraries";
            this.lblLibraries.Size = new System.Drawing.Size(86, 25);
            this.lblLibraries.TabIndex = 2;
            this.lblLibraries.Text = "Libraries";
            // 
            // dgvMetadataItems
            // 
            this.dgvMetadataItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMetadataItems.Location = new System.Drawing.Point(6, 28);
            this.dgvMetadataItems.Name = "dgvMetadataItems";
            this.dgvMetadataItems.RowTemplate.Height = 31;
            this.dgvMetadataItems.Size = new System.Drawing.Size(1487, 283);
            this.dgvMetadataItems.TabIndex = 3;
            this.dgvMetadataItems.SelectionChanged += new System.EventHandler(this.dgvMetadataItems_SelectionChanged);
            // 
            // dgvActors
            // 
            this.dgvActors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActors.Location = new System.Drawing.Point(10, 79);
            this.dgvActors.Name = "dgvActors";
            this.dgvActors.RowTemplate.Height = 31;
            this.dgvActors.Size = new System.Drawing.Size(1479, 283);
            this.dgvActors.TabIndex = 6;
            this.dgvActors.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvActors_DragDrop);
            this.dgvActors.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvActors_DragOver);
            this.dgvActors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvActors_MouseDown);
            this.dgvActors.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvActors_MouseMove);
            // 
            // txtActor
            // 
            this.txtActor.Location = new System.Drawing.Point(6, 35);
            this.txtActor.Name = "txtActor";
            this.txtActor.Size = new System.Drawing.Size(516, 29);
            this.txtActor.TabIndex = 7;
            // 
            // btnAddActor
            // 
            this.btnAddActor.Location = new System.Drawing.Point(528, 28);
            this.btnAddActor.Name = "btnAddActor";
            this.btnAddActor.Size = new System.Drawing.Size(126, 45);
            this.btnAddActor.TabIndex = 8;
            this.btnAddActor.Text = "Add Actor";
            this.btnAddActor.UseVisualStyleBackColor = true;
            this.btnAddActor.Click += new System.EventHandler(this.btnAddActor_Click);
            // 
            // gbActors
            // 
            this.gbActors.Controls.Add(this.btnCancel);
            this.gbActors.Controls.Add(this.btnSave);
            this.gbActors.Controls.Add(this.dgvActors);
            this.gbActors.Controls.Add(this.btnAddActor);
            this.gbActors.Controls.Add(this.txtActor);
            this.gbActors.Location = new System.Drawing.Point(27, 446);
            this.gbActors.Name = "gbActors";
            this.gbActors.Size = new System.Drawing.Size(1503, 440);
            this.gbActors.TabIndex = 9;
            this.gbActors.TabStop = false;
            this.gbActors.Text = "Actors";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1274, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(215, 56);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1053, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(215, 56);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // gbItems
            // 
            this.gbItems.Controls.Add(this.dgvMetadataItems);
            this.gbItems.Location = new System.Drawing.Point(23, 108);
            this.gbItems.Name = "gbItems";
            this.gbItems.Size = new System.Drawing.Size(1507, 332);
            this.gbItems.TabIndex = 10;
            this.gbItems.TabStop = false;
            this.gbItems.Text = "Items";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1545, 902);
            this.Controls.Add(this.gbItems);
            this.Controls.Add(this.gbActors);
            this.Controls.Add(this.lblLibraries);
            this.Controls.Add(this.cmbLIbraries);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetadataItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActors)).EndInit();
            this.gbActors.ResumeLayout(false);
            this.gbActors.PerformLayout();
            this.gbItems.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdDatabase;
        private System.Windows.Forms.ComboBox cmbLIbraries;
        private System.Windows.Forms.Label lblLibraries;
        private System.Windows.Forms.DataGridView dgvMetadataItems;
        private System.Windows.Forms.DataGridView dgvActors;
        private System.Windows.Forms.TextBox txtActor;
        private System.Windows.Forms.Button btnAddActor;
        private System.Windows.Forms.GroupBox gbActors;
        private System.Windows.Forms.GroupBox gbItems;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}

