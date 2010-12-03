namespace Tablega {
    partial class SelTblForm {
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
            this.gv = new System.Windows.Forms.DataGridView();
            this.ts = new System.Windows.Forms.ToolStrip();
            this.tslHere = new System.Windows.Forms.ToolStripLabel();
            this.ts2 = new System.Windows.Forms.ToolStrip();
            this.bConv2Date = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bCopy = new System.Windows.Forms.ToolStripButton();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.ts.SuspendLayout();
            this.ts2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsc
            // 
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.gv);
            this.tsc.ContentPanel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tsc.ContentPanel.Size = new System.Drawing.Size(762, 414);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(762, 464);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.ts);
            this.tsc.TopToolStripPanel.Controls.Add(this.ts2);
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv.Location = new System.Drawing.Point(0, 0);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 21;
            this.gv.Size = new System.Drawing.Size(762, 414);
            this.gv.TabIndex = 0;
            // 
            // ts
            // 
            this.ts.Dock = System.Windows.Forms.DockStyle.None;
            this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslHere});
            this.ts.Location = new System.Drawing.Point(0, 0);
            this.ts.Name = "ts";
            this.ts.Size = new System.Drawing.Size(762, 25);
            this.ts.Stretch = true;
            this.ts.TabIndex = 0;
            // 
            // tslHere
            // 
            this.tslHere.Name = "tslHere";
            this.tslHere.Size = new System.Drawing.Size(86, 22);
            this.tslHere.Text = "選択してください：";
            // 
            // ts2
            // 
            this.ts2.Dock = System.Windows.Forms.DockStyle.None;
            this.ts2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bConv2Date,
            this.toolStripSeparator1,
            this.bCopy});
            this.ts2.Location = new System.Drawing.Point(0, 25);
            this.ts2.Name = "ts2";
            this.ts2.Size = new System.Drawing.Size(762, 25);
            this.ts2.Stretch = true;
            this.ts2.TabIndex = 1;
            // 
            // bConv2Date
            // 
            this.bConv2Date.Image = global::Tablega.Properties.Resources.Calendar_scheduleHS;
            this.bConv2Date.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bConv2Date.Name = "bConv2Date";
            this.bConv2Date.Size = new System.Drawing.Size(157, 22);
            this.bConv2Date.Text = "日付っぽいものを日付にする";
            this.bConv2Date.Click += new System.EventHandler(this.bConv2Date_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bCopy
            // 
            this.bCopy.Image = global::Tablega.Properties.Resources.CopyHS;
            this.bCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(161, 22);
            this.bCopy.Text = "表をクリップボードにコピーする";
            this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
            // 
            // SelTblForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 464);
            this.Controls.Add(this.tsc);
            this.Name = "SelTblForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Table一覧の表示から";
            this.Load += new System.EventHandler(this.SelTblForm_Load);
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ts.ResumeLayout(false);
            this.ts.PerformLayout();
            this.ts2.ResumeLayout(false);
            this.ts2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.ToolStrip ts;
        private System.Windows.Forms.ToolStripLabel tslHere;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.ToolStrip ts2;
        private System.Windows.Forms.ToolStripButton bCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bConv2Date;
    }
}