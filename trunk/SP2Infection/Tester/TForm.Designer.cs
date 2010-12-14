namespace Tester {
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbIn = new System.Windows.Forms.TextBox();
            this.bSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInfected = new System.Windows.Forms.TextBox();
            this.bwSearcher = new System.ComponentModel.BackgroundWorker();
            this.lNow = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "パス：";
            // 
            // tbIn
            // 
            this.tbIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIn.Location = new System.Drawing.Point(12, 24);
            this.tbIn.Multiline = true;
            this.tbIn.Name = "tbIn";
            this.tbIn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbIn.Size = new System.Drawing.Size(245, 73);
            this.tbIn.TabIndex = 1;
            // 
            // bSearch
            // 
            this.bSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSearch.Location = new System.Drawing.Point(263, 24);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(95, 36);
            this.bSearch.TabIndex = 2;
            this.bSearch.Text = "検査";
            this.bSearch.UseVisualStyleBackColor = true;
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "SP2汚染：";
            // 
            // tbInfected
            // 
            this.tbInfected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInfected.Location = new System.Drawing.Point(12, 115);
            this.tbInfected.Multiline = true;
            this.tbInfected.Name = "tbInfected";
            this.tbInfected.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInfected.Size = new System.Drawing.Size(346, 80);
            this.tbInfected.TabIndex = 4;
            // 
            // bwSearcher
            // 
            this.bwSearcher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSearcher_DoWork);
            this.bwSearcher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSearcher_RunWorkerCompleted);
            // 
            // lNow
            // 
            this.lNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lNow.Location = new System.Drawing.Point(263, 63);
            this.lNow.Name = "lNow";
            this.lNow.Size = new System.Drawing.Size(95, 49);
            this.lNow.TabIndex = 5;
            this.lNow.Text = "〔検査中ファイル〕";
            // 
            // TForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 207);
            this.Controls.Add(this.lNow);
            this.Controls.Add(this.tbInfected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bSearch);
            this.Controls.Add(this.tbIn);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(195, 206);
            this.Name = "TForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIn;
        private System.Windows.Forms.Button bSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInfected;
        private System.ComponentModel.BackgroundWorker bwSearcher;
        private System.Windows.Forms.Label lNow;
    }
}

