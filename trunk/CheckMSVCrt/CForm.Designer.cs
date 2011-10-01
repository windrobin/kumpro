namespace CheckMSVCrt {
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
            this.llAPI = new System.Windows.Forms.LinkLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.lvR = new System.Windows.Forms.ListView();
            this.chProduct = new System.Windows.Forms.ColumnHeader();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chIS = new System.Windows.Forms.ColumnHeader();
            this.chDL = new System.Windows.Forms.ColumnHeader();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.flpLinks = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.tstop = new System.Windows.Forms.ToolStrip();
            this.bRefresh = new System.Windows.Forms.ToolStripButton();
            this.bCopyCsv = new System.Windows.Forms.ToolStripButton();
            this.tsact = new System.Windows.Forms.ToolStrip();
            this.bAppwiz = new System.Windows.Forms.ToolStripButton();
            this.ttLink = new System.Windows.Forms.ToolTip(this.components);
            this.mOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chVer = new System.Windows.Forms.ColumnHeader();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.flpLinks.SuspendLayout();
            this.tstop.SuspendLayout();
            this.tsact.SuspendLayout();
            this.SuspendLayout();
            // 
            // llAPI
            // 
            this.llAPI.AutoSize = true;
            this.llAPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.llAPI.LinkArea = new System.Windows.Forms.LinkArea(0, 20);
            this.llAPI.Location = new System.Drawing.Point(0, 0);
            this.llAPI.Name = "llAPI";
            this.llAPI.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.llAPI.Size = new System.Drawing.Size(207, 23);
            this.llAPI.TabIndex = 0;
            this.llAPI.TabStop = true;
            this.llAPI.Text = "MsiQueryProductStateによる検定結果：";
            this.ttLink.SetToolTip(this.llAPI, "http://msdn.microsoft.com/en-us/library/aa370363.aspx");
            this.llAPI.UseCompatibleTextRendering = true;
            this.llAPI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lvR);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.flpLinks);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.llAPI);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(643, 567);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(643, 592);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tstop);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsact);
            // 
            // lvR
            // 
            this.lvR.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProduct,
            this.chVer,
            this.chID,
            this.chIS,
            this.chDL});
            this.lvR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvR.FullRowSelect = true;
            this.lvR.GridLines = true;
            this.lvR.Location = new System.Drawing.Point(0, 23);
            this.lvR.MultiSelect = false;
            this.lvR.Name = "lvR";
            this.lvR.Size = new System.Drawing.Size(643, 459);
            this.lvR.SmallImageList = this.il;
            this.lvR.TabIndex = 3;
            this.lvR.UseCompatibleStateImageBehavior = false;
            this.lvR.View = System.Windows.Forms.View.Details;
            this.lvR.ItemActivate += new System.EventHandler(this.lvR_ItemActivate);
            // 
            // chProduct
            // 
            this.chProduct.Text = "製品";
            this.chProduct.Width = 230;
            // 
            // chID
            // 
            this.chID.Text = "GUID";
            // 
            // chIS
            // 
            this.chIS.Text = "INSTALLSTATE";
            this.chIS.Width = 100;
            // 
            // chDL
            // 
            this.chDL.Text = "DL";
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "Y");
            this.il.Images.SetKeyName(1, "N");
            this.il.Images.SetKeyName(2, "Web");
            // 
            // flpLinks
            // 
            this.flpLinks.AutoSize = true;
            this.flpLinks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpLinks.Controls.Add(this.label1);
            this.flpLinks.Controls.Add(this.linkLabel1);
            this.flpLinks.Controls.Add(this.linkLabel2);
            this.flpLinks.Controls.Add(this.linkLabel3);
            this.flpLinks.Controls.Add(this.linkLabel4);
            this.flpLinks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpLinks.Location = new System.Drawing.Point(0, 482);
            this.flpLinks.Name = "flpLinks";
            this.flpLinks.Size = new System.Drawing.Size(643, 85);
            this.flpLinks.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.flpLinks.SetFlowBreak(this.label1, true);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "References, links:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(2, 42);
            this.linkLabel1.Location = new System.Drawing.Point(3, 17);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(232, 17);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "* How to detect VC++ 2008 redistributable?";
            this.ttLink.SetToolTip(this.linkLabel1, "http://stackoverflow.com/questions/203195/how-to-detect-vc-2008-redistributable");
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(2, 84);
            this.linkLabel2.Location = new System.Drawing.Point(3, 34);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(448, 17);
            this.linkLabel2.TabIndex = 1;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "* Mailbag: How to detect the presence of the Visual C++ 2010 redistributable pack" +
                "age";
            this.ttLink.SetToolTip(this.linkLabel2, "http://blogs.msdn.com/b/astebner/archive/2010/05/05/10008146.aspx");
            this.linkLabel2.UseCompatibleTextRendering = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.LinkArea = new System.Windows.Forms.LinkArea(2, 91);
            this.linkLabel3.Location = new System.Drawing.Point(3, 51);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(481, 17);
            this.linkLabel3.TabIndex = 3;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "* Mailbag: How to detect the presence of the Visual C++ 9.0 runtime redistributab" +
                "le package";
            this.ttLink.SetToolTip(this.linkLabel3, "http://blogs.msdn.com/b/astebner/archive/2009/01/29/9384143.aspx");
            this.linkLabel3.UseCompatibleTextRendering = true;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.LinkArea = new System.Windows.Forms.LinkArea(2, 83);
            this.linkLabel4.Location = new System.Drawing.Point(3, 68);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(442, 17);
            this.linkLabel4.TabIndex = 4;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "* Mailbag: How to detect the presence of the VC 8.0 runtime redistributable packa" +
                "ge";
            this.ttLink.SetToolTip(this.linkLabel4, "http://blogs.msdn.com/b/astebner/archive/2007/01/16/mailbag-how-to-detect-the-pre" +
                    "sence-of-the-vc-8-0-runtime-redistributable-package.aspx");
            this.linkLabel4.UseCompatibleTextRendering = true;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tstop
            // 
            this.tstop.Dock = System.Windows.Forms.DockStyle.None;
            this.tstop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRefresh,
            this.bCopyCsv});
            this.tstop.Location = new System.Drawing.Point(175, 0);
            this.tstop.Name = "tstop";
            this.tstop.Size = new System.Drawing.Size(149, 25);
            this.tstop.TabIndex = 0;
            // 
            // bRefresh
            // 
            this.bRefresh.Image = global::CheckMSVCrt.Properties.Resources.RefreshDocViewHS;
            this.bRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(52, 22);
            this.bRefresh.Text = "更新";
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // bCopyCsv
            // 
            this.bCopyCsv.Image = global::CheckMSVCrt.Properties.Resources.CopyHS;
            this.bCopyCsv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCopyCsv.Name = "bCopyCsv";
            this.bCopyCsv.Size = new System.Drawing.Size(85, 22);
            this.bCopyCsv.Text = "&Copy CSV";
            this.bCopyCsv.Click += new System.EventHandler(this.bCopyCsv_Click);
            // 
            // tsact
            // 
            this.tsact.Dock = System.Windows.Forms.DockStyle.None;
            this.tsact.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bAppwiz});
            this.tsact.Location = new System.Drawing.Point(3, 0);
            this.tsact.Name = "tsact";
            this.tsact.Size = new System.Drawing.Size(172, 25);
            this.tsact.TabIndex = 1;
            // 
            // bAppwiz
            // 
            this.bAppwiz.Image = ((System.Drawing.Image)(resources.GetObject("bAppwiz.Image")));
            this.bAppwiz.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bAppwiz.Name = "bAppwiz";
            this.bAppwiz.Size = new System.Drawing.Size(160, 22);
            this.bAppwiz.Text = "プログラムの追加と削除";
            this.bAppwiz.Click += new System.EventHandler(this.bAppwiz_Click);
            // 
            // mOpen
            // 
            this.mOpen.Name = "cmsOpen";
            this.mOpen.Size = new System.Drawing.Size(61, 4);
            // 
            // chVer
            // 
            this.chVer.Text = "Ver";
            this.chVer.Width = 100;
            // 
            // CForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 592);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "CForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ckeck MSVCrt";
            this.Load += new System.EventHandler(this.CForm_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.flpLinks.ResumeLayout(false);
            this.flpLinks.PerformLayout();
            this.tstop.ResumeLayout(false);
            this.tstop.PerformLayout();
            this.tsact.ResumeLayout(false);
            this.tsact.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel llAPI;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip tstop;
        private System.Windows.Forms.ToolStripButton bRefresh;
        private System.Windows.Forms.ToolStripButton bCopyCsv;
        private System.Windows.Forms.ListView lvR;
        private System.Windows.Forms.ColumnHeader chProduct;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chIS;
        private System.Windows.Forms.FlowLayoutPanel flpLinks;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ToolTip ttLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.ColumnHeader chDL;
        private System.Windows.Forms.ContextMenuStrip mOpen;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.ToolStrip tsact;
        private System.Windows.Forms.ToolStripButton bAppwiz;
        private System.Windows.Forms.ColumnHeader chVer;

    }
}

