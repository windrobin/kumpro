namespace Sfgaof {
    partial class FForm {
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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.bIn10 = new System.Windows.Forms.Button();
            this.bIn16 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbOut10 = new System.Windows.Forms.TextBox();
            this.tbOut16 = new System.Windows.Forms.TextBox();
            this.lvf = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.llGo = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.tt = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "INPUT";
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(12, 24);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(148, 19);
            this.tbInput.TabIndex = 1;
            this.tbInput.TextChanged += new System.EventHandler(this.tbInput_TextChanged);
            // 
            // bIn10
            // 
            this.bIn10.Location = new System.Drawing.Point(166, 22);
            this.bIn10.Name = "bIn10";
            this.bIn10.Size = new System.Drawing.Size(75, 23);
            this.bIn10.TabIndex = 2;
            this.bIn10.Text = "As decimal";
            this.bIn10.UseVisualStyleBackColor = true;
            this.bIn10.Click += new System.EventHandler(this.bIn10_Click);
            // 
            // bIn16
            // 
            this.bIn16.Location = new System.Drawing.Point(247, 22);
            this.bIn16.Name = "bIn16";
            this.bIn16.Size = new System.Drawing.Size(75, 23);
            this.bIn16.TabIndex = 3;
            this.bIn16.Text = "As hex";
            this.bIn16.UseVisualStyleBackColor = true;
            this.bIn16.Click += new System.EventHandler(this.bIn10_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Flags";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(338, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "OUTPUT (Decimal, Hex)";
            // 
            // tbOut10
            // 
            this.tbOut10.Location = new System.Drawing.Point(340, 24);
            this.tbOut10.Name = "tbOut10";
            this.tbOut10.Size = new System.Drawing.Size(100, 19);
            this.tbOut10.TabIndex = 5;
            // 
            // tbOut16
            // 
            this.tbOut16.Location = new System.Drawing.Point(446, 24);
            this.tbOut16.Name = "tbOut16";
            this.tbOut16.Size = new System.Drawing.Size(100, 19);
            this.tbOut16.TabIndex = 6;
            // 
            // lvf
            // 
            this.lvf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvf.CheckBoxes = true;
            this.lvf.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvf.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvf.FullRowSelect = true;
            this.lvf.GridLines = true;
            this.lvf.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvf.HideSelection = false;
            this.lvf.Location = new System.Drawing.Point(12, 61);
            this.lvf.Name = "lvf";
            this.lvf.Size = new System.Drawing.Size(534, 494);
            this.lvf.TabIndex = 8;
            this.lvf.UseCompatibleStateImageBehavior = false;
            this.lvf.View = System.Windows.Forms.View.Details;
            this.lvf.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvf_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Flags";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Comment";
            this.columnHeader3.Width = 200;
            // 
            // llGo
            // 
            this.llGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llGo.AutoSize = true;
            this.llGo.Location = new System.Drawing.Point(12, 570);
            this.llGo.Name = "llGo";
            this.llGo.Size = new System.Drawing.Size(330, 12);
            this.llGo.TabIndex = 10;
            this.llGo.TabStop = true;
            this.llGo.Text = "http://msdn.microsoft.com/en-us/library/bb762589(VS.85).aspx";
            this.llGo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGo_LinkClicked);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 558);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "MSDN: SFGAO";
            // 
            // tt
            // 
            this.tt.IsBalloon = true;
            this.tt.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            this.tt.ToolTipTitle = "失敗";
            this.tt.Popup += new System.Windows.Forms.PopupEventHandler(this.tt_Popup);
            // 
            // FForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 591);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.llGo);
            this.Controls.Add(this.lvf);
            this.Controls.Add(this.tbOut16);
            this.Controls.Add(this.tbOut10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bIn16);
            this.Controls.Add(this.bIn10);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(570, 203);
            this.Name = "FForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SFGAOF";
            this.Load += new System.EventHandler(this.FForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Button bIn10;
        private System.Windows.Forms.Button bIn16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbOut10;
        private System.Windows.Forms.TextBox tbOut16;
        private System.Windows.Forms.ListView lvf;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.LinkLabel llGo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip tt;
    }
}

