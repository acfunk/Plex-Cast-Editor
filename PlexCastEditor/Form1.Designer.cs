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
            this.lblItems = new System.Windows.Forms.Label();
            this.lblActors = new System.Windows.Forms.Label();
            this.dgvActors = new System.Windows.Forms.DataGridView();
            this.txtActor = new System.Windows.Forms.TextBox();
            this.btnAddActor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetadataItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActors)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbLIbraries
            // 
            this.cmbLIbraries.FormattingEnabled = true;
            this.cmbLIbraries.Location = new System.Drawing.Point(12, 49);
            this.cmbLIbraries.Name = "cmbLIbraries";
            this.cmbLIbraries.Size = new System.Drawing.Size(521, 32);
            this.cmbLIbraries.TabIndex = 1;
            // 
            // lblLibraries
            // 
            this.lblLibraries.AutoSize = true;
            this.lblLibraries.Location = new System.Drawing.Point(12, 21);
            this.lblLibraries.Name = "lblLibraries";
            this.lblLibraries.Size = new System.Drawing.Size(86, 25);
            this.lblLibraries.TabIndex = 2;
            this.lblLibraries.Text = "Libraries";
            // 
            // dgvMetadataItems
            // 
            this.dgvMetadataItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMetadataItems.Location = new System.Drawing.Point(12, 131);
            this.dgvMetadataItems.Name = "dgvMetadataItems";
            this.dgvMetadataItems.RowTemplate.Height = 31;
            this.dgvMetadataItems.Size = new System.Drawing.Size(1713, 283);
            this.dgvMetadataItems.TabIndex = 3;
            this.dgvMetadataItems.SelectionChanged += new System.EventHandler(this.dgvMetadataItems_SelectionChanged);
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Location = new System.Drawing.Point(12, 103);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(59, 25);
            this.lblItems.TabIndex = 4;
            this.lblItems.Text = "Items";
            // 
            // lblActors
            // 
            this.lblActors.AutoSize = true;
            this.lblActors.Location = new System.Drawing.Point(12, 446);
            this.lblActors.Name = "lblActors";
            this.lblActors.Size = new System.Drawing.Size(68, 25);
            this.lblActors.TabIndex = 5;
            this.lblActors.Text = "Actors";
            // 
            // dgvActors
            // 
            this.dgvActors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActors.Location = new System.Drawing.Point(12, 545);
            this.dgvActors.Name = "dgvActors";
            this.dgvActors.RowTemplate.Height = 31;
            this.dgvActors.Size = new System.Drawing.Size(1713, 283);
            this.dgvActors.TabIndex = 6;
            // 
            // txtActor
            // 
            this.txtActor.Location = new System.Drawing.Point(17, 485);
            this.txtActor.Name = "txtActor";
            this.txtActor.Size = new System.Drawing.Size(516, 29);
            this.txtActor.TabIndex = 7;
            // 
            // btnAddActor
            // 
            this.btnAddActor.Location = new System.Drawing.Point(562, 479);
            this.btnAddActor.Name = "btnAddActor";
            this.btnAddActor.Size = new System.Drawing.Size(126, 43);
            this.btnAddActor.TabIndex = 8;
            this.btnAddActor.Text = "Add Actor";
            this.btnAddActor.UseVisualStyleBackColor = true;
            this.btnAddActor.Click += new System.EventHandler(this.btnAddActor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1737, 1041);
            this.Controls.Add(this.btnAddActor);
            this.Controls.Add(this.txtActor);
            this.Controls.Add(this.dgvActors);
            this.Controls.Add(this.lblActors);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.dgvMetadataItems);
            this.Controls.Add(this.lblLibraries);
            this.Controls.Add(this.cmbLIbraries);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetadataItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdDatabase;
        private System.Windows.Forms.ComboBox cmbLIbraries;
        private System.Windows.Forms.Label lblLibraries;
        private System.Windows.Forms.DataGridView dgvMetadataItems;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Label lblActors;
        private System.Windows.Forms.DataGridView dgvActors;
        private System.Windows.Forms.TextBox txtActor;
        private System.Windows.Forms.Button btnAddActor;
    }
}

