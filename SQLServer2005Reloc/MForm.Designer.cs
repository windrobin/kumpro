namespace SQLServer2005Reloc {
    partial class MForm {
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
            this.cbSQLServers = new System.Windows.Forms.ComboBox();
            this.bUseSQLServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSQLInst = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSQLConn = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.llNeedRestart = new System.Windows.Forms.LinkLabel();
            this.bRefMdf = new System.Windows.Forms.Button();
            this.bUpdateRes = new System.Windows.Forms.Button();
            this.tbResLdf = new System.Windows.Forms.TextBox();
            this.tbResMdf = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ofdMdf = new System.Windows.Forms.OpenFileDialog();
            this.bConnTest = new System.Windows.Forms.Button();
            this.bGetDBt = new System.Windows.Forms.Button();
            this.gv = new System.Windows.Forms.DataGridView();
            this.bRunServer = new System.Windows.Forms.Button();
            this.bStopServer = new System.Windows.Forms.Button();
            this.bSaveDBt = new System.Windows.Forms.Button();
            this.bReplDBt = new System.Windows.Forms.Button();
            this.llNeedRestart2 = new System.Windows.Forms.LinkLabel();
            this.bStartSqlNorm = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSQLServers
            // 
            this.cbSQLServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSQLServers.FormattingEnabled = true;
            this.cbSQLServers.Location = new System.Drawing.Point(14, 35);
            this.cbSQLServers.Name = "cbSQLServers";
            this.cbSQLServers.Size = new System.Drawing.Size(176, 20);
            this.cbSQLServers.TabIndex = 1;
            // 
            // bUseSQLServer
            // 
            this.bUseSQLServer.Location = new System.Drawing.Point(202, 33);
            this.bUseSQLServer.Name = "bUseSQLServer";
            this.bUseSQLServer.Size = new System.Drawing.Size(75, 23);
            this.bUseSQLServer.TabIndex = 2;
            this.bUseSQLServer.Text = "採用";
            this.bUseSQLServer.UseVisualStyleBackColor = true;
            this.bUseSQLServer.Click += new System.EventHandler(this.bUseSQLServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "SQL Server インスタンス：";
            // 
            // tbSQLInst
            // 
            this.tbSQLInst.Location = new System.Drawing.Point(14, 86);
            this.tbSQLInst.Name = "tbSQLInst";
            this.tbSQLInst.Size = new System.Drawing.Size(172, 19);
            this.tbSQLInst.TabIndex = 7;
            this.tbSQLInst.TextChanged += new System.EventHandler(this.tbSQLInst_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "サービス名から入力しますか?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "SQL Server 接続文字列：";
            // 
            // tbSQLConn
            // 
            this.tbSQLConn.Location = new System.Drawing.Point(14, 135);
            this.tbSQLConn.Name = "tbSQLConn";
            this.tbSQLConn.Size = new System.Drawing.Size(370, 19);
            this.tbSQLConn.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.llNeedRestart);
            this.groupBox2.Controls.Add(this.bRefMdf);
            this.groupBox2.Controls.Add(this.bUpdateRes);
            this.groupBox2.Controls.Add(this.tbResLdf);
            this.groupBox2.Controls.Add(this.tbResMdf);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(14, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 110);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "mssqlsystemresource ファイルパス更新：";
            // 
            // llNeedRestart
            // 
            this.llNeedRestart.AutoSize = true;
            this.llNeedRestart.LinkArea = new System.Windows.Forms.LinkArea(21, 3);
            this.llNeedRestart.Location = new System.Drawing.Point(157, 85);
            this.llNeedRestart.Name = "llNeedRestart";
            this.llNeedRestart.Size = new System.Drawing.Size(263, 17);
            this.llNeedRestart.TabIndex = 6;
            this.llNeedRestart.TabStop = true;
            this.llNeedRestart.Text = "設定を反映するには、SQL Serverの再起動が必要。";
            this.llNeedRestart.UseCompatibleTextRendering = true;
            this.llNeedRestart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llNeedRestart_LinkClicked);
            // 
            // bRefMdf
            // 
            this.bRefMdf.Location = new System.Drawing.Point(508, 20);
            this.bRefMdf.Name = "bRefMdf";
            this.bRefMdf.Size = new System.Drawing.Size(75, 23);
            this.bRefMdf.TabIndex = 2;
            this.bRefMdf.Text = "MDF参照";
            this.bRefMdf.UseVisualStyleBackColor = true;
            this.bRefMdf.Click += new System.EventHandler(this.bRefMdf_Click);
            // 
            // bUpdateRes
            // 
            this.bUpdateRes.Location = new System.Drawing.Point(508, 51);
            this.bUpdateRes.Name = "bUpdateRes";
            this.bUpdateRes.Size = new System.Drawing.Size(75, 23);
            this.bUpdateRes.TabIndex = 5;
            this.bUpdateRes.Text = "SQL確認";
            this.bUpdateRes.UseVisualStyleBackColor = true;
            this.bUpdateRes.Click += new System.EventHandler(this.bUpdateRes_Click);
            // 
            // tbResLdf
            // 
            this.tbResLdf.Location = new System.Drawing.Point(159, 53);
            this.tbResLdf.Name = "tbResLdf";
            this.tbResLdf.Size = new System.Drawing.Size(331, 19);
            this.tbResLdf.TabIndex = 4;
            // 
            // tbResMdf
            // 
            this.tbResMdf.Location = new System.Drawing.Point(159, 22);
            this.tbResMdf.Name = "tbResMdf";
            this.tbResMdf.Size = new System.Drawing.Size(331, 19);
            this.tbResMdf.TabIndex = 1;
            this.tbResMdf.TextChanged += new System.EventHandler(this.tbResMdf_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "mssqlsystemresource.ldf";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "mssqlsystemresource.mdf";
            // 
            // ofdMdf
            // 
            this.ofdMdf.Filter = "mssqlsystemresource.mdf|mssqlsystemresource.mdf";
            // 
            // bConnTest
            // 
            this.bConnTest.Location = new System.Drawing.Point(202, 84);
            this.bConnTest.Name = "bConnTest";
            this.bConnTest.Size = new System.Drawing.Size(75, 23);
            this.bConnTest.TabIndex = 8;
            this.bConnTest.Text = "接続テスト";
            this.bConnTest.UseVisualStyleBackColor = true;
            this.bConnTest.Click += new System.EventHandler(this.bConnTest_Click);
            // 
            // bGetDBt
            // 
            this.bGetDBt.Location = new System.Drawing.Point(14, 307);
            this.bGetDBt.Name = "bGetDBt";
            this.bGetDBt.Size = new System.Drawing.Size(75, 23);
            this.bGetDBt.TabIndex = 12;
            this.bGetDBt.Text = "DB表取得";
            this.bGetDBt.UseVisualStyleBackColor = true;
            this.bGetDBt.Click += new System.EventHandler(this.bGetDBt_Click);
            // 
            // gv
            // 
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Location = new System.Drawing.Point(14, 336);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 21;
            this.gv.Size = new System.Drawing.Size(664, 150);
            this.gv.TabIndex = 14;
            // 
            // bRunServer
            // 
            this.bRunServer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bRunServer.Location = new System.Drawing.Point(314, 33);
            this.bRunServer.Name = "bRunServer";
            this.bRunServer.Size = new System.Drawing.Size(150, 23);
            this.bRunServer.TabIndex = 3;
            this.bRunServer.Text = "開始(緊急メンテ用)";
            this.bRunServer.UseVisualStyleBackColor = true;
            this.bRunServer.Click += new System.EventHandler(this.bRunServer_Click);
            // 
            // bStopServer
            // 
            this.bStopServer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bStopServer.Location = new System.Drawing.Point(473, 33);
            this.bStopServer.Name = "bStopServer";
            this.bStopServer.Size = new System.Drawing.Size(150, 23);
            this.bStopServer.TabIndex = 4;
            this.bStopServer.Text = "停止";
            this.bStopServer.UseVisualStyleBackColor = true;
            this.bStopServer.Click += new System.EventHandler(this.bStopServer_Click);
            // 
            // bSaveDBt
            // 
            this.bSaveDBt.Location = new System.Drawing.Point(603, 307);
            this.bSaveDBt.Name = "bSaveDBt";
            this.bSaveDBt.Size = new System.Drawing.Size(75, 23);
            this.bSaveDBt.TabIndex = 15;
            this.bSaveDBt.Text = "DB表保存";
            this.bSaveDBt.UseVisualStyleBackColor = true;
            this.bSaveDBt.Click += new System.EventHandler(this.bSaveDBt_Click);
            // 
            // bReplDBt
            // 
            this.bReplDBt.Location = new System.Drawing.Point(111, 307);
            this.bReplDBt.Name = "bReplDBt";
            this.bReplDBt.Size = new System.Drawing.Size(75, 23);
            this.bReplDBt.TabIndex = 13;
            this.bReplDBt.Text = "置き換え";
            this.bReplDBt.UseVisualStyleBackColor = true;
            this.bReplDBt.Click += new System.EventHandler(this.bReplDBt_Click);
            // 
            // llNeedRestart2
            // 
            this.llNeedRestart2.AutoSize = true;
            this.llNeedRestart2.LinkArea = new System.Windows.Forms.LinkArea(21, 3);
            this.llNeedRestart2.Location = new System.Drawing.Point(14, 501);
            this.llNeedRestart2.Name = "llNeedRestart2";
            this.llNeedRestart2.Size = new System.Drawing.Size(263, 17);
            this.llNeedRestart2.TabIndex = 16;
            this.llNeedRestart2.TabStop = true;
            this.llNeedRestart2.Text = "設定を反映するには、SQL Serverの再起動が必要。";
            this.llNeedRestart2.UseCompatibleTextRendering = true;
            this.llNeedRestart2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llNeedRestart2_LinkClicked);
            // 
            // bStartSqlNorm
            // 
            this.bStartSqlNorm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bStartSqlNorm.Location = new System.Drawing.Point(314, 62);
            this.bStartSqlNorm.Name = "bStartSqlNorm";
            this.bStartSqlNorm.Size = new System.Drawing.Size(150, 23);
            this.bStartSqlNorm.TabIndex = 5;
            this.bStartSqlNorm.Text = "開始(通常)";
            this.bStartSqlNorm.UseVisualStyleBackColor = true;
            this.bStartSqlNorm.Click += new System.EventHandler(this.bStartSqlNorm_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(312, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "SQL Server サービスを操作しますか?";
            // 
            // MForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 540);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bStartSqlNorm);
            this.Controls.Add(this.llNeedRestart2);
            this.Controls.Add(this.bReplDBt);
            this.Controls.Add(this.bSaveDBt);
            this.Controls.Add(this.bStopServer);
            this.Controls.Add(this.bRunServer);
            this.Controls.Add(this.gv);
            this.Controls.Add(this.bGetDBt);
            this.Controls.Add(this.bConnTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tbSQLConn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSQLInst);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSQLServers);
            this.Controls.Add(this.bUseSQLServer);
            this.Name = "MForm";
            this.Text = "SQLServer2005Reloc";
            this.Load += new System.EventHandler(this.MForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSQLServers;
        private System.Windows.Forms.Button bUseSQLServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSQLInst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSQLConn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbResMdf;
        private System.Windows.Forms.TextBox tbResLdf;
        private System.Windows.Forms.Button bUpdateRes;
        private System.Windows.Forms.Button bRefMdf;
        private System.Windows.Forms.OpenFileDialog ofdMdf;
        private System.Windows.Forms.Button bConnTest;
        private System.Windows.Forms.Button bGetDBt;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.LinkLabel llNeedRestart;
        private System.Windows.Forms.Button bRunServer;
        private System.Windows.Forms.Button bStopServer;
        private System.Windows.Forms.Button bSaveDBt;
        private System.Windows.Forms.Button bReplDBt;
        private System.Windows.Forms.LinkLabel llNeedRestart2;
        private System.Windows.Forms.Button bStartSqlNorm;
        private System.Windows.Forms.Label label4;
    }
}

