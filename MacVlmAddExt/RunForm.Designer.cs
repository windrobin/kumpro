namespace MacVlmAddExt {
    partial class RunForm {
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
            this.lExecing = new System.Windows.Forms.Label();
            this.lRepo = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.bwAddExt = new System.ComponentModel.BackgroundWorker();
            this.lDone = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lExecing
            // 
            this.lExecing.AutoSize = true;
            this.lExecing.Location = new System.Drawing.Point(4, 0);
            this.lExecing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lExecing.Name = "lExecing";
            this.lExecing.Size = new System.Drawing.Size(212, 16);
            this.lExecing.TabIndex = 0;
            this.lExecing.Text = "実行しています。お待ちください。";
            // 
            // lRepo
            // 
            this.lRepo.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.lRepo, true);
            this.lRepo.Location = new System.Drawing.Point(223, 0);
            this.lRepo.Name = "lRepo";
            this.lRepo.Size = new System.Drawing.Size(17, 16);
            this.lRepo.TabIndex = 1;
            this.lRepo.Text = "...";
            // 
            // bClose
            // 
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bClose.Enabled = false;
            this.bClose.Location = new System.Drawing.Point(12, 77);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "閉じる";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bwAddExt
            // 
            this.bwAddExt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwAddExt_DoWork);
            this.bwAddExt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwAddExt_RunWorkerCompleted);
            // 
            // lDone
            // 
            this.lDone.AutoSize = true;
            this.lDone.Location = new System.Drawing.Point(3, 16);
            this.lDone.Name = "lDone";
            this.lDone.Size = new System.Drawing.Size(96, 16);
            this.lDone.TabIndex = 3;
            this.lDone.Text = "終わりました。";
            this.lDone.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lExecing);
            this.flowLayoutPanel1.Controls.Add(this.lRepo);
            this.flowLayoutPanel1.Controls.Add(this.lDone);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(735, 59);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 112);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.bClose);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "RunForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunForm";
            this.Load += new System.EventHandler(this.RunForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lExecing;
        private System.Windows.Forms.Label lRepo;
        private System.Windows.Forms.Button bClose;
        private System.ComponentModel.BackgroundWorker bwAddExt;
        private System.Windows.Forms.Label lDone;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}