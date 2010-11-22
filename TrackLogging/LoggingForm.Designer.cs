namespace TrackLogging {
    partial class LoggingForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggingForm));
            this.ts1 = new System.Windows.Forms.ToolStrip();
            this.bClr = new System.Windows.Forms.ToolStripButton();
            this.bErase = new System.Windows.Forms.ToolStripButton();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bCSC = new System.Windows.Forms.ToolStripButton();
            this.ts1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts1
            // 
            this.ts1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bClr,
            this.bErase,
            this.toolStripSeparator1,
            this.bCSC});
            this.ts1.Location = new System.Drawing.Point(0, 0);
            this.ts1.Name = "ts1";
            this.ts1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ts1.Size = new System.Drawing.Size(664, 25);
            this.ts1.TabIndex = 1;
            this.ts1.Text = "toolStrip1";
            // 
            // bClr
            // 
            this.bClr.Image = ((System.Drawing.Image)(resources.GetObject("bClr.Image")));
            this.bClr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bClr.Name = "bClr";
            this.bClr.Size = new System.Drawing.Size(110, 22);
            this.bClr.Text = "選択解除(Alt+&C)";
            this.bClr.ToolTipText = "ログ追記を検出した際の、検出マークを最後に移動します";
            this.bClr.Click += new System.EventHandler(this.bClr_Click);
            // 
            // bErase
            // 
            this.bErase.Image = ((System.Drawing.Image)(resources.GetObject("bErase.Image")));
            this.bErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bErase.Name = "bErase";
            this.bErase.Size = new System.Drawing.Size(85, 22);
            this.bErase.Text = "除去(Alt+&E)";
            this.bErase.ToolTipText = "表示している内容を削除し、検出マークを最後に移動します。";
            this.bErase.Click += new System.EventHandler(this.bErase_Click);
            // 
            // tb1
            // 
            this.tb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tb1.Location = new System.Drawing.Point(0, 25);
            this.tb1.Multiline = true;
            this.tb1.Name = "tb1";
            this.tb1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb1.Size = new System.Drawing.Size(664, 211);
            this.tb1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bCSC
            // 
            this.bCSC.Image = ((System.Drawing.Image)(resources.GetObject("bCSC.Image")));
            this.bCSC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bCSC.Name = "bCSC";
            this.bCSC.Size = new System.Drawing.Size(102, 22);
            this.bCSC.Text = "CreateShortcut";
            this.bCSC.ToolTipText = "デスクトップに起動用のショートカットを作成します";
            this.bCSC.Click += new System.EventHandler(this.bCSC_Click);
            // 
            // LoggingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 236);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.ts1);
            this.Name = "LoggingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrackLogging";
            this.Load += new System.EventHandler(this.LoggingForm_Load);
            this.ts1.ResumeLayout(false);
            this.ts1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ts1;
        private System.Windows.Forms.ToolStripButton bClr;
        private System.Windows.Forms.ToolStripButton bErase;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bCSC;
    }
}

