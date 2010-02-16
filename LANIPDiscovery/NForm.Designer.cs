namespace LANIPDiscovery {
    partial class NForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NForm));
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.lvl = new System.Windows.Forms.ListView();
            this.chAddr = new System.Windows.Forms.ColumnHeader();
            this.chStat = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mstscadminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mstscconsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mstscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vncviewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsc
            // 
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.lvl);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(533, 254);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(533, 279);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // lvl
            // 
            this.lvl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAddr,
            this.chStat,
            this.chName});
            this.lvl.ContextMenuStrip = this.menu;
            this.lvl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvl.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvl.FullRowSelect = true;
            this.lvl.GridLines = true;
            this.lvl.Location = new System.Drawing.Point(0, 0);
            this.lvl.MultiSelect = false;
            this.lvl.Name = "lvl";
            this.lvl.Size = new System.Drawing.Size(533, 254);
            this.lvl.TabIndex = 0;
            this.lvl.UseCompatibleStateImageBehavior = false;
            this.lvl.View = System.Windows.Forms.View.Details;
            this.lvl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvl_MouseDown);
            // 
            // chAddr
            // 
            this.chAddr.Text = "アドレス";
            this.chAddr.Width = 120;
            // 
            // chStat
            // 
            this.chStat.Text = "状態";
            this.chStat.Width = 180;
            // 
            // chName
            // 
            this.chName.Text = "NetBIOS";
            this.chName.Width = 200;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEdit,
            this.tsbRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(175, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbEdit
            // 
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(81, 22);
            this.tsbEdit.Text = "編集 Alt+&E";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(82, 22);
            this.tsbRefresh.Text = "更新 Alt+&R";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mstscadminToolStripMenuItem,
            this.mstscconsoleToolStripMenuItem,
            this.mstscToolStripMenuItem,
            this.vncviewerToolStripMenuItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(153, 114);
            // 
            // mstscadminToolStripMenuItem
            // 
            this.mstscadminToolStripMenuItem.Name = "mstscadminToolStripMenuItem";
            this.mstscadminToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mstscadminToolStripMenuItem.Text = "mstsc /&admin";
            this.mstscadminToolStripMenuItem.Click += new System.EventHandler(this.mstscadminToolStripMenuItem_Click);
            // 
            // mstscconsoleToolStripMenuItem
            // 
            this.mstscconsoleToolStripMenuItem.Name = "mstscconsoleToolStripMenuItem";
            this.mstscconsoleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mstscconsoleToolStripMenuItem.Text = "mstsc /&console";
            this.mstscconsoleToolStripMenuItem.Click += new System.EventHandler(this.mstscconsoleToolStripMenuItem_Click);
            // 
            // mstscToolStripMenuItem
            // 
            this.mstscToolStripMenuItem.Name = "mstscToolStripMenuItem";
            this.mstscToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mstscToolStripMenuItem.Text = "mst&sc";
            this.mstscToolStripMenuItem.Click += new System.EventHandler(this.mstscToolStripMenuItem_Click);
            // 
            // vncviewerToolStripMenuItem
            // 
            this.vncviewerToolStripMenuItem.Name = "vncviewerToolStripMenuItem";
            this.vncviewerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vncviewerToolStripMenuItem.Text = "vncviewer";
            this.vncviewerToolStripMenuItem.Click += new System.EventHandler(this.vncviewerToolStripMenuItem_Click);
            // 
            // NForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 279);
            this.Controls.Add(this.tsc);
            this.Name = "NForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LANIPDiscovery";
            this.Load += new System.EventHandler(this.NForm_Load);
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ListView lvl;
        private System.Windows.Forms.ColumnHeader chAddr;
        private System.Windows.Forms.ColumnHeader chStat;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mstscadminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mstscconsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mstscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vncviewerToolStripMenuItem;
    }
}

