namespace AxXlsVw {
    partial class VwXls {
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.tss = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.gv = new System.Windows.Forms.DataGridView();
            this.bwLoader = new System.ComponentModel.BackgroundWorker();
            this.tsc.BottomToolStripPanel.SuspendLayout();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.tss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.SuspendLayout();
            // 
            // tsc
            // 
            // 
            // tsc.BottomToolStripPanel
            // 
            this.tsc.BottomToolStripPanel.Controls.Add(this.tss);
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.gv);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(300, 100);
            this.tsc.ContentPanel.Load += new System.EventHandler(this.toolStripContainer1_ContentPanel_Load);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(300, 150);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tss
            // 
            this.tss.Dock = System.Windows.Forms.DockStyle.None;
            this.tss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.tss.Location = new System.Drawing.Point(0, 0);
            this.tss.Name = "tss";
            this.tss.Size = new System.Drawing.Size(300, 25);
            this.tss.Stretch = true;
            this.tss.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel1.Text = "Sheets:";
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.AllowUserToDeleteRows = false;
            this.gv.AllowUserToOrderColumns = true;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv.Location = new System.Drawing.Point(0, 0);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 21;
            this.gv.Size = new System.Drawing.Size(300, 100);
            this.gv.TabIndex = 0;
            // 
            // bwLoader
            // 
            this.bwLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoader_DoWork);
            this.bwLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwLoader_RunWorkerCompleted);
            // 
            // VwXls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsc);
            this.Name = "VwXls";
            this.Size = new System.Drawing.Size(300, 150);
            this.Load += new System.EventHandler(this.VwXls_Load);
            this.tsc.BottomToolStripPanel.ResumeLayout(false);
            this.tsc.BottomToolStripPanel.PerformLayout();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.tss.ResumeLayout(false);
            this.tss.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.ToolStrip tss;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridView gv;
        private System.ComponentModel.BackgroundWorker bwLoader;

    }
}
