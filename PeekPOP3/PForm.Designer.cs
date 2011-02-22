namespace PeekPOP3 {
    partial class PForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PForm));
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.tstop = new System.Windows.Forms.ToolStrip();
            this.bConn = new System.Windows.Forms.ToolStripButton();
            this.vsc = new System.Windows.Forms.SplitContainer();
            this.lvm = new System.Windows.Forms.ListView();
            this.tbEML = new System.Windows.Forms.TextBox();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.bwResolver = new System.ComponentModel.BackgroundWorker();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.bResolver = new System.Windows.Forms.ToolStripButton();
            this.chSubject = new System.Windows.Forms.ColumnHeader();
            this.chCb = new System.Windows.Forms.ColumnHeader();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bSaveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsc.BottomToolStripPanel.SuspendLayout();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.tstop.SuspendLayout();
            this.vsc.Panel1.SuspendLayout();
            this.vsc.Panel2.SuspendLayout();
            this.vsc.SuspendLayout();
            this.ss.SuspendLayout();
            this.cms.SuspendLayout();
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
            this.tsc.ContentPanel.Controls.Add(this.vsc);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(973, 519);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(973, 566);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.tstop);
            // 
            // tstop
            // 
            this.tstop.Dock = System.Windows.Forms.DockStyle.None;
            this.tstop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bConn,
            this.bResolver});
            this.tstop.Location = new System.Drawing.Point(3, 0);
            this.tstop.Name = "tstop";
            this.tstop.Size = new System.Drawing.Size(162, 25);
            this.tstop.TabIndex = 0;
            // 
            // bConn
            // 
            this.bConn.Image = ((System.Drawing.Image)(resources.GetObject("bConn.Image")));
            this.bConn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bConn.Name = "bConn";
            this.bConn.Size = new System.Drawing.Size(77, 22);
            this.bConn.Text = "POP3接続";
            this.bConn.Click += new System.EventHandler(this.bConn_Click);
            // 
            // vsc
            // 
            this.vsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vsc.Location = new System.Drawing.Point(0, 0);
            this.vsc.Name = "vsc";
            // 
            // vsc.Panel1
            // 
            this.vsc.Panel1.Controls.Add(this.lvm);
            // 
            // vsc.Panel2
            // 
            this.vsc.Panel2.Controls.Add(this.tbEML);
            this.vsc.Size = new System.Drawing.Size(973, 519);
            this.vsc.SplitterDistance = 389;
            this.vsc.TabIndex = 0;
            // 
            // lvm
            // 
            this.lvm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSubject,
            this.chCb});
            this.lvm.ContextMenuStrip = this.cms;
            this.lvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvm.FullRowSelect = true;
            this.lvm.GridLines = true;
            this.lvm.HideSelection = false;
            this.lvm.LargeImageList = this.il;
            this.lvm.Location = new System.Drawing.Point(0, 0);
            this.lvm.Name = "lvm";
            this.lvm.Size = new System.Drawing.Size(389, 519);
            this.lvm.SmallImageList = this.il;
            this.lvm.TabIndex = 0;
            this.lvm.UseCompatibleStateImageBehavior = false;
            this.lvm.View = System.Windows.Forms.View.Details;
            this.lvm.SelectedIndexChanged += new System.EventHandler(this.lvm_SelectedIndexChanged);
            // 
            // tbEML
            // 
            this.tbEML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEML.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbEML.Location = new System.Drawing.Point(0, 0);
            this.tbEML.Multiline = true;
            this.tbEML.Name = "tbEML";
            this.tbEML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEML.Size = new System.Drawing.Size(580, 519);
            this.tbEML.TabIndex = 0;
            this.tbEML.WordWrap = false;
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "IPML.ICO");
            this.il.Images.SetKeyName(1, "mail.ico");
            // 
            // bwResolver
            // 
            this.bwResolver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwResolver_DoWork);
            // 
            // ss
            // 
            this.ss.Dock = System.Windows.Forms.DockStyle.None;
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lSize});
            this.ss.Location = new System.Drawing.Point(0, 0);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(973, 22);
            this.ss.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "Mail Size:";
            // 
            // lSize
            // 
            this.lSize.Name = "lSize";
            this.lSize.Size = new System.Drawing.Size(11, 17);
            this.lSize.Text = "...";
            // 
            // bResolver
            // 
            this.bResolver.Image = ((System.Drawing.Image)(resources.GetObject("bResolver.Image")));
            this.bResolver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bResolver.Name = "bResolver";
            this.bResolver.Size = new System.Drawing.Size(73, 22);
            this.bResolver.Text = "件名解決";
            this.bResolver.Click += new System.EventHandler(this.bResolver_Click);
            // 
            // chSubject
            // 
            this.chSubject.Text = "件名";
            this.chSubject.Width = 277;
            // 
            // chCb
            // 
            this.chCb.Text = "サイズ";
            this.chCb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chCb.Width = 71;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSaveTo});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(153, 48);
            this.cms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_Opening);
            // 
            // bSaveTo
            // 
            this.bSaveTo.Name = "bSaveTo";
            this.bSaveTo.Size = new System.Drawing.Size(152, 22);
            this.bSaveTo.Text = "保存する...";
            this.bSaveTo.Click += new System.EventHandler(this.bSaveTo_Click);
            // 
            // PForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 566);
            this.Controls.Add(this.tsc);
            this.Name = "PForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Peek POP3";
            this.Load += new System.EventHandler(this.PForm_Load);
            this.tsc.BottomToolStripPanel.ResumeLayout(false);
            this.tsc.BottomToolStripPanel.PerformLayout();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.tstop.ResumeLayout(false);
            this.tstop.PerformLayout();
            this.vsc.Panel1.ResumeLayout(false);
            this.vsc.Panel2.ResumeLayout(false);
            this.vsc.Panel2.PerformLayout();
            this.vsc.ResumeLayout(false);
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.SplitContainer vsc;
        private System.Windows.Forms.ListView lvm;
        private System.Windows.Forms.TextBox tbEML;
        private System.Windows.Forms.ToolStrip tstop;
        private System.Windows.Forms.ToolStripButton bConn;
        private System.Windows.Forms.ImageList il;
        private System.ComponentModel.BackgroundWorker bwResolver;
        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lSize;
        private System.Windows.Forms.ToolStripButton bResolver;
        private System.Windows.Forms.ColumnHeader chSubject;
        private System.Windows.Forms.ColumnHeader chCb;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem bSaveTo;
    }
}

