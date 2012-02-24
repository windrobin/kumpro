namespace EdMyFav {
    partial class EForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbDirs = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bEnum = new System.Windows.Forms.Button();
            this.bAddPC = new System.Windows.Forms.Button();
            this.flpwipNSE = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvPC = new System.Windows.Forms.ListView();
            this.chPC = new System.Windows.Forms.ColumnHeader();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.bwNSE = new System.ComponentModel.BackgroundWorker();
            this.bSave = new System.Windows.Forms.Button();
            this.bwAD = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flpwipShare = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lvDir = new System.Windows.Forms.ListView();
            this.chDir = new System.Windows.Forms.ColumnHeader();
            this.bwShare = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.flpwipNSE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.flpwipShare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "「あっちこっち」フォルダに次の場所を登録します：";
            // 
            // tbDirs
            // 
            this.tbDirs.AllowDrop = true;
            this.tbDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDirs.Location = new System.Drawing.Point(12, 24);
            this.tbDirs.Multiline = true;
            this.tbDirs.Name = "tbDirs";
            this.tbDirs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDirs.Size = new System.Drawing.Size(501, 164);
            this.tbDirs.TabIndex = 1;
            this.tbDirs.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbDirs_DragDrop);
            this.tbDirs.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbDirs_DragEnter);
            this.tbDirs.DragOver += new System.Windows.Forms.DragEventHandler(this.tbDirs_DragEnter);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.bEnum);
            this.groupBox1.Controls.Add(this.bAddPC);
            this.groupBox1.Controls.Add(this.flpwipNSE);
            this.groupBox1.Controls.Add(this.lvPC);
            this.groupBox1.Location = new System.Drawing.Point(14, 194);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 270);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ダブルクリックして、コンピュータを追加";
            // 
            // bEnum
            // 
            this.bEnum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bEnum.Image = global::EdMyFav.Properties.Resources.openfolderHS;
            this.bEnum.Location = new System.Drawing.Point(87, 195);
            this.bEnum.Name = "bEnum";
            this.bEnum.Size = new System.Drawing.Size(131, 46);
            this.bEnum.TabIndex = 2;
            this.bEnum.Text = "共有フォルダ一覧を右に表示する";
            this.bEnum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bEnum.UseVisualStyleBackColor = true;
            this.bEnum.Click += new System.EventHandler(this.bEnum_Click);
            // 
            // bAddPC
            // 
            this.bAddPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAddPC.Image = global::EdMyFav.Properties.Resources.Computer;
            this.bAddPC.Location = new System.Drawing.Point(6, 195);
            this.bAddPC.Name = "bAddPC";
            this.bAddPC.Size = new System.Drawing.Size(75, 46);
            this.bAddPC.TabIndex = 1;
            this.bAddPC.Text = "追加する";
            this.bAddPC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bAddPC.UseVisualStyleBackColor = true;
            this.bAddPC.Click += new System.EventHandler(this.bAddPC_Click);
            // 
            // flpwipNSE
            // 
            this.flpwipNSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpwipNSE.AutoSize = true;
            this.flpwipNSE.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpwipNSE.Controls.Add(this.pictureBox1);
            this.flpwipNSE.Controls.Add(this.label2);
            this.flpwipNSE.Location = new System.Drawing.Point(6, 247);
            this.flpwipNSE.Name = "flpwipNSE";
            this.flpwipNSE.Size = new System.Drawing.Size(124, 17);
            this.flpwipNSE.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Image = global::EdMyFav.Properties.Resources.status_anim;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 11);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "検索中です...";
            // 
            // lvPC
            // 
            this.lvPC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvPC.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPC});
            this.lvPC.FullRowSelect = true;
            this.lvPC.GridLines = true;
            this.lvPC.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvPC.Location = new System.Drawing.Point(6, 18);
            this.lvPC.MultiSelect = false;
            this.lvPC.Name = "lvPC";
            this.lvPC.Size = new System.Drawing.Size(212, 171);
            this.lvPC.SmallImageList = this.il;
            this.lvPC.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPC.TabIndex = 0;
            this.lvPC.UseCompatibleStateImageBehavior = false;
            this.lvPC.View = System.Windows.Forms.View.Details;
            this.lvPC.ItemActivate += new System.EventHandler(this.lvPC_ItemActivate);
            // 
            // chPC
            // 
            this.chPC.Text = "名称";
            this.chPC.Width = 150;
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "PC");
            this.il.Images.SetKeyName(1, "Dir");
            // 
            // bwNSE
            // 
            this.bwNSE.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwNSE_DoWork);
            this.bwNSE.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwNSE_RunWorkerCompleted);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Image = global::EdMyFav.Properties.Resources.saveHS;
            this.bSave.Location = new System.Drawing.Point(519, 24);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 46);
            this.bSave.TabIndex = 2;
            this.bSave.Text = "設定を保存";
            this.bSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bwAD
            // 
            this.bwAD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwAD_DoWork);
            this.bwAD.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwAD_RunWorkerCompleted);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.flpwipShare);
            this.groupBox2.Controls.Add(this.lvDir);
            this.groupBox2.Location = new System.Drawing.Point(244, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 270);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ダブルクリックして、共有フォルダを追加：";
            // 
            // flpwipShare
            // 
            this.flpwipShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpwipShare.AutoSize = true;
            this.flpwipShare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpwipShare.Controls.Add(this.pictureBox2);
            this.flpwipShare.Controls.Add(this.label3);
            this.flpwipShare.Location = new System.Drawing.Point(6, 247);
            this.flpwipShare.Name = "flpwipShare";
            this.flpwipShare.Size = new System.Drawing.Size(124, 17);
            this.flpwipShare.TabIndex = 1;
            this.flpwipShare.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox2.Image = global::EdMyFav.Properties.Resources.status_anim;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 11);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "検索中です...";
            // 
            // lvDir
            // 
            this.lvDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDir.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDir});
            this.lvDir.FullRowSelect = true;
            this.lvDir.GridLines = true;
            this.lvDir.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDir.Location = new System.Drawing.Point(6, 18);
            this.lvDir.MultiSelect = false;
            this.lvDir.Name = "lvDir";
            this.lvDir.Size = new System.Drawing.Size(255, 223);
            this.lvDir.SmallImageList = this.il;
            this.lvDir.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvDir.TabIndex = 0;
            this.lvDir.UseCompatibleStateImageBehavior = false;
            this.lvDir.View = System.Windows.Forms.View.Details;
            this.lvDir.ItemActivate += new System.EventHandler(this.lvDir_ItemActivate);
            // 
            // chDir
            // 
            this.chDir.Text = "名称";
            this.chDir.Width = 200;
            // 
            // bwShare
            // 
            this.bwShare.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwShare_DoWork);
            this.bwShare.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwShare_RunWorkerCompleted);
            // 
            // EForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.tbDirs);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(556, 366);
            this.Name = "EForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyFav 編集";
            this.Load += new System.EventHandler(this.EForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flpwipNSE.ResumeLayout(false);
            this.flpwipNSE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flpwipShare.ResumeLayout(false);
            this.flpwipShare.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDirs;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker bwNSE;
        private System.Windows.Forms.ListView lvPC;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.FlowLayoutPanel flpwipNSE;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bwAD;
        private System.Windows.Forms.ColumnHeader chPC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvDir;
        private System.Windows.Forms.ColumnHeader chDir;
        private System.Windows.Forms.FlowLayoutPanel flpwipShare;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bAddPC;
        private System.Windows.Forms.Button bEnum;
        private System.ComponentModel.BackgroundWorker bwShare;
    }
}

