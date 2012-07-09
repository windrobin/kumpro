using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using SQLServer2005Reloc.Properties;
using Microsoft.VisualBasic;

namespace SQLServer2005Reloc {
    public partial class MForm : Form {
        public MForm() {
            InitializeComponent();
        }

        private void MForm_Load(object sender, EventArgs e) {
            cbSQLServers.Items.Clear();
            foreach (ServiceController sc in ServiceController.GetServices()) {
                String a = sc.ServiceName;
                if (a.StartsWith("MSSQL$")) {
                    cbSQLServers.Items.Add(a);
                }
            }
            if (cbSQLServers.Items.Count != 0) {
                cbSQLServers.SelectedIndex = 0;
                bUseSQLServer.PerformClick();
            }
        }

        private void bUseSQLServer_Click(object sender, EventArgs e) {
            tbSQLInst.Text = Environment.MachineName + "\\" + cbSQLServers.Text.Split('$')[1];
        }

        private void tbSQLInst_TextChanged(object sender, EventArgs e) {
            tbSQLConn.Text = String.Format("Data Source={0};Integrated Security=SSPI;"
                , tbSQLInst.Text
                );
        }

        private void tbResMdf_TextChanged(object sender, EventArgs e) {

        }

        class Ut {
            internal static bool IsCommand(Control tb) {
                if (tb.Text.Trim().Length != 0)
                    return true;
                tb.Select();
                return false;
            }
            internal static bool FileExists(Control tb) {
                if (File.Exists(tb.Text))
                    return true;
                tb.Select();
                return false;
            }
        }

        private void bUpdateRes_Click(object sender, EventArgs e) {
            if (!Ut.FileExists(tbResMdf)) return;
            if (!Ut.FileExists(tbResLdf)) return;

            if (!Ut.IsCommand(tbSQLConn)) return;
            using (SqlConnection db = new SqlConnection(tbSQLConn.Text)) {
                db.Open();
                new RunSQLForm(new SqlCommand(String.Format(Resources.ALTDb, tbResMdf.Text, tbResLdf.Text), db)).ShowDialog(this);
                db.Close();
            }
        }

        private void bRefMdf_Click(object sender, EventArgs e) {
            if (ofdMdf.ShowDialog(this) == DialogResult.OK) {
                tbResMdf.Text = ofdMdf.FileName;
                tbResLdf.Text = tbResMdf.Text.Replace(".mdf", ".ldf");
            }
        }

        private void bConnTest_Click(object sender, EventArgs e) {
            if (!Ut.IsCommand(tbSQLConn)) return;
            using (SqlConnection db = new SqlConnection(tbSQLConn.Text)) {
                db.Open();
                db.Close();
            }
            MessageBox.Show(this, "接続Ok", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bGetDBt_Click(object sender, EventArgs e) {
            if (!Ut.IsCommand(tbSQLConn)) return;
            using (SqlConnection db = new SqlConnection(tbSQLConn.Text)) {
                db.Open();
                using (SqlDataReader dr = new SqlCommand("SELECT (SELECT name FROM sys.databases WHERE sys.databases.database_id = sys.master_files.database_id)as db,name,physical_name FROM sys.master_files;", db).ExecuteReader()) {
                    DataTable dt = new DataTable();
                    int cx = dr.FieldCount;
                    for (int x = 0; x < cx; x++) {
                        dt.Columns.Add(dr.GetName(x), dr.GetFieldType(x));
                    }
                    Object[] vals = new Object[cx];
                    while (dr.Read()) {
                        dr.GetValues(vals);
                        dt.Rows.Add(vals);
                    }
                    dt.AcceptChanges();
                    gv.DataSource = dt;
                    gv.Refresh();
                    gv.AutoResizeColumns();
                }
                db.Close();
            }
        }

        private void llNeedRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            using (RunCmdForm form = new RunCmdForm("NET.exe STOP " + cbSQLServers.Text + " " + "\r\n" + "NET.exe START " + cbSQLServers.Text + " /f /T3608" + "\r\n" + "PAUSE")) {
                form.ShowDialog(this);
            }
        }

        private void bRunServer_Click(object sender, EventArgs e) {
            using (RunCmdForm form = new RunCmdForm("NET.exe START " + cbSQLServers.Text + " /f /T3608" + "\r\n" + "PAUSE")) {
                form.ShowDialog(this);
            }
        }

        private void bStopServer_Click(object sender, EventArgs e) {
            using (RunCmdForm form = new RunCmdForm("NET.exe STOP " + cbSQLServers.Text + " " + "\r\n" + "PAUSE")) {
                form.ShowDialog(this);
            }
        }

        private void bSaveDBt_Click(object sender2, EventArgs e2) {
            if (!Ut.IsCommand(tbSQLConn)) return;
            if (MessageBox.Show(this, "本当に?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            using (SqlConnection db = new SqlConnection(tbSQLConn.Text)) {
                db.Open();
                DataTable dt = (DataTable)gv.DataSource;
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = new SqlCommand("ALTER DATABASE @db MODIFY FILE (NAME=@name, FILENAME= @physical_name);", db);
                da.RowUpdating += delegate(object sender, SqlRowUpdatingEventArgs e) {
                    e.Command.CommandText = ("ALTER DATABASE @db MODIFY FILE (NAME=@name, FILENAME= @physical_name);"
                        .Replace("@db", "[" + e.Command.Parameters["db"].Value + "]")
                        .Replace("@name", "[" + e.Command.Parameters["name"].Value + "]")
                        .Replace("@physical_name", "'" + e.Command.Parameters["physical_name"].Value + "'")
                        );
                    if (MessageBox.Show(this, e.Command.CommandText, "SQL確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                        throw new ApplicationException("キャンセルしました。");
                    return;
                };
                da.UpdateCommand.Parameters.Add("db", SqlDbType.Text, 0, "db");
                da.UpdateCommand.Parameters.Add("name", SqlDbType.Text, 0, "name");
                da.UpdateCommand.Parameters.Add("physical_name", SqlDbType.Text, 0, "physical_name");
                da.Update(dt);
                db.Close();
            }
        }

        private void bReplDBt_Click(object sender, EventArgs e) {
            String sFrm = Interaction.InputBox("検索する文字列", "", "C:\\Program Files\\", -1, -1);
            if (sFrm.Length == 0) return;
            String sTo = Interaction.InputBox("置換後の文字列", "", "C:\\Program Files (x86)\\", -1, -1);
            if (sTo.Length == 0) return;

            foreach (DataGridViewRow row in gv.Rows) {
                foreach (DataGridViewCell cell in row.Cells) {
                    if (cell.Value is String) {
                        String s = (String)cell.Value;
                        String s2 = s.Replace(sFrm, sTo);
                        if (s != s2) cell.Value = s2;
                    }
                }
            }
        }

        private void bStartSqlNorm_Click(object sender, EventArgs e) {
            using (RunCmdForm form = new RunCmdForm("NET.exe START " + cbSQLServers.Text + " " + "\r\n" + "PAUSE")) {
                form.ShowDialog(this);
            }
        }

        private void llNeedRestart2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            using (RunCmdForm form = new RunCmdForm("NET.exe STOP " + cbSQLServers.Text + " " + "\r\n" + "NET.exe START " + cbSQLServers.Text + " " + "\r\n" + "PAUSE")) {
                form.ShowDialog(this);
            }
        }
    }
}