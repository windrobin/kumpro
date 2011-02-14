namespace SendHostAd {
    partial class NForm {
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
            this.tbTask = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbH6 = new System.Windows.Forms.RadioButton();
            this.rbD1 = new System.Windows.Forms.RadioButton();
            this.rbD3 = new System.Windows.Forms.RadioButton();
            this.bREG = new System.Windows.Forms.Button();
            this.bTL = new System.Windows.Forms.Button();
            this.E = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.E)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "タスク名称";
            // 
            // tbTask
            // 
            this.tbTask.Location = new System.Drawing.Point(12, 24);
            this.tbTask.Name = "tbTask";
            this.tbTask.Size = new System.Drawing.Size(155, 19);
            this.tbTask.TabIndex = 1;
            this.tbTask.Text = "ホスト通知";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "通知先のURL";
            // 
            // tbUri
            // 
            this.tbUri.Location = new System.Drawing.Point(12, 61);
            this.tbUri.Name = "tbUri";
            this.tbUri.Size = new System.Drawing.Size(416, 19);
            this.tbUri.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "タイミング";
            // 
            // rbH6
            // 
            this.rbH6.AutoSize = true;
            this.rbH6.Checked = true;
            this.rbH6.Location = new System.Drawing.Point(12, 98);
            this.rbH6.Name = "rbH6";
            this.rbH6.Size = new System.Drawing.Size(83, 16);
            this.rbH6.TabIndex = 5;
            this.rbH6.TabStop = true;
            this.rbH6.Text = "6時間／1回";
            this.rbH6.UseVisualStyleBackColor = true;
            // 
            // rbD1
            // 
            this.rbD1.AutoSize = true;
            this.rbD1.Location = new System.Drawing.Point(101, 98);
            this.rbD1.Name = "rbD1";
            this.rbD1.Size = new System.Drawing.Size(71, 16);
            this.rbD1.TabIndex = 6;
            this.rbD1.Text = "1日／1回";
            this.rbD1.UseVisualStyleBackColor = true;
            // 
            // rbD3
            // 
            this.rbD3.AutoSize = true;
            this.rbD3.Location = new System.Drawing.Point(178, 98);
            this.rbD3.Name = "rbD3";
            this.rbD3.Size = new System.Drawing.Size(71, 16);
            this.rbD3.TabIndex = 7;
            this.rbD3.TabStop = true;
            this.rbD3.Text = "3日／1回";
            this.rbD3.UseVisualStyleBackColor = true;
            // 
            // bREG
            // 
            this.bREG.Location = new System.Drawing.Point(12, 132);
            this.bREG.Name = "bREG";
            this.bREG.Size = new System.Drawing.Size(108, 35);
            this.bREG.TabIndex = 8;
            this.bREG.Text = "タスク登録";
            this.bREG.UseVisualStyleBackColor = true;
            this.bREG.Click += new System.EventHandler(this.bREG_Click);
            // 
            // bTL
            // 
            this.bTL.Location = new System.Drawing.Point(126, 132);
            this.bTL.Name = "bTL";
            this.bTL.Size = new System.Drawing.Size(108, 35);
            this.bTL.TabIndex = 9;
            this.bTL.Text = "タスク一覧";
            this.bTL.UseVisualStyleBackColor = true;
            this.bTL.Click += new System.EventHandler(this.bTL_Click);
            // 
            // E
            // 
            this.E.ContainerControl = this;
            // 
            // NForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 179);
            this.Controls.Add(this.bTL);
            this.Controls.Add(this.bREG);
            this.Controls.Add(this.rbD3);
            this.Controls.Add(this.rbD1);
            this.Controls.Add(this.rbH6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbUri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTask);
            this.Controls.Add(this.label1);
            this.Name = "NForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ホスト通知";
            this.Load += new System.EventHandler(this.NForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.E)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbH6;
        private System.Windows.Forms.RadioButton rbD1;
        private System.Windows.Forms.RadioButton rbD3;
        private System.Windows.Forms.Button bREG;
        private System.Windows.Forms.Button bTL;
        private System.Windows.Forms.ErrorProvider E;
    }
}

