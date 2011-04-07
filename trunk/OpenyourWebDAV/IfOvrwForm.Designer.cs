namespace OpenyourWebDAV {
    partial class IfOvrwForm {
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
            this.lfp = new System.Windows.Forms.Label();
            this.bYes = new System.Windows.Forms.Button();
            this.pbMark = new System.Windows.Forms.PictureBox();
            this.bNo = new System.Windows.Forms.Button();
            this.bYesToAll = new System.Windows.Forms.Button();
            this.bNoToAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMark)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "上書きしますか?";
            // 
            // lfp
            // 
            this.lfp.AutoSize = true;
            this.lfp.Location = new System.Drawing.Point(66, 48);
            this.lfp.Name = "lfp";
            this.lfp.Size = new System.Drawing.Size(11, 12);
            this.lfp.TabIndex = 2;
            this.lfp.Text = "...";
            // 
            // bYes
            // 
            this.bYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.bYes.Location = new System.Drawing.Point(68, 79);
            this.bYes.Name = "bYes";
            this.bYes.Size = new System.Drawing.Size(104, 32);
            this.bYes.TabIndex = 3;
            this.bYes.Text = "はい";
            this.bYes.UseVisualStyleBackColor = true;
            this.bYes.Click += new System.EventHandler(this.bOk_Click);
            // 
            // pbMark
            // 
            this.pbMark.Image = global::OpenyourWebDAV.Properties.Resources.question;
            this.pbMark.Location = new System.Drawing.Point(12, 12);
            this.pbMark.Name = "pbMark";
            this.pbMark.Size = new System.Drawing.Size(48, 48);
            this.pbMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMark.TabIndex = 0;
            this.pbMark.TabStop = false;
            // 
            // bNo
            // 
            this.bNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.bNo.Location = new System.Drawing.Point(178, 79);
            this.bNo.Name = "bNo";
            this.bNo.Size = new System.Drawing.Size(104, 32);
            this.bNo.TabIndex = 4;
            this.bNo.Text = "いいえ";
            this.bNo.UseVisualStyleBackColor = true;
            this.bNo.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bYesToAll
            // 
            this.bYesToAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bYesToAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bYesToAll.Location = new System.Drawing.Point(288, 79);
            this.bYesToAll.Name = "bYesToAll";
            this.bYesToAll.Size = new System.Drawing.Size(104, 32);
            this.bYesToAll.TabIndex = 5;
            this.bYesToAll.Text = "すべて、はい";
            this.bYesToAll.UseVisualStyleBackColor = true;
            this.bYesToAll.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bNoToAll
            // 
            this.bNoToAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bNoToAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bNoToAll.Location = new System.Drawing.Point(398, 79);
            this.bNoToAll.Name = "bNoToAll";
            this.bNoToAll.Size = new System.Drawing.Size(104, 32);
            this.bNoToAll.TabIndex = 6;
            this.bNoToAll.Text = "すべて、いいえ";
            this.bNoToAll.UseVisualStyleBackColor = true;
            this.bNoToAll.Click += new System.EventHandler(this.bOk_Click);
            // 
            // IfOvrwForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 123);
            this.Controls.Add(this.bNoToAll);
            this.Controls.Add(this.bYesToAll);
            this.Controls.Add(this.bNo);
            this.Controls.Add(this.bYes);
            this.Controls.Add(this.lfp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbMark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IfOvrwForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "上書き確認";
            this.Load += new System.EventHandler(this.IfOvrwForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IfOvrwForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbMark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lfp;
        private System.Windows.Forms.Button bYes;
        private System.Windows.Forms.Button bNo;
        private System.Windows.Forms.Button bYesToAll;
        private System.Windows.Forms.Button bNoToAll;
    }
}