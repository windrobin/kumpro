namespace StatFilesbyExt {
    partial class NForm {
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
            this.buttonRef = new System.Windows.Forms.Button();
            this.buttonStat = new System.Windows.Forms.Button();
            this.cbIn = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "集計したいフォルダを選択してください。";
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(220, 50);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(91, 34);
            this.buttonRef.TabIndex = 2;
            this.buttonRef.Text = "参照します...";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // buttonStat
            // 
            this.buttonStat.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonStat.Location = new System.Drawing.Point(317, 50);
            this.buttonStat.Name = "buttonStat";
            this.buttonStat.Size = new System.Drawing.Size(91, 34);
            this.buttonStat.TabIndex = 3;
            this.buttonStat.Text = "集計します";
            this.buttonStat.UseVisualStyleBackColor = true;
            this.buttonStat.Click += new System.EventHandler(this.buttonStat_Click);
            // 
            // cbIn
            // 
            this.cbIn.FormattingEnabled = true;
            this.cbIn.Location = new System.Drawing.Point(12, 24);
            this.cbIn.Name = "cbIn";
            this.cbIn.Size = new System.Drawing.Size(396, 20);
            this.cbIn.TabIndex = 1;
            // 
            // NForm
            // 
            this.AcceptButton = this.buttonStat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 100);
            this.Controls.Add(this.cbIn);
            this.Controls.Add(this.buttonStat);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.label1);
            this.Name = "NForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拡張子ごとにファイル数・容量を集計します";
            this.Load += new System.EventHandler(this.NForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Button buttonStat;
        private System.Windows.Forms.ComboBox cbIn;
    }
}

