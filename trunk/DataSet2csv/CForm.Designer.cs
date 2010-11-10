namespace DataSet2csv {
    partial class CForm {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tstop = new System.Windows.Forms.ToolStrip();
            this.dataSet1 = new System.Data.DataSet();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.gv = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cbTable = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bRead = new System.Windows.Forms.ToolStripButton();
            this.bCopySel = new System.Windows.Forms.ToolStripButton();
            this.bSavecsv = new System.Windows.Forms.ToolStripDropDownButton();
            this.bTabsv = new System.Windows.Forms.ToolStripMenuItem();
            this.bCammasv = new System.Windows.Forms.ToolStripMenuItem();
            this.bIncludeHeader = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tstop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.gv);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(807, 458);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(807, 483);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tstop);
            // 
            // tstop
            // 
            this.tstop.Dock = System.Windows.Forms.DockStyle.None;
            this.tstop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRead,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cbTable,
            this.toolStripSeparator3,
            this.bCopySel,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.bSavecsv,
            this.bIncludeHeader});
            this.tstop.Location = new System.Drawing.Point(0, 0);
            this.tstop.Name = "tstop";
            this.tstop.Size = new System.Drawing.Size(807, 25);
            this.tstop.Stretch = true;
            this.tstop.TabIndex = 0;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "表示：";
            // 
            // gv
            // 
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv.Location = new System.Drawing.Point(0, 0);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 21;
            this.gv.Size = new System.Drawing.Size(807, 458);
            this.gv.TabIndex = 0;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = this.dataSet1;
            this.bindingSource1.Position = 0;
            // 
            // cbTable
            // 
            this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(160, 25);
            this.cbTable.SelectedIndexChanged += new System.EventHandler(this.cbTable_SelectedIndexChanged);
            this.cbTable.Click += new System.EventHandler(this.cbTable_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel2.Text = "CSVとして保存：";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bRead
            // 
            this.bRead.Image = global::DataSet2csv.Properties.Resources.openfolderHS;
            this.bRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRead.Name = "bRead";
            this.bRead.Size = new System.Drawing.Size(176, 22);
            this.bRead.Text = "DataSet形式のXMLを読み込む";
            this.bRead.Click += new System.EventHandler(this.bRead_Click);
            // 
            // bCopySel
            // 
            this.bCopySel.Image = global::DataSet2csv.Properties.Resources.CopyHS;
            this.bCopySel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCopySel.Name = "bCopySel";
            this.bCopySel.Size = new System.Drawing.Size(128, 22);
            this.bCopySel.Text = "選択部分をコピーする";
            this.bCopySel.Click += new System.EventHandler(this.bCopySel_Click);
            // 
            // bSavecsv
            // 
            this.bSavecsv.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bTabsv,
            this.bCammasv});
            this.bSavecsv.Image = global::DataSet2csv.Properties.Resources.saveHS;
            this.bSavecsv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSavecsv.Name = "bSavecsv";
            this.bSavecsv.Size = new System.Drawing.Size(77, 22);
            this.bSavecsv.Text = "保存する";
            // 
            // bTabsv
            // 
            this.bTabsv.Name = "bTabsv";
            this.bTabsv.Size = new System.Drawing.Size(162, 22);
            this.bTabsv.Text = "タブ区切りのCSV";
            this.bTabsv.Click += new System.EventHandler(this.bTabsv_Click);
            // 
            // bCammasv
            // 
            this.bCammasv.Name = "bCammasv";
            this.bCammasv.Size = new System.Drawing.Size(162, 22);
            this.bCammasv.Text = "カンマ区切りのCSV";
            this.bCammasv.Click += new System.EventHandler(this.bTabsv_Click);
            // 
            // bIncludeHeader
            // 
            this.bIncludeHeader.Checked = true;
            this.bIncludeHeader.CheckOnClick = true;
            this.bIncludeHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bIncludeHeader.Image = ((System.Drawing.Image)(resources.GetObject("bIncludeHeader.Image")));
            this.bIncludeHeader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bIncludeHeader.Name = "bIncludeHeader";
            this.bIncludeHeader.Size = new System.Drawing.Size(85, 22);
            this.bIncludeHeader.Text = "ヘッダ行含む";
            // 
            // CForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 483);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "CForm";
            this.Text = "DataSet2csv";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tstop.ResumeLayout(false);
            this.tstop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip tstop;
        private System.Windows.Forms.ToolStripButton bRead;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripComboBox cbTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton bSavecsv;
        private System.Windows.Forms.ToolStripMenuItem bTabsv;
        private System.Windows.Forms.ToolStripMenuItem bCammasv;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton bIncludeHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton bCopySel;
    }
}

