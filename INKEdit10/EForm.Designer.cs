namespace INKEdit10 {
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
            this.inkEdit1 = new Microsoft.Ink.InkEdit();
            this.SuspendLayout();
            // 
            // inkEdit1
            // 
            this.inkEdit1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inkEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inkEdit1.Location = new System.Drawing.Point(0, 0);
            this.inkEdit1.Name = "inkEdit1";
            this.inkEdit1.Size = new System.Drawing.Size(902, 517);
            this.inkEdit1.TabIndex = 0;
            this.inkEdit1.Text = "";
            // 
            // EForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 517);
            this.Controls.Add(this.inkEdit1);
            this.Name = "EForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INKEdit";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Ink.InkEdit inkEdit1;
    }
}

