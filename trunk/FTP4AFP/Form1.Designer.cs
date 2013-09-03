namespace FTP4AFP {
    partial class Form1 {
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
            this.tbAFP = new System.Windows.Forms.TextBox();
            this.bStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLOG = new System.Windows.Forms.RichTextBox();
            this.tbAFPPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFTPPort = new System.Windows.Forms.TextBox();
            this.bStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbAppend = new System.Windows.Forms.RadioButton();
            this.rbPrepend = new System.Windows.Forms.RadioButton();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connect to AFP server";
            // 
            // tbAFP
            // 
            this.tbAFP.Location = new System.Drawing.Point(12, 24);
            this.tbAFP.Name = "tbAFP";
            this.tbAFP.Size = new System.Drawing.Size(100, 19);
            this.tbAFP.TabIndex = 1;
            this.tbAFP.Text = "192.168.2.101";
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(12, 137);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 2;
            this.bStart.Text = "&Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ログ";
            // 
            // tbLOG
            // 
            this.tbLOG.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLOG.Location = new System.Drawing.Point(12, 187);
            this.tbLOG.Name = "tbLOG";
            this.tbLOG.Size = new System.Drawing.Size(353, 87);
            this.tbLOG.TabIndex = 4;
            this.tbLOG.Text = "";
            // 
            // tbAFPPort
            // 
            this.tbAFPPort.Location = new System.Drawing.Point(118, 24);
            this.tbAFPPort.Name = "tbAFPPort";
            this.tbAFPPort.Size = new System.Drawing.Size(44, 19);
            this.tbAFPPort.TabIndex = 5;
            this.tbAFPPort.Text = "548";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "FTP server";
            // 
            // tbFTPPort
            // 
            this.tbFTPPort.Location = new System.Drawing.Point(118, 99);
            this.tbFTPPort.Name = "tbFTPPort";
            this.tbFTPPort.Size = new System.Drawing.Size(44, 19);
            this.tbFTPPort.TabIndex = 7;
            this.tbFTPPort.Text = "2121";
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Location = new System.Drawing.Point(93, 137);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 23);
            this.bStop.TabIndex = 8;
            this.bStop.Text = "&Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Resource fork as";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.rbNo);
            this.flowLayoutPanel1.Controls.Add(this.rbAppend);
            this.flowLayoutPanel1.Controls.Add(this.rbPrepend);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(170, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(215, 22);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // rbAppend
            // 
            this.rbAppend.AutoSize = true;
            this.rbAppend.Location = new System.Drawing.Point(46, 3);
            this.rbAppend.Name = "rbAppend";
            this.rbAppend.Size = new System.Drawing.Size(115, 16);
            this.rbAppend.TabIndex = 0;
            this.rbAppend.TabStop = true;
            this.rbAppend.Text = "file.AFP_Resource";
            this.rbAppend.UseVisualStyleBackColor = true;
            this.rbAppend.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // rbPrepend
            // 
            this.rbPrepend.AutoSize = true;
            this.rbPrepend.Checked = true;
            this.rbPrepend.Location = new System.Drawing.Point(167, 3);
            this.rbPrepend.Name = "rbPrepend";
            this.rbPrepend.Size = new System.Drawing.Size(45, 16);
            this.rbPrepend.TabIndex = 1;
            this.rbPrepend.TabStop = true;
            this.rbPrepend.Text = "._file";
            this.rbPrepend.UseVisualStyleBackColor = true;
            this.rbPrepend.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(3, 3);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(37, 16);
            this.rbNo.TabIndex = 2;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 294);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.tbFTPPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbAFPPort);
            this.Controls.Add(this.tbLOG);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.tbAFP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP for AFP";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAFP;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox tbLOG;
        private System.Windows.Forms.TextBox tbAFPPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFTPPort;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbAppend;
        private System.Windows.Forms.RadioButton rbPrepend;
        private System.Windows.Forms.RadioButton rbNo;
    }
}

