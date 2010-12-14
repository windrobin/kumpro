using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using LANIPDiscovery.Properties;

namespace LANIPDiscovery {
    public partial class NForm : Form {
        public NForm() {
            InitializeComponent();
        }

        private void tsbEdit_Click(object sender, EventArgs e) {
            Process.Start("notepad.exe", " \"" + fpip + "\"");
        }

        static string fpip { get { return Path.Combine(Application.StartupPath, "ip.txt"); } }

        static string[] IPList {
            get {
                if (File.Exists(fpip))
                    return File.ReadAllLines(fpip, Encoding.Default);
                return new string[0];
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e) {
            lvl.Items.Clear();

            foreach (String ips in IPList) {
                String ip = ips.Trim();
                if (ip.Length == 0) continue;

                ListViewItem lvi = new ListViewItem(ip);
                lvi.SubItems.Add("調査中");
                lvi.SubItems.Add("");
                lvl.Items.Add(lvi);
                lvi.Name = Guid.NewGuid().ToString("N");

                IPAddress addr = IPAddress.Parse(ip);

                Quest a = new Quest(addr, lvi.Name);

                BackgroundWorker bwStat = new BackgroundWorker();
                bwStat.DoWork += new DoWorkEventHandler(bwStat_DoWork);
                bwStat.RunWorkerAsync(a);

                BackgroundWorker bwName = new BackgroundWorker();
                bwName.DoWork += new DoWorkEventHandler(bwName_DoWork);
                bwName.RunWorkerAsync(a);
            }
        }

        void bwName_DoWork(object sender, DoWorkEventArgs e) {
            Quest a = (Quest)e.Argument;

            try {
                XmlDocument xmlo = null;
                for (int retry = 0; xmlo == null && retry < 5; retry++) {
                    try {
                        xmlo = Winsc.Query(new IPEndPoint(a.ip, 137), 1000);
                        break;
                    }
                    catch (Exception) {

                    }
                }
                if (xmlo == null) throw new ApplicationException();
                String names = "";
                foreach (XmlNode el in xmlo.SelectNodes("/wins/response/answer-rrs/answer/name[@name-type='00']/@name")) {
                    names += el.Value.TrimEnd() + ",";
                }

                UpdateText(a.key, 2, names.TrimEnd(','));
            }
            catch (Exception) {
                UpdateText(a.key, 2, "不明");
            }
        }

        void bwStat_DoWork(object sender, DoWorkEventArgs e) {
            Quest a = (Quest)e.Argument;

            String s = Arput.GetMac(a.ip);
            UpdateText(a.key, 1, (s == null) ? "不明" : s);
        }

        delegate void _UpdateText(String key, int i, String val);

        void UpdateText(String key, int i, String val) {
            if (InvokeRequired) { Invoke(new _UpdateText(UpdateText), key, i, val); return; }

            try {
                lvl.Items[key].SubItems[i].Text = val;
            }
            catch (NullReferenceException) { }
        }

        class Quest {
            public IPAddress ip;
            public String key;

            public Quest(IPAddress ip, String key) {
                this.ip = ip;
                this.key = key;
            }
        }

        class Arput {
            [DllImport("iphlpapi.dll", ExactSpelling = true)]
            static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref int PhyAddrLen);

            public static String GetMac(IPAddress ip) {
                byte[] buff = new byte[6];
                int cb = 6;
                int r;
                if (0 == (r = SendARP(IPv4Ut.GetIPv4(ip), 0, buff, ref cb))) {
                    String s = "";
                    for (int x = 0; x < cb; x++) {
                        if (x != 0) s += ":";
                        s += buff[x].ToString("x2");
                    }
                    return s;
                }
                return null;
            }

            class IPv4Ut {
                public static int GetIPv4(IPAddress ip) {
                    if (ip.AddressFamily != AddressFamily.InterNetwork) throw new NotSupportedException();
                    byte[] bin = ip.GetAddressBytes();
                    uint v = bin[3];
                    v <<= 8; v |= bin[2];
                    v <<= 8; v |= bin[1];
                    v <<= 8; v |= bin[0];
                    return (int)v;
                }
            }
        }

        class Winsc {
            static void CbReceived(IAsyncResult ar) {
            }

