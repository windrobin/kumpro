namespace EVTracer {
    partial class TForm {
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
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.sch = new System.Windows.Forms.SplitContainer();
            this.gv = new System.Windows.Forms.DataGridView();
            this.日時DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.オブジェクトDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.一次アカウントDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.クライアントアカウントDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ds1 = new EVTracer.Ds();
            this.label1 = new System.Windows.Forms.Label();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tstop = new System.Windows.Forms.ToolStrip();
            this.bLoadfrmSeclog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bHelpSecpol = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bHelpSacl = new System.Windows.Forms.ToolStripButton();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.sch.Panel1.SuspendLayout();
            this.sch.Panel2.SuspendLayout();
            this.sch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds1)).BeginInit();
            this.tstop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsc
            // 
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.sch);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(904, 517);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(904, 542);
            this.tsc.TabIndex = 0;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.tstop);
            // 
            // sch
            // 
            this.sch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sch.Location = new System.Drawing.Point(0, 0);
            this.sch.Name = "sch";
            // 
            // sch.Panel1
            // 
            this.sch.Panel1.Controls.Add(this.gv);
            this.sch.Panel1.Controls.Add(this.label1);
            // 
            // sch.Panel2
            // 
            this.sch.Panel2.Controls.Add(this.rtb);
            this.sch.Panel2.Controls.Add(this.label2);
            this.sch.Size = new System.Drawing.Size(904, 517);
            this.sch.SplitterDistance = 590;
            this.sch.SplitterWidth = 5;
            this.sch.TabIndex = 0;
            // 
            // gv
            // 
            this.gv.AutoGenerateColumns = false;
            this.gv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.日時DataGridViewTextBoxColumn,
            this.オブジェクトDataGridViewTextBoxColumn,
            this.一次アカウントDataGridViewTextBoxColumn,
            this.クライアントアカウントDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn});
            this.gv.DataMember = "DTDel";
            this.gv.DataSource = this.ds1;
            this.gv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv.Location = new System.Drawing.Point(0, 12);
            this.gv.Name = "gv";
            this.gv.RowTemplate.Height = 21;
            this.gv.Size = new System.Drawing.Size(588, 503);
            this.gv.TabIndex = 1;
            this.gv.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_RowEnter);
            // 
            // 日時DataGridViewTextBoxColumn
            // 
            this.日時DataGridViewTextBoxColumn.DataPropertyName = "日時";
            this.日時DataGridViewTextBoxColumn.HeaderText = "日時";
            this.日時DataGridViewTextBoxColumn.Name = "日時DataGridViewTextBoxColumn";
            // 
            // オブジェクトDataGridViewTextBoxColumn
            // 
            this.オブジェクトDataGridViewTextBoxColumn.DataPropertyName = "オブジェクト";
            this.オブジェクトDataGridViewTextBoxColumn.HeaderText = "オブジェクト";
            this.オブジェクトDataGridViewTextBoxColumn.Name = "オブジェクトDataGridViewTextBoxColumn";
            this.オブジェクトDataGridViewTextBoxColumn.Width = 300;
            // 
            // 一次アカウントDataGridViewTextBoxColumn
            // 
            this.一次アカウントDataGridViewTextBoxColumn.DataPropertyName = "一次アカウント";
            this.一次アカウントDataGridViewTextBoxColumn.HeaderText = "一次アカウント";
            this.一次アカウントDataGridViewTextBoxColumn.Name = "一次アカウントDataGridViewTextBoxColumn";
            // 
            // クライアントアカウントDataGridViewTextBoxColumn
            // 
            this.クライアントアカウントDataGridViewTextBoxColumn.DataPropertyName = "クライアントアカウント";
            this.クライアントアカウントDataGridViewTextBoxColumn.HeaderText = "クライアントアカウント";
            this.クライアントアカウントDataGridViewTextBoxColumn.Name = "クライアントアカウントDataGridViewTextBoxColumn";
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.Width = 50;
            // 
            // ds1
            // 
            this.ds1.DataSetName = "Ds";
            this.ds1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "削除したオブジェクトたち：";
            // 
            // rtb
            // 
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(0, 12);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(307, 503);
            this.rtb.TabIndex = 1;
            this.rtb.Text = "";
            this.rtb.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "イベントログエントリを追跡します：";
            // 
            // tstop
            // 
            this.tstop.Dock = System.Windows.Forms.DockStyle.None;
            this.tstop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bLoadfrmSeclog,
            this.toolStripSeparator1,
            this.bHelpSecpol,
            this.toolStripSeparator2,
            this.bHelpSacl});
            this.tstop.Location = new System.Drawing.Point(3, 0);
            this.tstop.Name = "tstop";
            this.tstop.Size = new System.Drawing.Size(798, 25);
            this.tstop.TabIndex = 0;
            // 
            // bLoadfrmSeclog
            // 
            this.bLoadfrmSeclog.Image = global::EVTracer.Properties.Resources.AttachmentHS;
            this.bLoadfrmSeclog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bLoadfrmSeclog.Name = "bLoadfrmSeclog";
            this.bLoadfrmSeclog.Size = new System.Drawing.Size(165, 22);
            this.bLoadfrmSeclog.Text = "セキュリティログから、読み込む";
            this.bLoadfrmSeclog.Click += new System.EventHandler(this.bLoadfrmSeclog_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bHelpSecpol
            // 
            this.bHelpSecpol.Image = global::EVTracer.Properties.Resources.Help;
            this.bHelpSecpol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bHelpSecpol.Name = "bHelpSecpol";
            this.bHelpSecpol.Size = new System.Drawing.Size(298, 22);
            this.bHelpSecpol.Text = "監査内容をセキュリティログに記録する方法を参照(HTML)";
            this.bHelpSecpol.Click += new System.EventHandler(this.bHelpSecpol_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bHelpSacl
            // 
            this.bHelpSacl.Image = global::EVTracer.Properties.Resources.Help;
            this.bHelpSacl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bHelpSacl.Name = "bHelpSacl";
            this.bHelpSacl.Size = new System.Drawing.Size(280, 22);
            this.bHelpSacl.Text = "ファイル・フォルダを監査対象にする方法を参照(HTML)";
            this.bHelpSacl.Click += new System.EventHandler(this.bHelpSacl_Click);
            // 
            // TForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 542);
            this.Controls.Add(this.tsc);
            this.Name = "TForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVTracer";
            this.Load += new System.EventHandler(this.TForm_Load);
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.sch.Panel1.ResumeLayout(false);
            this.sch.Panel1.PerformLayout();
            this.sch.Panel2.ResumeLayout(false);
            this.sch.Panel2.PerformLayout();
            this.sch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds1)).EndInit();
            this.tstop.ResumeLayout(false);
            this.tstop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.ToolStrip tstop;
        private System.Windows.Forms.SplitContainer sch;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.Label label2;
        private Ds ds1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日時DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn オブジェクトDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn 一次アカウントDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn クライアントアカウントDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripButton bLoadfrmSeclog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bHelpSecpol;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton bHelpSacl;
    }
}

