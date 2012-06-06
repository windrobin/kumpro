namespace INetCfg_viewer {
    partial class VForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VForm));
            this.gv = new System.Windows.Forms.DataGridView();
            this.displayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.helpTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.characteristicsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instanceGuidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnpDevNodeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classGuidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ds = new INetCfg_viewer.DS();
            this.rbGUID_DEVCLASS_NET = new System.Windows.Forms.RadioButton();
            this.rbGUID_DEVCLASS_NETTRANS = new System.Windows.Forms.RadioButton();
            this.rbGUID_DEVCLASS_NETSERVICE = new System.Windows.Forms.RadioButton();
            this.rbGUID_DEVCLASS_NETCLIENT = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvP = new System.Windows.Forms.ListView();
            this.chPath = new System.Windows.Forms.ColumnHeader();
            this.il16 = new System.Windows.Forms.ImageList(this.components);
            this.cbPath = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.AllowUserToDeleteRows = false;
            this.gv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv.AutoGenerateColumns = false;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayNameDataGridViewTextBoxColumn,
            this.helpTextDataGridViewTextBoxColumn,
            this.cId,
            this.characteristicsDataGridViewTextBoxColumn,
            this.instanceGuidDataGridViewTextBoxColumn,
            this.pnpDevNodeIdDataGridViewTextBoxColumn,
            this.classGuidDataGridViewTextBoxColumn,
            this.bindNameDataGridViewTextBoxColumn});
            this.gv.DataSource = this.componentBindingSource;
            this.gv.Location = new System.Drawing.Point(12, 12);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 21;
            this.gv.Size = new System.Drawing.Size(630, 186);
            this.gv.TabIndex = 1;
            this.gv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_RowEnter);
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            this.displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.HeaderText = "DisplayName";
            this.displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            // 
            // helpTextDataGridViewTextBoxColumn
            // 
            this.helpTextDataGridViewTextBoxColumn.DataPropertyName = "HelpText";
            this.helpTextDataGridViewTextBoxColumn.HeaderText = "HelpText";
            this.helpTextDataGridViewTextBoxColumn.Name = "helpTextDataGridViewTextBoxColumn";
            // 
            // cId
            // 
            this.cId.DataPropertyName = "Id";
            this.cId.HeaderText = "Id";
            this.cId.Name = "cId";
            // 
            // characteristicsDataGridViewTextBoxColumn
            // 
            this.characteristicsDataGridViewTextBoxColumn.DataPropertyName = "Characteristics";
            this.characteristicsDataGridViewTextBoxColumn.HeaderText = "Characteristics";
            this.characteristicsDataGridViewTextBoxColumn.Name = "characteristicsDataGridViewTextBoxColumn";
            // 
            // instanceGuidDataGridViewTextBoxColumn
            // 
            this.instanceGuidDataGridViewTextBoxColumn.DataPropertyName = "InstanceGuid";
            this.instanceGuidDataGridViewTextBoxColumn.HeaderText = "InstanceGuid";
            this.instanceGuidDataGridViewTextBoxColumn.Name = "instanceGuidDataGridViewTextBoxColumn";
            // 
            // pnpDevNodeIdDataGridViewTextBoxColumn
            // 
            this.pnpDevNodeIdDataGridViewTextBoxColumn.DataPropertyName = "PnpDevNodeId";
            this.pnpDevNodeIdDataGridViewTextBoxColumn.HeaderText = "PnpDevNodeId";
            this.pnpDevNodeIdDataGridViewTextBoxColumn.Name = "pnpDevNodeIdDataGridViewTextBoxColumn";
            // 
            // classGuidDataGridViewTextBoxColumn
            // 
            this.classGuidDataGridViewTextBoxColumn.DataPropertyName = "ClassGuid";
            this.classGuidDataGridViewTextBoxColumn.HeaderText = "ClassGuid";
            this.classGuidDataGridViewTextBoxColumn.Name = "classGuidDataGridViewTextBoxColumn";
            // 
            // bindNameDataGridViewTextBoxColumn
            // 
            this.bindNameDataGridViewTextBoxColumn.DataPropertyName = "BindName";
            this.bindNameDataGridViewTextBoxColumn.HeaderText = "BindName";
            this.bindNameDataGridViewTextBoxColumn.Name = "bindNameDataGridViewTextBoxColumn";
            // 
            // componentBindingSource
            // 
            this.componentBindingSource.DataMember = "Component";
            this.componentBindingSource.DataSource = this.ds;
            // 
            // ds
            // 
            this.ds.DataSetName = "DS";
            this.ds.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rbGUID_DEVCLASS_NET
            // 
            this.rbGUID_DEVCLASS_NET.AutoSize = true;
            this.rbGUID_DEVCLASS_NET.Checked = true;
            this.rbGUID_DEVCLASS_NET.Location = new System.Drawing.Point(6, 18);
            this.rbGUID_DEVCLASS_NET.Name = "rbGUID_DEVCLASS_NET";
            this.rbGUID_DEVCLASS_NET.Size = new System.Drawing.Size(139, 16);
            this.rbGUID_DEVCLASS_NET.TabIndex = 2;
            this.rbGUID_DEVCLASS_NET.TabStop = true;
            this.rbGUID_DEVCLASS_NET.Text = "GUID_DEVCLASS_NET";
            this.rbGUID_DEVCLASS_NET.UseVisualStyleBackColor = true;
            this.rbGUID_DEVCLASS_NET.CheckedChanged += new System.EventHandler(this.rbGUID_DEVCLASS_NET_CheckedChanged);
            // 
            // rbGUID_DEVCLASS_NETTRANS
            // 
            this.rbGUID_DEVCLASS_NETTRANS.AutoSize = true;
            this.rbGUID_DEVCLASS_NETTRANS.Location = new System.Drawing.Point(6, 40);
            this.rbGUID_DEVCLASS_NETTRANS.Name = "rbGUID_DEVCLASS_NETTRANS";
            this.rbGUID_DEVCLASS_NETTRANS.Size = new System.Drawing.Size(177, 16);
            this.rbGUID_DEVCLASS_NETTRANS.TabIndex = 3;
            this.rbGUID_DEVCLASS_NETTRANS.TabStop = true;
            this.rbGUID_DEVCLASS_NETTRANS.Text = "GUID_DEVCLASS_NETTRANS";
            this.rbGUID_DEVCLASS_NETTRANS.UseVisualStyleBackColor = true;
            this.rbGUID_DEVCLASS_NETTRANS.CheckedChanged += new System.EventHandler(this.rbGUID_DEVCLASS_NET_CheckedChanged);
            // 
            // rbGUID_DEVCLASS_NETSERVICE
            // 
            this.rbGUID_DEVCLASS_NETSERVICE.AutoSize = true;
            this.rbGUID_DEVCLASS_NETSERVICE.Location = new System.Drawing.Point(6, 62);
            this.rbGUID_DEVCLASS_NETSERVICE.Name = "rbGUID_DEVCLASS_NETSERVICE";
            this.rbGUID_DEVCLASS_NETSERVICE.Size = new System.Drawing.Size(187, 16);
            this.rbGUID_DEVCLASS_NETSERVICE.TabIndex = 4;
            this.rbGUID_DEVCLASS_NETSERVICE.TabStop = true;
            this.rbGUID_DEVCLASS_NETSERVICE.Text = "GUID_DEVCLASS_NETSERVICE";
            this.rbGUID_DEVCLASS_NETSERVICE.UseVisualStyleBackColor = true;
            this.rbGUID_DEVCLASS_NETSERVICE.CheckedChanged += new System.EventHandler(this.rbGUID_DEVCLASS_NET_CheckedChanged);
            // 
            // rbGUID_DEVCLASS_NETCLIENT
            // 
            this.rbGUID_DEVCLASS_NETCLIENT.AutoSize = true;
            this.rbGUID_DEVCLASS_NETCLIENT.Location = new System.Drawing.Point(6, 84);
            this.rbGUID_DEVCLASS_NETCLIENT.Name = "rbGUID_DEVCLASS_NETCLIENT";
            this.rbGUID_DEVCLASS_NETCLIENT.Size = new System.Drawing.Size(178, 16);
            this.rbGUID_DEVCLASS_NETCLIENT.TabIndex = 5;
            this.rbGUID_DEVCLASS_NETCLIENT.TabStop = true;
            this.rbGUID_DEVCLASS_NETCLIENT.Text = "GUID_DEVCLASS_NETCLIENT";
            this.rbGUID_DEVCLASS_NETCLIENT.UseVisualStyleBackColor = true;
            this.rbGUID_DEVCLASS_NETCLIENT.CheckedChanged += new System.EventHandler(this.rbGUID_DEVCLASS_NET_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.rbGUID_DEVCLASS_NET);
            this.groupBox1.Controls.Add(this.rbGUID_DEVCLASS_NETCLIENT);
            this.groupBox1.Controls.Add(this.rbGUID_DEVCLASS_NETTRANS);
            this.groupBox1.Controls.Add(this.rbGUID_DEVCLASS_NETSERVICE);
            this.groupBox1.Location = new System.Drawing.Point(12, 204);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 113);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device class";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lvP);
            this.groupBox2.Controls.Add(this.cbPath);
            this.groupBox2.Location = new System.Drawing.Point(251, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(391, 241);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Above, Me and Below";
            // 
            // lvP
            // 
            this.lvP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPath});
            this.lvP.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvP.Location = new System.Drawing.Point(6, 44);
            this.lvP.Name = "lvP";
            this.lvP.Size = new System.Drawing.Size(379, 191);
            this.lvP.SmallImageList = this.il16;
            this.lvP.TabIndex = 1;
            this.lvP.UseCompatibleStateImageBehavior = false;
            this.lvP.View = System.Windows.Forms.View.Details;
            // 
            // chPath
            // 
            this.chPath.Text = "Path";
            // 
            // il16
            // 
            this.il16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il16.ImageStream")));
            this.il16.TransparentColor = System.Drawing.Color.Transparent;
            this.il16.Images.SetKeyName(0, "ES");
            this.il16.Images.SetKeyName(1, "C");
            // 
            // cbPath
            // 
            this.cbPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPath.FormattingEnabled = true;
            this.cbPath.Location = new System.Drawing.Point(6, 18);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(379, 20);
            this.cbPath.TabIndex = 0;
            this.cbPath.SelectedIndexChanged += new System.EventHandler(this.cbPath_SelectedIndexChanged);
            // 
            // VForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gv);
            this.Name = "VForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INetCfg viewer";
            this.Load += new System.EventHandler(this.VForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.componentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.BindingSource componentBindingSource;
        private DS ds;
        private System.Windows.Forms.RadioButton rbGUID_DEVCLASS_NET;
        private System.Windows.Forms.RadioButton rbGUID_DEVCLASS_NETTRANS;
        private System.Windows.Forms.RadioButton rbGUID_DEVCLASS_NETSERVICE;
        private System.Windows.Forms.RadioButton rbGUID_DEVCLASS_NETCLIENT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn helpTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cId;
        private System.Windows.Forms.DataGridViewTextBoxColumn characteristicsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn instanceGuidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pnpDevNodeIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn classGuidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bindNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbPath;
        private System.Windows.Forms.ListView lvP;
        private System.Windows.Forms.ImageList il16;
        private System.Windows.Forms.ColumnHeader chPath;

    }
}

