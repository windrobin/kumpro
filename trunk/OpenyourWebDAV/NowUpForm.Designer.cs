namespace OpenyourWebDAV {
    partial class NowUpForm {
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
            this.pbAni = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lfp = new System.Windows.Forms.Label();
            this.pbIO = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbAni)).BeginInit();
            this.SuspendLayout();
            // 
            // pbAni
            // 
            this.pbAni.Image = global::OpenyourWebDAV.Properties.Resources.UPload_00;
            this.pbAni.Location = new System.Drawing.Point(12, 12);
            this.pbAni.Name = "pbAni";
            this.pbAni.Size = new System.Drawing.Size(657, 60);
            this.pbAni.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbAni.TabIndex = 0;
            this.pbAni.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Webサーバにファイルを送信しています。お待ちください。";
            // 
            // lfp
            // 
            this.lfp.AutoSize = true;
            this.lfp.Location = new System.Drawing.Point(12, 101);
            this.lfp.Name = "lfp";
            this.lfp.Size = new System.Drawing.Size(11, 12);
            this.lfp.TabIndex = 2;
            this.lfp.Text = "...";
            // 
            // pbIO
            // 
            this.pbIO.Location = new System.Drawing.Point(12, 116);
            this.pbIO.Name = "pbIO";
            this.pbIO.Size = new System.Drawing.Size(657, 14);
            this.pbIO.TabIndex = 3;
            // 
            // NowUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 145);
            this.Controls.Add(this.pbIO);
            this.Controls.Add(this.lfp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbAni);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NowUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "送信中";
            this.Load += new System.EventHandler(this.NowUpForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NowUpForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbAni)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbAni;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lfp;
        private System.Windows.Forms.ProgressBar pbIO;
    }
}