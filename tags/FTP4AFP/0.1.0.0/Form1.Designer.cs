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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAFPHost = new System.Windows.Forms.TextBox();
            this.bStart = new System.Windows.Forms.Button();
            this.tbAFPPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFTPPort = new System.Windows.Forms.TextBox();
            this.bStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.rbAppend = new System.Windows.Forms.RadioButton();
            this.rbPrepend = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbAFPHost2 = new System.Windows.Forms.TextBox();
            this.tbFTPPort2 = new System.Windows.Forms.TextBox();
            this.tbAFPPort2 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbNo2 = new System.Windows.Forms.RadioButton();
            this.rbAppend2 = new System.Windows.Forms.RadioButton();
            this.rbPrepend2 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFTPPort3 = new System.Windows.Forms.TextBox();
            this.tbAFPHost3 = new System.Windows.Forms.TextBox();
            this.tbAFPPort3 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbNo3 = new System.Windows.Forms.RadioButton();
            this.rbAppend3 = new System.Windows.Forms.RadioButton();
            this.rbPrepend3 = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.bStart2 = new System.Windows.Forms.Button();
            this.bStop2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.bStart3 = new System.Windows.Forms.Button();
            this.bStop3 = new System.Windows.Forms.Button();
            this.tbCmdl = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbA1 = new System.Windows.Forms.CheckBox();
            this.cbA2 = new System.Windows.Forms.CheckBox();
            this.cbA3 = new System.Windows.Forms.CheckBox();
            this.s1 = new FTP4AFP.AFPServ(this.components);
            this.s2 = new FTP4AFP.AFPServ(this.components);
            this.s3 = new FTP4AFP.AFPServ(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Location = new System.Drawing.Point(117, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connect to AFP server";
            // 
            // tbAFPHost
            // 
            this.tbAFPHost.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAFPHost.Location = new System.Drawing.Point(117, 55);
            this.tbAFPHost.Name = "tbAFPHost";
            this.tbAFPHost.Size = new System.Drawing.Size(100, 22);
            this.tbAFPHost.TabIndex = 6;
            this.tbAFPHost.Text = "192.168.2.101";
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(3, 3);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "&Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // tbAFPPort
            // 
            this.tbAFPPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAFPPort.Location = new System.Drawing.Point(225, 55);
            this.tbAFPPort.Name = "tbAFPPort";
            this.tbAFPPort.Size = new System.Drawing.Size(44, 22);
            this.tbAFPPort.TabIndex = 7;
            this.tbAFPPort.Text = "548";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "FTP server";
            // 
            // tbFTPPort
            // 
            this.tbFTPPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbFTPPort.Location = new System.Drawing.Point(43, 55);
            this.tbFTPPort.Name = "tbFTPPort";
            this.tbFTPPort.Size = new System.Drawing.Size(44, 22);
            this.tbFTPPort.TabIndex = 5;
            this.tbFTPPort.Text = "2121";
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Location = new System.Drawing.Point(84, 3);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 23);
            this.bStop.TabIndex = 1;
            this.bStop.Text = "&Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Resource-fork/Finder-info as";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.rbNo);
            this.flowLayoutPanel1.Controls.Add(this.rbAppend);
            this.flowLayoutPanel1.Controls.Add(this.rbPrepend);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(277, 44);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(244, 44);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // rbNo
            // 
            this.rbNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(3, 11);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(43, 21);
            this.rbNo.TabIndex = 0;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // rbAppend
            // 
            this.rbAppend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbAppend.AutoSize = true;
            this.rbAppend.Location = new System.Drawing.Point(52, 3);
            this.rbAppend.Name = "rbAppend";
            this.rbAppend.Size = new System.Drawing.Size(130, 38);
            this.rbAppend.TabIndex = 1;
            this.rbAppend.TabStop = true;
            this.rbAppend.Text = "file.AFP_Resource\r\nfile.AFP_AfpInfo\r\n";
            this.rbAppend.UseVisualStyleBackColor = true;
            this.rbAppend.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // rbPrepend
            // 
            this.rbPrepend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbPrepend.AutoSize = true;
            this.rbPrepend.Checked = true;
            this.rbPrepend.Location = new System.Drawing.Point(188, 11);
            this.rbPrepend.Name = "rbPrepend";
            this.rbPrepend.Size = new System.Drawing.Size(53, 21);
            this.rbPrepend.TabIndex = 2;
            this.rbPrepend.TabStop = true;
            this.rbPrepend.Text = "._file";
            this.rbPrepend.UseVisualStyleBackColor = true;
            this.rbPrepend.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cbA3, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbA2, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbFTPPort, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbAFPHost, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbAFPPort, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbAFPHost2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFTPPort2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbAFPPort2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbFTPPort3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbAFPHost3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbAFPPort3, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel6, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbA1, 5, 1);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(790, 217);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "№";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "1st";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "2nd";
            // 
            // tbAFPHost2
            // 
            this.tbAFPHost2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAFPHost2.Location = new System.Drawing.Point(117, 114);
            this.tbAFPHost2.Name = "tbAFPHost2";
            this.tbAFPHost2.Size = new System.Drawing.Size(100, 22);
            this.tbAFPHost2.TabIndex = 12;
            this.tbAFPHost2.Text = "127.0.0.1";
            // 
            // tbFTPPort2
            // 
            this.tbFTPPort2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbFTPPort2.Location = new System.Drawing.Point(43, 114);
            this.tbFTPPort2.Name = "tbFTPPort2";
            this.tbFTPPort2.Size = new System.Drawing.Size(44, 22);
            this.tbFTPPort2.TabIndex = 11;
            this.tbFTPPort2.Text = "2122";
            // 
            // tbAFPPort2
            // 
            this.tbAFPPort2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAFPPort2.Location = new System.Drawing.Point(225, 114);
            this.tbAFPPort2.Name = "tbAFPPort2";
            this.tbAFPPort2.Size = new System.Drawing.Size(44, 22);
            this.tbAFPPort2.TabIndex = 13;
            this.tbAFPPort2.Text = "548";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.rbNo2);
            this.flowLayoutPanel2.Controls.Add(this.rbAppend2);
            this.flowLayoutPanel2.Controls.Add(this.rbPrepend2);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(277, 103);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(244, 44);
            this.flowLayoutPanel2.TabIndex = 14;
            // 
            // rbNo2
            // 
            this.rbNo2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbNo2.AutoSize = true;
            this.rbNo2.Location = new System.Drawing.Point(3, 11);
            this.rbNo2.Name = "rbNo2";
            this.rbNo2.Size = new System.Drawing.Size(43, 21);
            this.rbNo2.TabIndex = 0;
            this.rbNo2.TabStop = true;
            this.rbNo2.Text = "No";
            this.rbNo2.UseVisualStyleBackColor = true;
            this.rbNo2.CheckedChanged += new System.EventHandler(this.rbNo2_CheckedChanged);
            // 
            // rbAppend2
            // 
            this.rbAppend2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbAppend2.AutoSize = true;
            this.rbAppend2.Location = new System.Drawing.Point(52, 3);
            this.rbAppend2.Name = "rbAppend2";
            this.rbAppend2.Size = new System.Drawing.Size(130, 38);
            this.rbAppend2.TabIndex = 1;
            this.rbAppend2.TabStop = true;
            this.rbAppend2.Text = "file.AFP_Resource\r\nfile.AFP_AfpInfo\r\n";
            this.rbAppend2.UseVisualStyleBackColor = true;
            this.rbAppend2.CheckedChanged += new System.EventHandler(this.rbNo2_CheckedChanged);
            // 
            // rbPrepend2
            // 
            this.rbPrepend2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbPrepend2.AutoSize = true;
            this.rbPrepend2.Checked = true;
            this.rbPrepend2.Location = new System.Drawing.Point(188, 11);
            this.rbPrepend2.Name = "rbPrepend2";
            this.rbPrepend2.Size = new System.Drawing.Size(53, 21);
            this.rbPrepend2.TabIndex = 2;
            this.rbPrepend2.TabStop = true;
            this.rbPrepend2.Text = "._file";
            this.rbPrepend2.UseVisualStyleBackColor = true;
            this.rbPrepend2.CheckedChanged += new System.EventHandler(this.rbNo2_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "3rd";
            // 
            // tbFTPPort3
            // 
            this.tbFTPPort3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbFTPPort3.Location = new System.Drawing.Point(43, 174);
            this.tbFTPPort3.Name = "tbFTPPort3";
            this.tbFTPPort3.Size = new System.Drawing.Size(44, 22);
            this.tbFTPPort3.TabIndex = 17;
            // 
            // tbAFPHost3
            // 
            this.tbAFPHost3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAFPHost3.Location = new System.Drawing.Point(117, 174);
            this.tbAFPHost3.Name = "tbAFPHost3";
            this.tbAFPHost3.Size = new System.Drawing.Size(100, 22);
            this.tbAFPHost3.TabIndex = 18;
            // 
            // tbAFPPort3
            // 
            this.tbAFPPort3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAFPPort3.Location = new System.Drawing.Point(225, 174);
            this.tbAFPPort3.Name = "tbAFPPort3";
            this.tbAFPPort3.Size = new System.Drawing.Size(44, 22);
            this.tbAFPPort3.TabIndex = 19;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.rbNo3);
            this.flowLayoutPanel3.Controls.Add(this.rbAppend3);
            this.flowLayoutPanel3.Controls.Add(this.rbPrepend3);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(277, 163);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(244, 44);
            this.flowLayoutPanel3.TabIndex = 20;
            // 
            // rbNo3
            // 
            this.rbNo3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbNo3.AutoSize = true;
            this.rbNo3.Location = new System.Drawing.Point(3, 11);
            this.rbNo3.Name = "rbNo3";
            this.rbNo3.Size = new System.Drawing.Size(43, 21);
            this.rbNo3.TabIndex = 0;
            this.rbNo3.TabStop = true;
            this.rbNo3.Text = "No";
            this.rbNo3.UseVisualStyleBackColor = true;
            this.rbNo3.CheckedChanged += new System.EventHandler(this.rbNo3_CheckedChanged);
            // 
            // rbAppend3
            // 
            this.rbAppend3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbAppend3.AutoSize = true;
            this.rbAppend3.Location = new System.Drawing.Point(52, 3);
            this.rbAppend3.Name = "rbAppend3";
            this.rbAppend3.Size = new System.Drawing.Size(130, 38);
            this.rbAppend3.TabIndex = 1;
            this.rbAppend3.TabStop = true;
            this.rbAppend3.Text = "file.AFP_Resource\r\nfile.AFP_AfpInfo\r\n";
            this.rbAppend3.UseVisualStyleBackColor = true;
            this.rbAppend3.CheckedChanged += new System.EventHandler(this.rbNo3_CheckedChanged);
            // 
            // rbPrepend3
            // 
            this.rbPrepend3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbPrepend3.AutoSize = true;
            this.rbPrepend3.Checked = true;
            this.rbPrepend3.Location = new System.Drawing.Point(188, 11);
            this.rbPrepend3.Name = "rbPrepend3";
            this.rbPrepend3.Size = new System.Drawing.Size(53, 21);
            this.rbPrepend3.TabIndex = 2;
            this.rbPrepend3.TabStop = true;
            this.rbPrepend3.Text = "._file";
            this.rbPrepend3.UseVisualStyleBackColor = true;
            this.rbPrepend3.CheckedChanged += new System.EventHandler(this.rbNo3_CheckedChanged);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.bStart);
            this.flowLayoutPanel4.Controls.Add(this.bStop);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(574, 52);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel4.TabIndex = 9;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.bStart2);
            this.flowLayoutPanel5.Controls.Add(this.bStop2);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(574, 111);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel5.TabIndex = 15;
            // 
            // bStart2
            // 
            this.bStart2.Location = new System.Drawing.Point(3, 3);
            this.bStart2.Name = "bStart2";
            this.bStart2.Size = new System.Drawing.Size(75, 23);
            this.bStart2.TabIndex = 0;
            this.bStart2.Text = "&Start";
            this.bStart2.UseVisualStyleBackColor = true;
            this.bStart2.Click += new System.EventHandler(this.bStart2_Click);
            // 
            // bStop2
            // 
            this.bStop2.Enabled = false;
            this.bStop2.Location = new System.Drawing.Point(84, 3);
            this.bStop2.Name = "bStop2";
            this.bStop2.Size = new System.Drawing.Size(75, 23);
            this.bStop2.TabIndex = 1;
            this.bStop2.Text = "&Stop";
            this.bStop2.UseVisualStyleBackColor = true;
            this.bStop2.Click += new System.EventHandler(this.bStop2_Click);
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.bStart3);
            this.flowLayoutPanel6.Controls.Add(this.bStop3);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(574, 171);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel6.TabIndex = 21;
            // 
            // bStart3
            // 
            this.bStart3.Location = new System.Drawing.Point(3, 3);
            this.bStart3.Name = "bStart3";
            this.bStart3.Size = new System.Drawing.Size(75, 23);
            this.bStart3.TabIndex = 0;
            this.bStart3.Text = "&Start";
            this.bStart3.UseVisualStyleBackColor = true;
            this.bStart3.Click += new System.EventHandler(this.bStart3_Click);
            // 
            // bStop3
            // 
            this.bStop3.Enabled = false;
            this.bStop3.Location = new System.Drawing.Point(84, 3);
            this.bStop3.Name = "bStop3";
            this.bStop3.Size = new System.Drawing.Size(75, 23);
            this.bStop3.TabIndex = 1;
            this.bStop3.Text = "&Stop";
            this.bStop3.UseVisualStyleBackColor = true;
            this.bStop3.Click += new System.EventHandler(this.bStop3_Click);
            // 
            // tbCmdl
            // 
            this.tbCmdl.Location = new System.Drawing.Point(12, 263);
            this.tbCmdl.Name = "tbCmdl";
            this.tbCmdl.Size = new System.Drawing.Size(736, 19);
            this.tbCmdl.TabIndex = 2;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(10, 248);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(79, 12);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Command line:";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(529, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 34);
            this.label2.TabIndex = 22;
            this.label2.Text = "Auto\r\nstart";
            // 
            // cbA1
            // 
            this.cbA1.AutoSize = true;
            this.cbA1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbA1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbA1.Location = new System.Drawing.Point(529, 41);
            this.cbA1.Name = "cbA1";
            this.cbA1.Size = new System.Drawing.Size(37, 51);
            this.cbA1.TabIndex = 23;
            this.cbA1.UseVisualStyleBackColor = true;
            // 
            // cbA2
            // 
            this.cbA2.AutoSize = true;
            this.cbA2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbA2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbA2.Location = new System.Drawing.Point(529, 100);
            this.cbA2.Name = "cbA2";
            this.cbA2.Size = new System.Drawing.Size(37, 51);
            this.cbA2.TabIndex = 24;
            this.cbA2.UseVisualStyleBackColor = true;
            // 
            // cbA3
            // 
            this.cbA3.AutoSize = true;
            this.cbA3.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbA3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbA3.Location = new System.Drawing.Point(529, 159);
            this.cbA3.Name = "cbA3";
            this.cbA3.Size = new System.Drawing.Size(37, 53);
            this.cbA3.TabIndex = 25;
            this.cbA3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 295);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.tbCmdl);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP for AFP";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAFPHost;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.TextBox tbAFPPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFTPPort;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbAppend;
        private System.Windows.Forms.RadioButton rbPrepend;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbAFPHost2;
        private System.Windows.Forms.TextBox tbFTPPort2;
        private System.Windows.Forms.TextBox tbAFPPort2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton rbNo2;
        private System.Windows.Forms.RadioButton rbAppend2;
        private System.Windows.Forms.RadioButton rbPrepend2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbFTPPort3;
        private System.Windows.Forms.TextBox tbAFPHost3;
        private System.Windows.Forms.TextBox tbAFPPort3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RadioButton rbNo3;
        private System.Windows.Forms.RadioButton rbAppend3;
        private System.Windows.Forms.RadioButton rbPrepend3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Button bStart2;
        private System.Windows.Forms.Button bStop2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Button bStart3;
        private System.Windows.Forms.Button bStop3;
        private AFPServ s1;
        private AFPServ s2;
        private AFPServ s3;
        private System.Windows.Forms.TextBox tbCmdl;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox cbA2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbA1;
        private System.Windows.Forms.CheckBox cbA3;
    }
}

