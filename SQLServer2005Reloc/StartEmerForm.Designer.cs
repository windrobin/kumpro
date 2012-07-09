namespace SQLServer2005Reloc {
    partial class StartEmerForm {
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbRunEmer2 = new System.Windows.Forms.TextBox();
            this.tbRunEmer = new System.Windows.Forms.TextBox();
            this.bExecEmer = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbRunEmer2);
            this.groupBox2.Controls.Add(this.tbRunEmer);
            this.groupBox2.Controls.Add(this.bExecEmer);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 102);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "復旧するモードで開始します。";
            // 
            // tbRunEmer2
            // 
            this.tbRunEmer2.Location = new System.Drawing.Point(6, 43);
            this.tbRunEmer2.Name = "tbRunEmer2";
            this.tbRunEmer2.Size = new System.Drawing.Size(333, 19);
            this.tbRunEmer2.TabIndex = 3;
            this.tbRunEmer2.Text = "START MSSQL$KODB2007_05 /f /T3608";
            // 
            // tbRunEmer
            // 
            this.tbRunEmer.Location = new System.Drawing.Point(6, 18);
            this.tbRunEmer.Name = "tbRunEmer";
            this.tbRunEmer.Size = new System.Drawing.Size(333, 19);
            this.tbRunEmer.TabIndex = 1;
            this.tbRunEmer.Text = "NET.exe";
            // 
            // bExecEmer
            // 
            this.bExecEmer.Location = new System.Drawing.Point(6, 68);
            this.bExecEmer.Name = "bExecEmer";
            this.bExecEmer.Size = new System.Drawing.Size(75, 23);
            this.bExecEmer.TabIndex = 2;
            this.bExecEmer.Text = "実行";
            this.bExecEmer.UseVisualStyleBackColor = true;
            // 
            // StartEmerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 151);
            this.Controls.Add(this.groupBox2);
            this.Name = "StartEmerForm";
            this.Text = "StartEmerForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbRunEmer2;
        private System.Windows.Forms.TextBox tbRunEmer;
        private System.Windows.Forms.Button bExecEmer;
    }
}