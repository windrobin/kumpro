namespace StatFilesbyExt {
    partial class StatForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.vStatBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.vStatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsVol = new StatFilesbyExt.DSVol();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.vStatBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.vStatDataGridView = new System.Windows.Forms.DataGridView();
            this.c拡張子 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cファイル数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c数の割合 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cファイル容量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c容量の割合 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bwComputer = new System.ComponentModel.BackgroundWorker();
            this.tsc = new System.Windows.Forms.ToolStripContainer();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.vStatBindingNavigator)).BeginInit();
            this.vStatBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vStatBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vStatDataGridView)).BeginInit();
            this.tsc.ContentPanel.SuspendLayout();
            this.tsc.TopToolStripPanel.SuspendLayout();
            this.tsc.SuspendLayout();
            this.SuspendLayout();
            // 
            // vStatBindingNavigator
            // 
            this.vStatBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.vStatBindingNavigator.BindingSource = this.vStatBindingSource;
            this.vStatBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.vStatBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.vStatBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.vStatBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.vStatBindingNavigatorSaveItem,
            this.toolStripSeparator1,
            this.tsbCopy});
            this.vStatBindingNavigator.Location = new System.Drawing.Point(3, 0);
            this.vStatBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.vStatBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.vStatBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.vStatBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.vStatBindingNavigator.Name = "vStatBindingNavigator";
            this.vStatBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.vStatBindingNavigator.Size = new System.Drawing.Size(435, 25);
            this.vStatBindingNavigator.TabIndex = 0;
            this.vStatBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新規追加";
            // 
            // vStatBindingSource
            // 
            this.vStatBindingSource.DataMember = "VStat";
            this.vStatBindingSource.DataSource = this.dsVol;
            // 
            // dsVol
            // 
            this.dsVol.DataSetName = "DSVol";
            this.dsVol.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(27, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "項目の総数";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "削除";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "最初に移動";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "前に戻る";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 19);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "現在の場所";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "次に移動";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "最後に移動";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // vStatBindingNavigatorSaveItem
            // 
            this.vStatBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vStatBindingNavigatorSaveItem.Enabled = false;
            this.vStatBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("vStatBindingNavigatorSaveItem.Image")));
            this.vStatBindingNavigatorSaveItem.Name = "vStatBindingNavigatorSaveItem";
            this.vStatBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.vStatBindingNavigatorSaveItem.Text = "データの保存";
            // 
            // vStatDataGridView
            // 
            this.vStatDataGridView.AllowUserToAddRows = false;
            this.vStatDataGridView.AllowUserToDeleteRows = false;
            this.vStatDataGridView.AutoGenerateColumns = false;
            this.vStatDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c拡張子,
            this.cファイル数,
            this.c数の割合,
            this.cファイル容量,
            this.c容量の割合});
            this.vStatDataGridView.DataSource = this.vStatBindingSource;
            this.vStatDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vStatDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.vStatDataGridView.Location = new System.Drawing.Point(0, 0);
            this.vStatDataGridView.Name = "vStatDataGridView";
            this.vStatDataGridView.RowTemplate.Height = 21;
            this.vStatDataGridView.Size = new System.Drawing.Size(681, 395);
            this.vStatDataGridView.TabIndex = 1;
            // 
            // c拡張子
            // 
            this.c拡張子.DataPropertyName = "拡張子";
            this.c拡張子.HeaderText = "拡張子";
            this.c拡張子.Name = "c拡張子";
            this.c拡張子.Width = 150;
            // 
            // cファイル数
            // 
            this.cファイル数.DataPropertyName = "ファイル数";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            this.cファイル数.DefaultCellStyle = dataGridViewCellStyle5;
            this.cファイル数.HeaderText = "ファイル数";
            this.cファイル数.Name = "cファイル数";
            this.cファイル数.Width = 120;
            // 
            // c数の割合
            // 
            this.c数の割合.DataPropertyName = "数の割合";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "P0";
            this.c数の割合.DefaultCellStyle = dataGridViewCellStyle6;
            this.c数の割合.HeaderText = "数の割合";
            this.c数の割合.Name = "c数の割合";
            // 
            // cファイル容量
            // 
            this.cファイル容量.DataPropertyName = "ファイル容量";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            this.cファイル容量.DefaultCellStyle = dataGridViewCellStyle7;
            this.cファイル容量.HeaderText = "ファイル容量";
            this.cファイル容量.Name = "cファイル容量";
            this.cファイル容量.Width = 120;
            // 
            // c容量の割合
            // 
            this.c容量の割合.DataPropertyName = "容量の割合";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "P0";
            this.c容量の割合.DefaultCellStyle = dataGridViewCellStyle8;
            this.c容量の割合.HeaderText = "容量の割合";
            this.c容量の割合.Name = "c容量の割合";
            // 
            // bwComputer
            // 
            this.bwComputer.WorkerSupportsCancellation = true;
            this.bwComputer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwComputer_DoWork);
            this.bwComputer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwComputer_RunWorkerCompleted);
            // 
            // tsc
            // 
            // 
            // tsc.ContentPanel
            // 
            this.tsc.ContentPanel.Controls.Add(this.vStatDataGridView);
            this.tsc.ContentPanel.Size = new System.Drawing.Size(681, 395);
            this.tsc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsc.Location = new System.Drawing.Point(0, 0);
            this.tsc.Name = "tsc";
            this.tsc.Size = new System.Drawing.Size(681, 420);
            this.tsc.TabIndex = 2;
            this.tsc.Text = "toolStripContainer1";
            // 
            // tsc.TopToolStripPanel
            // 
            this.tsc.TopToolStripPanel.Controls.Add(this.vStatBindingNavigator);
            this.tsc.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCopy
            // 
            this.tsbCopy.Image = global::StatFilesbyExt.Properties.Resources.CopyHS;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(128, 22);
            this.tsbCopy.Text = "CSV形式でコピーする";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // StatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 420);
            this.Controls.Add(this.tsc);
            this.Name = "StatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "集計結果";
            this.Load += new System.EventHandler(this.StatForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.vStatBindingNavigator)).EndInit();
            this.vStatBindingNavigator.ResumeLayout(false);
            this.vStatBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vStatBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vStatDataGridView)).EndInit();
            this.tsc.ContentPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.ResumeLayout(false);
            this.tsc.TopToolStripPanel.PerformLayout();
            this.tsc.ResumeLayout(false);
            this.tsc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DSVol dsVol;
        private System.Windows.Forms.BindingSource vStatBindingSource;
        private System.Windows.Forms.BindingNavigator vStatBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton vStatBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView vStatDataGridView;
        private System.ComponentModel.BackgroundWorker bwComputer;
        private System.Windows.Forms.ToolStripContainer tsc;
        private System.Windows.Forms.DataGridViewTextBoxColumn c拡張子;
        private System.Windows.Forms.DataGridViewTextBoxColumn cファイル数;
        private System.Windows.Forms.DataGridViewTextBoxColumn c数の割合;
        private System.Windows.Forms.DataGridViewTextBoxColumn cファイル容量;
        private System.Windows.Forms.DataGridViewTextBoxColumn c容量の割合;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCopy;
    }
}