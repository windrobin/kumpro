namespace ConvertYubinKenAll {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bSel = new System.Windows.Forms.Button();
            this.ofdcsv = new System.Windows.Forms.OpenFileDialog();
            this.sfdcsv = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFmt = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lWip = new System.Windows.Forms.Label();
            this.bConv = new System.Windows.Forms.Button();
            this.clb = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bFetch = new System.Windows.Forms.Button();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bwDLConv = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "郵便番号辞書を、都合の良い形式に変換します。";
            // 
            // bSel
            // 
            this.bSel.Location = new System.Drawing.Point(12, 126);
            this.bSel.Name = "bSel";
            this.bSel.Size = new System.Drawing.Size(150, 46);
            this.bSel.TabIndex = 2;
            this.bSel.Text = "ファイルを選択してください...";
            this.bSel.UseVisualStyleBackColor = true;
            this.bSel.Click += new System.EventHandler(this.bSel_Click);
            // 
            // ofdcsv
            // 
            this.ofdcsv.DefaultExt = "csv";
            this.ofdcsv.Filter = "*.csv|*.csv";
            // 
            // sfdcsv
            // 
            this.sfdcsv.DefaultExt = "csv";
            this.sfdcsv.Filter = "*.csv|*.csv";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "出力の書式：";
            // 
            // cbFmt
            // 
            this.cbFmt.FormattingEnabled = true;
            this.cbFmt.Items.AddRange(new object[] {
            "{郵便番号000-0000} {住所}",
            "{郵便番号000-0000} {住所空白有り}",
            "{郵便番号0000000} {住所}",
            "{郵便番号0000000} {住所空白有り}"});
            this.cbFmt.Location = new System.Drawing.Point(12, 91);
            this.cbFmt.Name = "cbFmt";
            this.cbFmt.Size = new System.Drawing.Size(256, 20);
            this.cbFmt.TabIndex = 4;
            this.cbFmt.Text = "{郵便番号000-0000} {住所}";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lWip);
            this.groupBox1.Controls.Add(this.bConv);
            this.groupBox1.Controls.Add(this.clb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.bFetch);
            this.groupBox1.Controls.Add(this.tbURL);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(191, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 278);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "又は、郵便局から…";
            // 
            // lWip
            // 
            this.lWip.AutoSize = true;
            this.lWip.Location = new System.Drawing.Point(6, 251);
            this.lWip.Name = "lWip";
            this.lWip.Size = new System.Drawing.Size(11, 24);
            this.lWip.TabIndex = 6;
            this.lWip.Text = "...\r\n...";
            // 
            // bConv
            // 
            this.bConv.Location = new System.Drawing.Point(346, 98);
            this.bConv.Name = "bConv";
            this.bConv.Size = new System.Drawing.Size(75, 23);
            this.bConv.TabIndex = 5;
            this.bConv.Text = "変換...";
            this.bConv.UseVisualStyleBackColor = true;
            this.bConv.Click += new System.EventHandler(this.bConv_Click);
            // 
            // clb
            // 
            this.clb.ColumnWidth = 100;
            this.clb.FormattingEnabled = true;
            this.clb.IntegralHeight = false;
            this.clb.Location = new System.Drawing.Point(6, 98);
            this.clb.MultiColumn = true;
            this.clb.Name = "clb";
            this.clb.ScrollAlwaysVisible = true;
            this.clb.Size = new System.Drawing.Size(334, 150);
            this.clb.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "都道府県一覧";
            // 
            // bFetch
            // 
            this.bFetch.Location = new System.Drawing.Point(6, 57);
            this.bFetch.Name = "bFetch";
            this.bFetch.Size = new System.Drawing.Size(75, 23);
            this.bFetch.TabIndex = 2;
            this.bFetch.Text = "一覧を取得";
            this.bFetch.UseVisualStyleBackColor = true;
            this.bFetch.Click += new System.EventHandler(this.bFetch_Click);
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(6, 32);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(415, 19);
            this.tbURL.TabIndex = 1;
            this.tbURL.Text = "http://www.post.japanpost.jp/zipcode/dl/kogaki-zip.html";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "URL";
            // 
            // bwDLConv
            // 
            this.bwDLConv.WorkerReportsProgress = true;
            this.bwDLConv.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDLConv_DoWork);
            this.bwDLConv.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDLConv_RunWorkerCompleted);
            this.bwDLConv.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwDLConv_ProgressChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 428);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbFmt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bSel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convert Yubin KenAll";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bSel;
        private System.Windows.Forms.OpenFileDialog ofdcsv;
        private System.Windows.Forms.SaveFileDialog sfdcsv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFmt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bFetch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clb;
        private System.Windows.Forms.Button bConv;
        private System.ComponentModel.BackgroundWorker bwDLConv;
        private System.Windows.Forms.Label lWip;
    }
}

