using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLServer2005Reloc {
    public partial class RunSQLForm : Form {
        SqlCommand Cmd;

        public RunSQLForm(SqlCommand Cmd) {
            InitializeComponent();

            this.Cmd = Cmd;

            this.tbSQL.Text = Cmd.CommandText;
        }

        private void RunSQLForm_Load(object sender, EventArgs e) {

        }

        private void bOk_Click(object sender, EventArgs e) {
            Cmd.CommandText = tbSQL.Text;

            Cmd.ExecuteNonQuery();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}