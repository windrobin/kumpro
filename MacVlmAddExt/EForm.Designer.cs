namespace MacVlmAddExt {
    partial class EForm {
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
            this.tbDir = new System.Windows.Forms.TextBox();
            this.bRef = new System.Windows.Forms.Button();
            this.bRun = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "フォルダ：";
            // 
            // tbDir
            // 
            this.tbDir.Location = new System.Drawing.Point(13, 29);
            this.tbDir.Margin = new System.Windows.Forms.Padding(4);
            this.tbDir.Name = "tbDir";
            this.tbDir.Size = new System.Drawing.Size(434, 23);
            this.tbDir.TabIndex = 1;
            // 
            // bRef
            // 
            this.bRef.Location = new System.Drawing.Point(372, 59);
            this.bRef.Name = "bRef";
            this.bRef.Size = new System.Drawing.Size(75, 23);
            this.bRef.TabIndex = 2;
            this.bRef.Text = "参照";
            this.bRef.UseVisualStyleBackColor = true;
            this.bRef.Click += new System.EventHandler(this.bRef_Click);
            // 
            // bRun
            // 
            this.bRun.Location = new System.Drawing.Point(372, 88);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(75, 23);
            this.bRun.TabIndex = 3;
            this.bRun.Text = "実行";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // fbd
            // 
            this.fbd.Description = "フォルダを選んでね♪";
            // 
            // EForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 138);
            this.Controls.Add(this.bRun);
            this.Controls.Add(this.bRef);
            this.Controls.Add(this.tbDir);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "EForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mac vlm Add Ext";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDir;
        private System.Windows.Forms.Button bRef;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}

