namespace Test {
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
            this.tbIn = new System.Windows.Forms.TextBox();
            this.bImm = new System.Windows.Forms.Button();
            this.bTsf = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "入力：";
            // 
            // tbIn
            // 
            this.tbIn.Location = new System.Drawing.Point(12, 24);
            this.tbIn.Multiline = true;
            this.tbIn.Name = "tbIn";
            this.tbIn.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbIn.Size = new System.Drawing.Size(528, 129);
            this.tbIn.TabIndex = 1;
            this.tbIn.Text = "枚岡合金工具株式会社";
            // 
            // bImm
            // 
            this.bImm.AutoSize = true;
            this.bImm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bImm.Location = new System.Drawing.Point(12, 159);
            this.bImm.Name = "bImm";
            this.bImm.Size = new System.Drawing.Size(106, 22);
            this.bImm.TabIndex = 2;
            this.bImm.Text = "ImmYomi.GetYomi";
            this.bImm.UseVisualStyleBackColor = true;
            this.bImm.Click += new System.EventHandler(this.bImm_Click);
            // 
            // bTsf
            // 
            this.bTsf.AutoSize = true;
            this.bTsf.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bTsf.Location = new System.Drawing.Point(147, 159);
            this.bTsf.Name = "bTsf";
            this.bTsf.Size = new System.Drawing.Size(106, 22);
            this.bTsf.TabIndex = 2;
            this.bTsf.Text = "TSFYomi.GetYomi";
            this.bTsf.UseVisualStyleBackColor = true;
            this.bTsf.Click += new System.EventHandler(this.bTsf_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "結果：";
            // 
            // tbRes
            // 
            this.tbRes.Location = new System.Drawing.Point(12, 211);
            this.tbRes.Multiline = true;
            this.tbRes.Name = "tbRes";
            this.tbRes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRes.Size = new System.Drawing.Size(528, 151);
            this.tbRes.TabIndex = 4;
            // 
            // EForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 379);
            this.Controls.Add(this.tbRes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bTsf);
            this.Controls.Add(this.bImm);
            this.Controls.Add(this.tbIn);
            this.Controls.Add(this.label1);
            this.Name = "EForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Test LibGetYomi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIn;
        private System.Windows.Forms.Button bImm;
        private System.Windows.Forms.Button bTsf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRes;
    }
}