            // Wiresharkの表示を参考にした
            public static XmlDocument Query(IPEndPoint ip, int millisecs) {
                using (UdpClient client = new UdpClient(ip.AddressFamily)) {
                    client.Connect(ip);

                    Random rand = new Random();
                    ushort seed = (ushort)rand.Next();
                    {
                        MemoryStream os = new MemoryStream();
                        BEW wr = new BEW(os);
                        wr.Write(seed);
                        wr.Write((ushort)0);//Flags: 0x0000 (Name query)
                        wr.Write((ushort)1);//Questions: 1
                        wr.Write((ushort)0);//Answer RRs: 0
                        wr.Write((ushort)0);//Authority RRs: 0
                        wr.Write((ushort)0);//Additional RRs: 0

                        wr.Write((byte)32);
                        wr.Write(Encoding.ASCII.GetBytes("CKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                        wr.Write((byte)0);

                        wr.Write((ushort)0x21);//Type: NBSTAT
                        wr.Write((ushort)0x01);//Class: IN

                        byte[] bin = os.ToArray();
                        if (client.Send(bin, bin.Length) != bin.Length) throw new EndOfStreamException();
                    }

                    {
                        XmlDocument xmlo = new XmlDocument();
                        XmlElement elroot = xmlo.CreateElement("wins");
                        xmlo.AppendChild(elroot);
                        XmlElement elres = xmlo.CreateElement("response");
                        elroot.AppendChild(elres);

                        byte[] bin;
                        {
                            IPEndPoint ipr = new IPEndPoint(0, 0);
                            IAsyncResult ar = client.BeginReceive(CbReceived, null);
                            if (false == ar.AsyncWaitHandle.WaitOne(millisecs, false)) {
                                client.Close();
                            }
                            bin = client.EndReceive(ar, ref ipr);
                        }
                        MemoryStream si = new MemoryStream(bin, false);
                        BER br = new BER(si);
                        ushort rseed = br.ReadUInt16();
                        if (rseed != seed) throw new InvalidDataException();
                        ushort fl = br.ReadUInt16();
                        if (0 == (fl & 0x8000) || 0 != (fl & 15)) throw new InvalidDataException();

                        {
                            int cnt = br.ReadUInt16();
                            for (int x = 0; x < cnt; ) {
                                throw new NotSupportedException();
                            }
                        }

                        int[] acnt = new int[3];
                        acnt[0] = br.ReadUInt16();
                        acnt[1] = br.ReadUInt16();
                        acnt[2] = br.ReadUInt16();

                        for (int t = 0; t < 3; t++) {
                            XmlElement elrrs = null;
                            if (t == 0) elrrs = xmlo.CreateElement("answer-rrs");
                            if (t == 1) elrrs = xmlo.CreateElement("authority-rrs");
                            if (t == 2) elrrs = xmlo.CreateElement("additional-rrs");
                            elres.AppendChild(elrrs);

                            for (int x = 0; x < acnt[t]; x++) {
                                XmlElement ela = xmlo.CreateElement("answer");
                                elrrs.AppendChild(ela);

                                byte cb = br.ReadByte();
                                byte[] name = br.ReadBytes(cb);
                                ela.SetAttribute("raw-name", Encoding.ASCII.GetString(name));
                                byte nodeType = br.ReadByte();
                                ela.SetAttribute("node-type", nodeType.ToString("x2"));

                                int atype = br.ReadUInt16();
                                if (atype != 0x21) throw new NotSupportedException();

                                int aclass = br.ReadUInt16();
                                if (aclass != 1) throw new NotSupportedException();

                                uint attl = br.ReadUInt32();
                                ela.SetAttribute("ttl", attl.ToString());

                                br.ReadUInt16();

                                byte aNamecnt = br.ReadByte();
                                for (int a = 0; a < aNamecnt; a++) {
                                    String aname = Encoding.Default.GetString(br.ReadBytes(15));
                                    byte anty = br.ReadByte();
                                    int afl = br.ReadUInt16();

                                    XmlElement elan = xmlo.CreateElement("name");
                                    ela.AppendChild(elan);
                                    elan.SetAttribute("name", aname);
                                    elan.SetAttribute("name-type", anty.ToString("x2"));
                                    elan.SetAttribute("flags", afl.ToString("x4"));
                                }

                                byte[] mac = br.ReadBytes(6);
                                ela.SetAttribute("unit-id", String.Format("{0:x2}:{1:x2}:{2:x2}:{3:x3}:{4:x2}:{5:x2}"
                                    , mac[0]
                                    , mac[1]
                                    , mac[2]
                                    , mac[3]
                                    , mac[4]
                                    , mac[5]
                                    ));
                            }
                        }

                        return xmlo;
                    }
                }
            }
        }

        private void NForm_Load(object sender, EventArgs e) {
            Location = Screen.PrimaryScreen.WorkingArea.Location + Screen.PrimaryScreen.WorkingArea.Size
                - Size;
        }

        private void lvl_MouseDown(object sender, MouseEventArgs e) { }

        void App(String exe, String arg) {
            foreach (ListViewItem lvi in lvl.SelectedItems) {
                try {
                    ProcessStartInfo psi = new ProcessStartInfo(exe, arg.Replace("*", lvi.Text));
                    psi.UseShellExecute = false;
                    Process.Start(psi);
                }
                catch (Exception err) {
                    MessageBox.Show(this, "失敗しました。\n\n" + err, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                break;
            }
        }

        private void mstscadminToolStripMenuItem_Click(object sender, EventArgs e) { App("mstsc.exe", "/admin /v:*"); }

        private void mstscconsoleToolStripMenuItem_Click(object sender, EventArgs e) { App("mstsc.exe", "/console /v:*"); }

        private void mstscToolStripMenuItem_Click(object sender, EventArgs e) { App("mstsc.exe", " /v:*"); }

        string VncViewer {
            get {
                String fpexe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"RealVNC\vncviewer.exe");

                if (Settings.Default.VNCViewer.Length != 0 && File.Exists(Settings.Default.VNCViewer)) {
                    fpexe = Settings.Default.VNCViewer;
                }

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.FileName = fpexe;
                ofd.Filter = "*.exe|*.exe||";
                ofd.Title = "vnc viewerを選んでください。";

                if (ofd.ShowDialog(this) == DialogResult.OK) {
                    Settings.Default.VNCViewer = fpexe;
                    Settings.Default.Save();
                    return fpexe;
                }

                return "";
            }
        }

        private void vncviewerToolStripMenuItem_Click(object sender, EventArgs e) { App(VncViewer, " *"); }

    }
}