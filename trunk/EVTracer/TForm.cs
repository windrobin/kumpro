using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace EVTracer {
    public partial class TForm : Form {
        public TForm() {
            InitializeComponent();
        }

        class XEv {
            public int id;
            public DateTime dt;
            public String[] al;
            public String message;

            public XEv(EventLogEntry ent) {
                this.id = ((int)ent.InstanceId) & 0x3fffffff;
                this.dt = ent.TimeGenerated;
                this.al = ent.ReplacementStrings;
                this.message = ent.Message;
            }

            public String ObjectName {
                get {
                    if (id != 560) throw new NotSupportedException();

                    return al[2];
                }
            }

            public String ClientLogonId {
                get {
                    if (id != 560) throw new NotSupportedException();

                    return al[13];
                }
            }

            public String PrimaryLogonId {
                get {
                    if (id != 560) throw new NotSupportedException();

                    return al[10];
                }
            }

            public String Message {
                get {
                    return dt.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + MFUt.Formalize(message);
                }

            }
        }

        class XLogon {
            public XEv ev680;
            public XEv ev576;

            public string Account {
                get {
                    String s = "";
                    {
                        s += FmtUt.FormatAccount(ev576.al[1], ev576.al[0]);
                    }
                    if (ev680 != null) {
                        s += " (" + FmtUt.FormatAccount(ev680.al[2], ev680.al[1]) + ")";
                    }
                    return s;
                }
            }

            public string Message {
                get {
                    String s = "";
                    if (ev576 != null) {
                        if (s != "") s += "\r\n---\r\n";
                        s += (ev576.Message);
                    }
                    if (ev680 != null) {
                        if (s != "") s += "\r\n---\r\n";
                        s += (ev680.Message);
                    }
                    return s;
                }
            }

            public XLogon(XEv ev680, XEv ev576) {
                this.ev576 = ev576;
                this.ev680 = ev680;
            }
        }

        class FmtUt {
            public static string FormatAccount(String domain, String user) {
                if (String.IsNullOrEmpty(domain))
                    return String.IsNullOrEmpty(user)
                        ? ""
                        : domain + "\\";
                return domain + "\\" + user;
            }
        }

        SortedDictionary<String, XLogon> dictLogonId = new SortedDictionary<String, XLogon>();

        SortedDictionary<String, XEv> dictObjHandle = new SortedDictionary<String, XEv>();

        Ds.DTDelDataTable dt {
            get {
                return ds1.DTDel;
            }
        }

        private void bLoadfrmSeclog_Click(object sender, EventArgs e) {
            using (WIP wip = WIP.Show(this)) {
                dt.Rows.Clear();

                EventLog ev = new EventLog("Security");
                XEv ev680 = null;
                foreach (EventLogEntry ent in ev.Entries) {
                    if (ent.Source != "Security" || ent.EntryType != EventLogEntryType.SuccessAudit) continue;
                    int id = ((int)ent.InstanceId) & 0x3fffffff;
                    if (id == 564) {
                        //削除されたオブジェクト:
                        //    オブジェクト サーバー:	Security Account Manager [0]
                        //    ハンドル ID:	1429160 [1]
                        //    プロセス ID:	1248 [2]
                        //    イメージ ファイル名:	C:\WINDOWS\system32\lsass.exe [3]

                        Ds.DTDelRow row = dt.NewDTDelRow();
                        row.日時 = ent.TimeGenerated;
                        row.オブジェクト = "";
                        row.クライアントアカウント = "";
                        row.一次アカウント = "";
                        row.Message = new XEv(ent).Message;

                        XEv evObjHandle;
                        if (dictObjHandle.TryGetValue(ent.ReplacementStrings[1], out evObjHandle)) {
                            row.オブジェクト = evObjHandle.ObjectName;
                            row.Message += "\r\n---\r\n" + (evObjHandle.Message);

                            XLogon evLogonId;
                            if (dictLogonId.TryGetValue(evObjHandle.ClientLogonId, out evLogonId)) {
                                row.クライアントアカウント = evLogonId.Account;
                                row.Message += "\r\n---\r\n" + (evLogonId.Message);
                            }
                            if (dictLogonId.TryGetValue(evObjHandle.PrimaryLogonId, out evLogonId)) {
                                row.一次アカウント = evLogonId.Account;
                                row.Message += "\r\n---\r\n" + (evLogonId.Message);
                            }
                        }

                        dt.AddDTDelRow(row);
                    }
                    else if (id == 680) { // アカウント ログオン 
                        //ログオン試行者: MICROSOFT_AUTHENTICATION_PACKAGE_V1_0 [0]
                        //ログオン アカウント:  KU [1]
                        //ソース ワークステーション: DD11 [2]
                        // エラー コード: 0x0 [3]
                        ev680 = new XEv(ent);
                    }
                    else if (id == 576) {
                        //新しいログオンへの特権の割り当て:
                        //    ユーザー名:	[0]
                        //    ドメイン:		[1]
                        //    ログオン ID:		(0x0,0x595A70) [2]
                        //    特権:		SeChangeNotifyPrivilege [3]
                        dictLogonId[ent.ReplacementStrings[2]] = new XLogon(
                            ev680,
                            new XEv(ent)
                            );
                        ev680 = null;
                    }
                    else if (id == 538) {
                        //ユーザーのログオフ:
                        //    ユーザー名:	DD7$ [0]
                        //    ドメイン:		HIRAOKA [1]
                        //    ログオン ID:		(0x0,0xF027A2) [2]
                        //    ログオンの種類:	3 [3]
                        dictLogonId.Remove(ent.ReplacementStrings[2]);
                    }
                    else if (id == 560) {
                        //オブジェクトのオープン:
                        //    オブジェクト サーバー:	Security [0]
                        //    オブジェクトの種類:	File [1]
                        //    オブジェクト名:	C:\Documents and Settings\KU\NetHood [2]
                        //    ハンドル ID:	4840 [3]
                        //    操作 ID:	{0,15252602} [4] [5]
                        //    プロセス ID:	5628 [6]
                        //    イメージ ファイル名:	C:\Proj\MkLnk\Debug\MkLnk.exe [7]
                        //    プライマリ ユーザー名:	KU [8]
                        //    プライマリ ドメイン:	DD11 [9]
                        //    プライマリ ログオン ID:	(0x0,0x101FC5) [10]
                        //    クライアント ユーザー名:	- [11]
                        //    クライアント ドメイン:	- [12]
                        //    クライアント ログオン ID:	- [13]
                        //    アクセス		%SYNCHRONIZE [14]
                        //            %ReadData (または ListDirectory)
                        //    特権		- [15]
                        //    制限された SID 数: 0 [16]
                        dictObjHandle[ent.ReplacementStrings[3]] = new XEv(ent);
                    }
                    else if (id == 562) {
                        //ハンドルのクローズ:
                        //オブジェクト サーバー:	Security [0]
                        //ハンドル ID:	4840 [1]
                        //プロセス ID:	5628 [2]
                        //イメージ ファイル名:	C:\Proj\MkLnk\Debug\MkLnk.exe [3]
                        dictObjHandle.Remove(ent.ReplacementStrings[1]);
                    }
                }

                dt.AcceptChanges();
            }
        }

        class MFUt {
            public static string Formalize(String message) {
                return message.Replace("\r\n\r\n", "\r\n");
            }
        }

        private void TForm_Load(object sender, EventArgs e) {
            foreach (DataGridViewColumn col in gv.Columns)
                if (col.DataPropertyName == "日時")
                    gv.Sort(col, ListSortDirection.Descending);
        }

        private void gv_RowEnter(object sender, DataGridViewCellEventArgs e) {
            DataRowView drv = ((uint)e.RowIndex < (uint)gv.Rows.Count) ? gv.Rows[e.RowIndex].DataBoundItem as DataRowView : null;
            if (drv == null) return;
            Ds.DTDelRow row = drv.Row as Ds.DTDelRow;
            if (row == null) return;

            rtb.Text = row.Message;
        }

        private void bHelpSecpol_Click(object sender, EventArgs e) {
            try {
                Process.Start(Path.Combine(Application.StartupPath, "help\\helpsecpol.html"));
            }
            catch (Exception err) {
                MessageBox.Show(this, "失敗しました。\n\n" + err.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bHelpSacl_Click(object sender, EventArgs e) {
            try {
                Process.Start(Path.Combine(Application.StartupPath, "help\\helpsacl.html"));
            }
            catch (Exception err) {
                MessageBox.Show(this, "失敗しました。\n\n" + err.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
