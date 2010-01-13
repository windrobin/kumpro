namespace RunTimeViser2 {
    partial class RForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lvt = new System.Windows.Forms.ListView();
            this.chEv = new System.Windows.Forms.ColumnHeader();
            this.chDT = new System.Windows.Forms.ColumnHeader();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.mtb = new System.Windows.Forms.MaskedTextBox();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.mc = new System.Windows.Forms.MonthCalendar();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "凡そのWindows起動時間・終了時間";
            // 
            // lvt
            // 
            this.lvt.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEv,
            this.chDT});
            this.lvt.FullRowSelect = true;
            this.lvt.GridLines = true;
            this.lvt.HideSelection = false;
            this.lvt.Location = new System.Drawing.Point(12, 24);
            this.lvt.Name = "lvt";
            this.lvt.Size = new System.Drawing.Size(297, 144);
            this.lvt.SmallImageList = this.il;
            this.lvt.TabIndex = 6;
            this.lvt.UseCompatibleStateImageBehavior = false;
            this.lvt.View = System.Windows.Forms.View.Details;
            this.lvt.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvt_ColumnClick);
            // 
            // chEv
            // 
            this.chEv.Text = "イベント";
            // 
            // chDT
            // 
            this.chDT.Text = "日時";
            this.chDT.Width = 200;
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Fuchsia;
            this.il.Images.SetKeyName(0, "on");
            this.il.Images.SetKeyName(1, "off");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "次の時間に、電源が入っていたかどうか。";
            // 
            // mtb
            // 
            this.mtb.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtb.Location = new System.Drawing.Point(315, 55);
            this.mtb.Mask = "90:00";
            this.mtb.Name = "mtb";
            this.mtb.Size = new System.Drawing.Size(93, 32);
            this.mtb.TabIndex = 1;
            this.mtb.ValidatingType = typeof(System.DateTime);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(414, 55);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 32);
            this.buttonVerify.TabIndex = 2;
            this.buttonVerify.Text = "検査";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // mc
            // 
            this.mc.CalendarDimensions = new System.Drawing.Size(4, 3);
            this.mc.Location = new System.Drawing.Point(18, 180);
            this.mc.Name = "mc";
            this.mc.TabIndex = 4;
            this.mc.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mc_DateSelected);
            this.mc.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mc_DateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 60);
            this.label3.TabIndex = 3;
            this.label3.Text = "「検査」ボタンをクリックしますと、\r\n\r\n直下のカレンダーに反映されます。\r\n\r\n太字＝電源Onであった時です。";
            // 
            // RForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 614);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mc);
            this.Controls.Add(this.buttonVerify);
            this.Controls.Add(this.mtb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvt);
            this.Controls.Add(this.label1);
            this.Name = "RForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunTimeViser 2";
            this.Load += new System.EventHandler(this.RForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvt;
        private System.Windows.Forms.ColumnHeader chEv;
        private System.Windows.Forms.ColumnHeader chDT;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtb;
        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.MonthCalendar mc;
        private System.Windows.Forms.Label label3;
    }
}

