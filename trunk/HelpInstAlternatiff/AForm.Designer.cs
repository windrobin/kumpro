namespace HelpInstAlternatiff {
    partial class AForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AForm));
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUACRaised = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslIL = new System.Windows.Forms.ToolStripStatusLabel();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.tstop = new System.Windows.Forms.ToolStrip();
            this.bRaise = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bInst = new System.Windows.Forms.ToolStripButton();
            this.bTest = new System.Windows.Forms.ToolStripButton();
            this.bReact = new System.Windows.Forms.ToolStripButton();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslIsAdmin = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsc.BottomToolStripPanel.SuspendLayout();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.ss.SuspendLayout();
            this.tstop.SuspendLayout();
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
            this.tsc.ContentPanel.Controls.Add(this.wb);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(812, 384);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(812, 431);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.tstop);
            // 
            // ss
            // 
            this.ss.Dock = System.Windows.Forms.DockStyle.None;
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslUACRaised,
            this.toolStripStatusLabel2,
            this.tsslIL,
            this.toolStripStatusLabel3,
            this.tsslIsAdmin});
            this.ss.Location = new System.Drawing.Point(0, 0);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(812, 22);
            this.ss.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "UAC状態：";
            // 
            // tsslUACRaised
            // 
            this.tsslUACRaised.Name = "tsslUACRaised";
            this.tsslUACRaised.Size = new System.Drawing.Size(10, 17);
            this.tsslUACRaised.Text = "?";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(12, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(76, 17);
            this.toolStripStatusLabel2.Text = "整合性レベル：";
            // 
            // tsslIL
            // 
            this.tsslIL.Name = "tsslIL";
            this.tsslIL.Size = new System.Drawing.Size(10, 17);
            this.tsslIL.Text = "?";
            // 
            // wb
            // 
            this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb.Location = new System.Drawing.Point(0, 0);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(812, 384);
            this.wb.TabIndex = 0;
            // 
            // tstop
            // 
            this.tstop.Dock = System.Windows.Forms.DockStyle.None;
            this.tstop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bRaise,
            this.toolStripSeparator1,
            this.bInst,
            this.bTest,
            this.bReact});
            this.tstop.Location = new System.Drawing.Point(3, 0);
            this.tstop.Name = "tstop";
            this.tstop.Size = new System.Drawing.Size(342, 25);
            this.tstop.TabIndex = 0;
            // 
            // bRaise
            // 
            this.bRaise.Image = ((System.Drawing.Image)(resources.GetObject("bRaise.Image")));
            this.bRaise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bRaise.Name = "bRaise";
            this.bRaise.Size = new System.Drawing.Size(74, 22);
            this.bRaise.Text = "昇格など...";
            this.bRaise.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bInst
            // 
            this.bInst.Image = ((System.Drawing.Image)(resources.GetObject("bInst.Image")));
            this.bInst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bInst.Name = "bInst";
            this.bInst.Size = new System.Drawing.Size(93, 22);
            this.bInst.Text = "Install into IE";
            this.bInst.Click += new System.EventHandler(this.bInst_Click);
            // 
            // bTest
            // 
            this.bTest.Image = ((System.Drawing.Image)(resources.GetObject("bTest.Image")));
            this.bTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bTest.Name = "bTest";
            this.bTest.Size = new System.Drawing.Size(77, 22);
            this.bTest.Text = "Test Page";
            this.bTest.Click += new System.EventHandler(this.bTest_Click);
            // 
            // bReact
            // 
            this.bReact.Image = ((System.Drawing.Image)(resources.GetObject("bReact.Image")));
            this.bReact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bReact.Name = "bReact";
            this.bReact.Size = new System.Drawing.Size(80, 22);
            this.bReact.Text = "Reactivate";
            this.bReact.Click += new System.EventHandler(this.bReact_Click);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(12, 3, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(52, 17);
            this.toolStripStatusLabel3.Text = "IsAdmin：";
            // 
            // tsslIsAdmin
            // 
            this.tsslIsAdmin.Name = "tsslIsAdmin";
            this.tsslIsAdmin.Size = new System.Drawing.Size(10, 17);
            this.tsslIsAdmin.Text = "?";
            // 
            // AForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 431);
            this.Controls.Add(this.tsc);
            this.Name = "AForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help install Alternatiff";
            this.Load += new System.EventHandler(this.AForm_Load);
            this.tsc.BottomToolStripPanel.ResumeLayout(false);
            this.tsc.BottomToolStripPanel.PerformLayout();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.tstop.ResumeLayout(false);
            this.tstop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.ToolStrip tstop;
        private System.Windows.Forms.ToolStripButton bInst;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.ToolStripButton bTest;
        private System.Windows.Forms.ToolStripButton bReact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bRaise;
        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslUACRaised;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslIL;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsslIsAdmin;
    }
}

