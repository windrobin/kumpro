namespace PeekPOP3 {
    partial class ConnForm {
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
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.tbPOP3 = new System.Windows.Forms.TextBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.tbU = new System.Windows.Forms.TextBox();
            this.tbP = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.flp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // flp
            // 
            this.flp.Controls.Add(this.label1);
            this.flp.Controls.Add(this.tbPOP3);
            this.flp.Controls.Add(this.label2);
            this.flp.Controls.Add(this.numPort);
            this.flp.Controls.Add(this.label3);
            this.flp.Controls.Add(this.tbU);
            this.flp.Controls.Add(this.label4);
            this.flp.Controls.Add(this.tbP);
            this.flp.Controls.Add(this.bSave);
            this.flp.Controls.Add(this.bOk);
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp.Location = new System.Drawing.Point(0, 0);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(187, 255);
            this.flp.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "POP3サーバ＝";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "POP3ポート＝";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "アカウント名＝";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "パス＝";
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(3, 207);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(155, 23);
            this.bOk.TabIndex = 10;
            this.bOk.Text = "いますぐ、接続";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // tbPOP3
            // 
            this.tbPOP3.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PeekPOP3.Properties.Settings.Default, "POP3", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbPOP3.Location = new System.Drawing.Point(3, 15);
            this.tbPOP3.Name = "tbPOP3";
            this.tbPOP3.Size = new System.Drawing.Size(155, 19);
            this.tbPOP3.TabIndex = 1;
            this.tbPOP3.Text = global::PeekPOP3.Properties.Settings.Default.POP3;
            // 
            // numPort
            // 
            this.numPort.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::PeekPOP3.Properties.Settings.Default, "POP3Port", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numPort.Location = new System.Drawing.Point(3, 52);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(155, 19);
            this.numPort.TabIndex = 3;
            this.numPort.Value = global::PeekPOP3.Properties.Settings.Default.POP3Port;
            // 
            // tbU
            // 
            this.tbU.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PeekPOP3.Properties.Settings.Default, "User", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbU.Location = new System.Drawing.Point(3, 89);
            this.tbU.Name = "tbU";
            this.tbU.Size = new System.Drawing.Size(155, 19);
            this.tbU.TabIndex = 5;
            this.tbU.Text = global::PeekPOP3.Properties.Settings.Default.User;
            // 
            // tbP
            // 
            this.tbP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PeekPOP3.Properties.Settings.Default, "Pass", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbP.Location = new System.Drawing.Point(3, 126);
            this.tbP.Name = "tbP";
            this.tbP.PasswordChar = '*';
            this.tbP.Size = new System.Drawing.Size(155, 19);
            this.tbP.TabIndex = 7;
            this.tbP.Text = global::PeekPOP3.Properties.Settings.Default.Pass;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(3, 178);
            this.bSave.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(155, 23);
            this.bSave.TabIndex = 9;
            this.bSave.Text = "保存";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // ConnForm
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 255);
            this.Controls.Add(this.flp);
            this.Name = "ConnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConnForm";
            this.flp.ResumeLayout(false);
            this.flp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPOP3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbU;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbP;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bSave;
    }
}