namespace Tablega {
    partial class TForm {
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
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.lStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bBack = new System.Windows.Forms.ToolStripButton();
            this.bNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbUrl = new System.Windows.Forms.ToolStripTextBox();
            this.bGo2Url = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bExport = new System.Windows.Forms.ToolStripButton();
            this.tsc.BottomToolStripPanel.SuspendLayout();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.ss.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.tsc.ContentPanel.Size = new System.Drawing.Size(769, 437);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(769, 487);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // ss
            // 
            this.ss.Dock = System.Windows.Forms.DockStyle.None;
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lStat});
            this.ss.Location = new System.Drawing.Point(0, 0);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(769, 22);
            this.ss.TabIndex = 0;
            // 
            // lStat
            // 
            this.lStat.Name = "lStat";
            this.lStat.Size = new System.Drawing.Size(11, 17);
            this.lStat.Text = "...";
            // 
            // wb
            // 
            this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb.Location = new System.Drawing.Point(0, 0);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.ScriptErrorsSuppressed = true;
            this.wb.Size = new System.Drawing.Size(769, 437);
            this.wb.TabIndex = 0;
            this.wb.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.wb_ProgressChanged);
            this.wb.LocationChanged += new System.EventHandler(this.wb_LocationChanged);
            this.wb.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wb_Navigated);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bBack,
            this.bNext,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tbUrl,
            this.bGo2Url,
            this.toolStripSeparator1,
            this.bExport});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(709, 28);
            this.toolStrip1.TabIndex = 0;
            // 
            // bBack
            // 
            this.bBack.Image = global::Tablega.Properties.Resources.NavBack;
            this.bBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(46, 25);
            this.bBack.Text = "戻る";
            this.bBack.Click += new System.EventHandler(this.bBack_Click);
            // 
            // bNext
            // 
            this.bNext.Image = global::Tablega.Properties.Resources.NavForward;
            this.bNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(47, 25);
            this.bNext.Text = "進む";
            this.bNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(29, 25);
            this.toolStripLabel1.Text = "URL:";
            // 
            // tbUrl
            // 
            this.tbUrl.Font = new System.Drawing.Font("Lucida Handwriting", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(355, 28);
            // 
            // bGo2Url
            // 
            this.bGo2Url.Image = global::Tablega.Properties.Resources.DataContainer_MoveNextHS;
            this.bGo2Url.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bGo2Url.Name = "bGo2Url";
            this.bGo2Url.Size = new System.Drawing.Size(81, 25);
            this.bGo2Url.Text = "URLへ移動";
            this.bGo2Url.Click += new System.EventHandler(this.bGo2Url_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // bExport
            // 
            this.bExport.Image = global::Tablega.Properties.Resources.TableHS;
            this.bExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(94, 25);
            this.bExport.Text = "Table掘り出し";
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // TForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 487);
            this.Controls.Add(this.tsc);
            this.Name = "TForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tableガ";
            this.Load += new System.EventHandler(this.TForm_Load);
            this.tsc.BottomToolStripPanel.ResumeLayout(false);
            this.tsc.BottomToolStripPanel.PerformLayout();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbUrl;
        private System.Windows.Forms.ToolStripButton bGo2Url;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bExport;
        private System.Windows.Forms.ToolStripStatusLabel lStat;
        private System.Windows.Forms.ToolStripButton bBack;
        private System.Windows.Forms.ToolStripButton bNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

