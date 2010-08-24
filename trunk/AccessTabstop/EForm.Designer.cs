namespace AccessTabstop {
    partial class EForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAssign = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSwap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHideNeg = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHideFrm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonIndexIndic = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripButtonAssign,
            this.toolStripButtonSwap,
            this.toolStripSeparator4,
            this.toolStripButtonHideNeg,
            this.toolStripSeparator5,
            this.toolStripButtonHideFrm,
            this.toolStripSeparator2,
            this.toolStripButtonIndexIndic,
            this.toolStripSeparator3,
            this.toolStripButtonAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(758, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(70, 22);
            this.toolStripDropDownButton1.Text = "フォーム";
            this.toolStripDropDownButton1.DropDownOpening += new System.EventHandler(this.toolStripDropDownButton1_DropDownOpening);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAssign
            // 
            this.toolStripButtonAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAssign.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAssign.Image")));
            this.toolStripButtonAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAssign.Name = "toolStripButtonAssign";
            this.toolStripButtonAssign.Size = new System.Drawing.Size(111, 22);
            this.toolStripButtonAssign.Text = "タブ順序を割当します";
            this.toolStripButtonAssign.Click += new System.EventHandler(this.toolStripButtonAssign_Click);
            // 
            // toolStripButtonSwap
            // 
            this.toolStripButtonSwap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSwap.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSwap.Image")));
            this.toolStripButtonSwap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSwap.Name = "toolStripButtonSwap";
            this.toolStripButtonSwap.Size = new System.Drawing.Size(131, 22);
            this.toolStripButtonSwap.Text = "タブ順序を入れ替えします";
            this.toolStripButtonSwap.Click += new System.EventHandler(this.toolStripButtonSwap_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonHideNeg
            // 
            this.toolStripButtonHideNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHideNeg.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHideNeg.Image")));
            this.toolStripButtonHideNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHideNeg.Name = "toolStripButtonHideNeg";
            this.toolStripButtonHideNeg.Size = new System.Drawing.Size(70, 22);
            this.toolStripButtonHideNeg.Text = "-1を隠します";
            this.toolStripButtonHideNeg.Click += new System.EventHandler(this.toolStripButtonHideNeg_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonHideFrm
            // 
            this.toolStripButtonHideFrm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonHideFrm.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHideFrm.Image")));
            this.toolStripButtonHideFrm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHideFrm.Name = "toolStripButtonHideFrm";
            this.toolStripButtonHideFrm.Size = new System.Drawing.Size(72, 22);
            this.toolStripButtonHideFrm.Text = "Frameを隠す";
            this.toolStripButtonHideFrm.Click += new System.EventHandler(this.toolStripButtonHideFrm_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonIndexIndic
            // 
            this.toolStripButtonIndexIndic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonIndexIndic.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonIndexIndic.Image")));
            this.toolStripButtonIndexIndic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonIndexIndic.Name = "toolStripButtonIndexIndic";
            this.toolStripButtonIndexIndic.Size = new System.Drawing.Size(69, 22);
            this.toolStripButtonIndexIndic.Text = "次インデックス";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(39, 22);
            this.toolStripButtonAbout.Text = "About";
            this.toolStripButtonAbout.Click += new System.EventHandler(this.toolStripButtonAbout_Click);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(758, 419);
            this.panelMain.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 444);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Accessフォーム タブストップ順序校正プログラム";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripButtonIndexIndic;
        private System.Windows.Forms.ToolStripButton toolStripButtonAssign;
        private System.Windows.Forms.ToolStripButton toolStripButtonSwap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonHideNeg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonHideFrm;


    }
}

