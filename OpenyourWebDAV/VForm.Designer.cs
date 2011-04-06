namespace OpenyourWebDAV {
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
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.lvF = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chSize = new System.Windows.Forms.ColumnHeader();
            this.chKind = new System.Windows.Forms.ColumnHeader();
            this.chMt = new System.Windows.Forms.ColumnHeader();
            this.cmsLvf = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mRenameSel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mDelSel = new System.Windows.Forms.ToolStripMenuItem();
            this.il16 = new System.Windows.Forms.ImageList(this.components);
            this.tstop = new System.Windows.Forms.ToolStrip();
            this.bNewFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.bGoHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bGoUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ddbView = new System.Windows.Forms.ToolStripSplitButton();
            this.bViewTile = new System.Windows.Forms.ToolStripMenuItem();
            this.bViewIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.bViewList = new System.Windows.Forms.ToolStripMenuItem();
            this.bViewDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsnav = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbUrl = new System.Windows.Forms.ToolStripTextBox();
            this.bNavi = new System.Windows.Forms.ToolStripButton();
            this.tsc.BottomToolStripPanel.SuspendLayout();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.cmsLvf.SuspendLayout();
            this.tstop.SuspendLayout();
            this.tsnav.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsc
            // 
            // 
            // tsc.BottomToolStripPanel
            // 
            this.tsc.BottomToolStripPanel.Controls.Add(this.ss);
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.lvF);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(627, 374);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(627, 456);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.tstop);
            this.tsc.TopToolStripPanel.Controls.Add(this.tsnav);
            // 
            // ss
            // 
            this.ss.Dock = System.Windows.Forms.DockStyle.None;
            this.ss.Location = new System.Drawing.Point(0, 0);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(627, 22);
            this.ss.TabIndex = 0;
            // 
            // lvF
            // 
            this.lvF.AllowDrop = true;
            this.lvF.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSize,
            this.chKind,
            this.chMt});
            this.lvF.ContextMenuStrip = this.cmsLvf;
            this.lvF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvF.FullRowSelect = true;
            this.lvF.GridLines = true;
            this.lvF.HideSelection = false;
            this.lvF.LargeImageList = this.il16;
            this.lvF.Location = new System.Drawing.Point(0, 0);
            this.lvF.Name = "lvF";
            this.lvF.Size = new System.Drawing.Size(627, 374);
            this.lvF.SmallImageList = this.il16;
            this.lvF.TabIndex = 0;
            this.lvF.UseCompatibleStateImageBehavior = false;
            this.lvF.View = System.Windows.Forms.View.Details;
            this.lvF.ItemActivate += new System.EventHandler(this.lvF_ItemActivate);
            this.lvF.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvF_DragDrop);
            this.lvF.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvF_ColumnClick);
            this.lvF.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvF_DragEnter);
            this.lvF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvF_KeyDown);
            this.lvF.DragOver += new System.Windows.Forms.DragEventHandler(this.lvF_DragOver);
            // 
            // chName
            // 
            this.chName.Text = "名称";
            this.chName.Width = 200;
            // 
            // chSize
            // 
            this.chSize.Text = "サイズ";
            this.chSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chSize.Width = 80;
            // 
            // chKind
            // 
            this.chKind.Text = "種類";
            this.chKind.Width = 80;
            // 
            // chMt
            // 
            this.chMt.Text = "更新日時";
            this.chMt.Width = 120;
            // 
            // cmsLvf
            // 
            this.cmsLvf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mRenameSel,
            this.toolStripSeparator4,
            this.mDelSel});
            this.cmsLvf.Name = "cmsLvf";
            this.cmsLvf.Size = new System.Drawing.Size(119, 54);
            this.cmsLvf.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLvf_Opening);
            // 
            // mRenameSel
            // 
            this.mRenameSel.Name = "mRenameSel";
            this.mRenameSel.Size = new System.Drawing.Size(118, 22);
            this.mRenameSel.Text = "名前変更";
            this.mRenameSel.Click += new System.EventHandler(this.mRenameSel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(115, 6);
            // 
            // mDelSel
            // 
            this.mDelSel.Image = global::OpenyourWebDAV.Properties.Resources.DeleteHS;
            this.mDelSel.Name = "mDelSel";
            this.mDelSel.Size = new System.Drawing.Size(118, 22);
            this.mDelSel.Text = "削除";
            this.mDelSel.Click += new System.EventHandler(this.mDelSel_Click);
            // 
            // il16
            // 
            this.il16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il16.ImageStream")));
            this.il16.TransparentColor = System.Drawing.Color.Transparent;
            this.il16.Images.SetKeyName(0, "0");
            this.il16.Images.SetKeyName(1, "D");
            this.il16.Images.SetKeyName(2, "F");
            // 
            // tstop
            // 
            this.tstop.Dock = System.Windows.Forms.DockStyle.None;
            this.tstop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bNewFolder,
            this.toolStripSeparator5,
            this.bGoUp,
            this.toolStripSeparator1,
            this.bRefresh,
            this.toolStripSeparator2,
            this.bGoHome,
            this.toolStripSeparator3,
            this.ddbView});
            this.tstop.Location = new System.Drawing.Point(3, 0);
            this.tstop.Name = "tstop";
            this.tstop.Size = new System.Drawing.Size(478, 35);
            this.tstop.TabIndex = 0;
            this.tstop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tstop_ItemClicked);
            // 
            // bNewFolder
            // 
            this.bNewFolder.Image = global::OpenyourWebDAV.Properties.Resources.NewFolderHS;
            this.bNewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNewFolder.Name = "bNewFolder";
            this.bNewFolder.Size = new System.Drawing.Size(74, 32);
            this.bNewFolder.Text = "フォルダを作る";
            this.bNewFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bNewFolder.Click += new System.EventHandler(this.bNewFolder_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 35);
            // 
            // bGoHome
            // 
            this.bGoHome.Image = global::OpenyourWebDAV.Properties.Resources.HomeHS;
            this.bGoHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bGoHome.Name = "bGoHome";
            this.bGoHome.Size = new System.Drawing.Size(108, 32);
            this.bGoHome.Text = "最初のフォルダに戻る";
            this.bGoHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bGoHome.Click += new System.EventHandler(this.bGoHome_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 35);
            // 
            // bGoUp
            // 
            this.bGoUp.Image = global::OpenyourWebDAV.Properties.Resources.GoToParentFolderHS;
            this.bGoUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bGoUp.Name = "bGoUp";
            this.bGoUp.Size = new System.Drawing.Size(84, 32);
            this.bGoUp.Text = "親フォルダへ行く";
            this.bGoUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bGoUp.Click += new System.EventHandler(this.bGoUp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // bRefresh
            // 
            this.bRefresh.Image = global::OpenyourWebDAV.Properties.Resources.RefreshDocViewHS;
            this.bRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(61, 32);
            this.bRefresh.Text = "最新にする";
            this.bRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // ddbView
            // 
            this.ddbView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bViewTile,
            this.bViewIcon,
            this.bViewList,
            this.bViewDetail});
            this.ddbView.Image = global::OpenyourWebDAV.Properties.Resources.ViewThumbnailsHS;
            this.ddbView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbView.Name = "ddbView";
            this.ddbView.Size = new System.Drawing.Size(84, 32);
            this.ddbView.Text = "見方を変える";
            this.ddbView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ddbView.ButtonClick += new System.EventHandler(this.ddbView_ButtonClick);
            // 
            // bViewTile
            // 
            this.bViewTile.Name = "bViewTile";
            this.bViewTile.Size = new System.Drawing.Size(125, 22);
            this.bViewTile.Text = "並べて表示";
            this.bViewTile.Click += new System.EventHandler(this.bViewTile_Click);
            // 
            // bViewIcon
            // 
            this.bViewIcon.Name = "bViewIcon";
            this.bViewIcon.Size = new System.Drawing.Size(125, 22);
            this.bViewIcon.Text = "アイコン";
            this.bViewIcon.Click += new System.EventHandler(this.bViewTile_Click);
            // 
            // bViewList
            // 
            this.bViewList.Name = "bViewList";
            this.bViewList.Size = new System.Drawing.Size(125, 22);
            this.bViewList.Text = "一覧";
            this.bViewList.Click += new System.EventHandler(this.bViewTile_Click);
            // 
            // bViewDetail
            // 
            this.bViewDetail.Checked = true;
            this.bViewDetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bViewDetail.Name = "bViewDetail";
            this.bViewDetail.Size = new System.Drawing.Size(125, 22);
            this.bViewDetail.Text = "詳細";
            this.bViewDetail.Click += new System.EventHandler(this.bViewTile_Click);
            // 
            // tsnav
            // 
            this.tsnav.Dock = System.Windows.Forms.DockStyle.None;
            this.tsnav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tbUrl,
            this.bNavi});
            this.tsnav.Location = new System.Drawing.Point(3, 35);
            this.tsnav.Name = "tsnav";
            this.tsnav.Size = new System.Drawing.Size(460, 25);
            this.tsnav.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "URL：";
            // 
            // tbUrl
            // 
            this.tbUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.tbUrl.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbUrl.HideSelection = false;
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(313, 25);
            // 
            // bNavi
            // 
            this.bNavi.Image = global::OpenyourWebDAV.Properties.Resources.FormRunHS;
            this.bNavi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNavi.Name = "bNavi";
            this.bNavi.Size = new System.Drawing.Size(100, 22);
            this.bNavi.Text = "URLへ移動する";
            this.bNavi.Click += new System.EventHandler(this.bNavi_Click);
            // 
            // VForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 456);
            this.Controls.Add(this.tsc);
            this.Name = "VForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open your WebDAV";
            this.Load += new System.EventHandler(this.VForm_Load);
            this.tsc.BottomToolStripPanel.ResumeLayout(false);
            this.tsc.BottomToolStripPanel.PerformLayout();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.cmsLvf.ResumeLayout(false);
            this.tstop.ResumeLayout(false);
            this.tstop.PerformLayout();
            this.tsnav.ResumeLayout(false);
            this.tsnav.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.ToolStrip tstop;
        private System.Windows.Forms.ListView lvF;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chKind;
        private System.Windows.Forms.ColumnHeader chMt;
        private System.Windows.Forms.ImageList il16;
        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bGoUp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bRefresh;
        private System.Windows.Forms.ContextMenuStrip cmsLvf;
        private System.Windows.Forms.ToolStripMenuItem mDelSel;
        private System.Windows.Forms.ToolStripButton bNewFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip tsnav;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbUrl;
        private System.Windows.Forms.ToolStripButton bNavi;
        private System.Windows.Forms.ToolStripSplitButton ddbView;
        private System.Windows.Forms.ToolStripMenuItem bViewTile;
        private System.Windows.Forms.ToolStripMenuItem bViewIcon;
        private System.Windows.Forms.ToolStripMenuItem bViewList;
        private System.Windows.Forms.ToolStripMenuItem bViewDetail;
        private System.Windows.Forms.ToolStripMenuItem mRenameSel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton bGoHome;
    }
}

