using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using AFPt2;
using System.Diagnostics;
using System.Globalization;

namespace FTP4AFP {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        Thread t1;


        private void bStop_Click(object sender, EventArgs e) {
            CloseIt();
        }

        private void bStart_Click(object sender, EventArgs e) {
            l = new TcpListener(new IPEndPoint(IPAddress.Any, int.Parse(tbFTPPort.Text)));
            l.Start();
            t1 = new Thread(T1);
            t1.Start();
            afp = new IPEndPoint(Dns.GetHostAddresses(tbAFP.Text)[0], int.Parse(tbAFPPort.Text));
            bStart.Enabled = false;
            bStop.Enabled = true;
        }

        TcpListener l;
        List<TcpClient> tcps = new List<TcpClient>();
        List<TcpListener> tcpls = new List<TcpListener>();
        IPEndPoint afp;

        void T1(object state) {
            try {
                while (true) {
                    TcpClient tcp = l.AcceptTcpClient();
                    lock (tcps) { tcps.Add(tcp); }
                    Thread t2 = new Thread(T2);
                    t2.Start(tcp);
                }
            }
            catch (SocketException err) {
                Debug.Assert(err.SocketErrorCode == System.Net.Sockets.SocketError.Interrupted);
            }
        }

        void LOG(string body) {
            if (InvokeRequired) { Invoke((Action<String>)LOG, body); return; }
            tbAFP.AppendText(body.Replace("\r\n", "\n").Replace("\n", "\r\n") + "\r\n");
            return;
        }

        class DataConn {
            TcpListener tcpl;
            bool isBin = true;
            IPEndPoint tcpep;

            internal void TypeI() {
                isBin = true;
            }

            internal void TypeA() {
                isBin = false;
            }

            internal void Pasv(EndPoint endPoint) {
                CloseIt();
                tcpl = new TcpListener(new IPEndPoint(((IPEndPoint)endPoint).Address, 0));
                tcpl.Start();
            }

            public void CloseIt() {
                if (tcpl != null) {
                    tcpl.Stop();
                    tcpl = null;
                }
                tcpep = null;
            }

            public String GetPasv() {
                IPEndPoint ipe = (IPEndPoint)tcpl.LocalEndpoint;
                IPAddress ip = ipe.Address;
                byte[] bin = ip.GetAddressBytes();
                int port = ipe.Port;

                return "" + bin[0] + "," + bin[1] + "," + bin[2] + "," + bin[3] + "," + (port >> 8) + "," + (port & 255) + "";
            }

            public void SendData(byte[] bin) {
                SendSt(new MemoryStream(bin, false));
            }

            public String Mode { get { return isBin ? "BINARY" : "ASCII"; } }

            public void SendSt(Stream si) {
                if (tcpl != null) {
                    using (TcpClient tcp = tcpl.AcceptTcpClient()) {
                        Stream st = tcp.GetStream();
                        byte[] bin = new byte[4000];
                        while (true) {
                            int r = si.Read(bin, 0, bin.Length);
                            if (r < 1) break;
                            st.Write(bin, 0, r);
                        }
                    }
                }
                else if (tcpep != null) {
                    using (TcpClient tcp = new TcpClient()) {
                        tcp.Connect(tcpep);
                        Stream st = tcp.GetStream();
                        byte[] bin = new byte[4000];
                        while (true) {
                            int r = si.Read(bin, 0, bin.Length);
                            if (r < 1) break;
                            st.Write(bin, 0, r);
                        }
                    }
                }
                else throw new ApplicationException("No PASV/PORT commands are issued.");
            }

            public void Port(string p) {
                CloseIt();
                String[] cols = p.Split(',');
                if (cols.Length != 6) throw new ArgumentException(p);

                tcpep = new IPEndPoint(new IPAddress(new byte[] { byte.Parse(cols[0]), byte.Parse(cols[1]), byte.Parse(cols[2]), byte.Parse(cols[3]) }), int.Parse(cols[4]) * 256 + int.Parse(cols[5]));
            }

            public void RecvSt(Stream os) {
                if (tcpl != null) {
                    using (TcpClient tcp = tcpl.AcceptTcpClient()) {
                        Stream st = tcp.GetStream();
                        byte[] bin = new byte[4000];
                        while (true) {
                            int r = st.Read(bin, 0, bin.Length);
                            if (r < 1) break;
                            os.Write(bin, 0, r);
                        }
                    }
                }
                else if (tcpep != null) {
                    using (TcpClient tcp = new TcpClient()) {
                        tcp.Connect(tcpep);
                        Stream st = tcp.GetStream();
                        byte[] bin = new byte[4000];
                        while (true) {
                            int r = st.Read(bin, 0, bin.Length);
                            if (r < 1) break;
                            os.Write(bin, 0, r);
                        }
                    }
                }
                else throw new ApplicationException("No PASV/PORT commands are issued.");
            }
        }

        // https://developer.apple.com/library/mac/documentation/networking/Reference/AFP_Reference/Reference/reference.html#//apple_ref/c/func/FPOpenDir

        void T2(object state) {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Encoding enc = Encoding.GetEncoding(932);

            ManualResetEvent evExit = new ManualResetEvent(false);

            using (TcpClient tcp = (TcpClient)state) {
                NetworkStream st = tcp.GetStream();
                StreamWriter wr = new StreamWriter(st, Encoding.UTF8);
                StreamReader rr = new StreamReader(st, Encoding.UTF8, false);

                using (MyDSI3 comm = new MyDSI3(afp)) {
                    TransmitRes res = comm.Transmit(new DSIGetStatus());
                    if (res.pack.IsResponse && res.pack.ErrorCode == 0) {
                        GetSrvrInfoPack pack = new GetSrvrInfoPack(res.br);
                        if (pack.AFPVersionsList.Contains("AFP2.2")) {

                        }
                        else { LOG("AFP2.2に非対応"); return; }
                    }
                    else { LOG("AFP接続に失敗"); return; }
                }
                String U = String.Empty;
                String P = String.Empty;
                IDir root = new DisconnetedRoot();
                IDir pwd = root;
                DataConn dc = new DataConn();
                using (MyDSI3 comm = new MyDSI3(afp)) {
                    Ut.WriteRes(wr, 220, "FTP4AFP/" + Application.ProductVersion);
                    while (true) {
                        try {
                            String row = rr.ReadLine();
                            if (row == null) break;
                            Match M;
                            M = Regex.Match(row, "^USER\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                U = M.Groups["a"].Value;
                                Ut.WriteRes(wr, 331, "Proceed to password.");
                                continue;
                            }
                            M = Regex.Match(row, "^PASS\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                P = M.Groups["a"].Value;
                                Uta a = new Uta();
                                root = pwd = a.Login(comm, cc, U, P);
                                Ut.WriteRes(wr, 230, "User logged in, proceed.");
                                continue;
                            }
                            if (row.TrimEnd() == "PWD") {
                                MLoc m = MLoc.Ut.Get(pwd);
                                Ut.WriteRes(wr, 257, "\"" + m.UnixPath + "\" is current directory.");
                                continue;
                            }
                            M = Regex.Match(row, "^TYPE\\s+(?<a>A|I)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String mode = M.Groups["a"].Value.ToUpperInvariant();
                                if (mode == "I") dc.TypeI();
                                if (mode == "A") dc.TypeA();
                                Ut.WriteRes(wr, 200, "Ok.");
                                continue;
                            }
                            if (row.TrimEnd() == "PASV") {
                                dc.Pasv(tcp.Client.LocalEndPoint);

                                Ut.WriteRes(wr, 227, "Entering Passive Mode (" + dc.GetPasv() + ")");
                                continue;
                            }
                            M = Regex.Match(row, "^PORT\\s+(?<a>\\d+,\\d+,\\d+,\\d+,\\d+,\\d+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                dc.Port(M.Groups["a"].Value);
                                Ut.WriteRes(wr, 200, "PORT command successful.");
                                continue;
                            }
                            if (row.TrimEnd() == "LIST") {
                                StringWriter ww = new StringWriter();
                                foreach (IEnt o in pwd.GetEnts()) {
                                    bool isDir = o is IDir;
                                    ww.WriteLine("{0}rw-r--r--    1 0        0      {1,10} {2,-12} {3}"
                                        , isDir ? "d" : "-"
                                        , isDir ? "0" : o.Size.ToString()
                                        , DUt.Format(o.Mt)
                                        , o.Name
                                        );
                                }
                                // http://blog.livedoor.jp/kumagai_nori/archives/51660940.html
                                // http://ash.jp/net/ftp_command.htm
                                // http://www.atmarkit.co.jp/ait/articles/0307/11/news001.html

                                // http://www.atmarkit.co.jp/fnetwork/rensai/netpro10/ftp-responsecode.html

                                // http://maruo.dyndns.org:81/hidesoft/hidesoft_2/x17565.html
                                // http://www.nsftools.com/tips/MSFTP.htm#dir
                                Ut.WriteRes(wr, 150, "Opening " + dc.Mode + " mode data connection for LIST");

                                dc.SendData(wr.Encoding.GetBytes(ww.ToString()));

                                Ut.WriteRes(wr, 226, "Transfer complete");
                                continue;
                            }
                            M = Regex.Match(row, "^MLSD(\\s+(?<a>.+))?\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    IDir newwd = (cwd.Length == 0) ? pwd : TravUt.Cwd(pwd, cwd);

                                    StringWriter ww = new StringWriter();
                                    foreach (IEnt o in newwd.GetEnts()) {
                                        bool isDir = o is IDir;
                                        ww.WriteLine("modify={0};perm={1};size={2};type={3}; {4}"
                                            , o.Mt.HasValue ? String.Format("{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}", o.Mt.Value) : ""
                                            , isDir ? "cdelmp" : "dlrw"
                                            , o.Size
                                            , isDir ? "dir" : "file"
                                            , o.Name
                                            );
                                    }

                                    Ut.WriteRes(wr, 150, "Opening " + dc.Mode + " mode data connection for MLSD");

                                    dc.SendData(wr.Encoding.GetBytes(ww.ToString()));

                                    Ut.WriteRes(wr, 226, "Transfer complete");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 501, err.Message);
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^CWD\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    IDir newwd = TravUt.Cwd(pwd, cwd);
                                    pwd = newwd;
                                    Ut.WriteRes(wr, 250, "Directory successfully changed.");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, err.Message);
                                    continue;
                                }
                            }
                            if (row.TrimEnd() == "CDUP") {
                                IDir newwd = pwd.ParentDir;
                                if (newwd == null) {
                                    Ut.WriteRes(wr, 250, "We are already at root directory.");
                                    continue;
                                }
                                else {
                                    pwd = newwd;
                                    Ut.WriteRes(wr, 250, "Directory successfully changed.");
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^OPTS\\s+UTF8\\s+ON\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                Ut.WriteRes(wr, 200, "It is in UTF8 mode.");
                                wr = new StreamWriter(st, Encoding.UTF8);
                                rr = new StreamReader(st, Encoding.UTF8, false);
                                continue;
                            }
                            M = Regex.Match(row, "^OPTS\\s+UTF8\\s+OFF\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                Ut.WriteRes(wr, 200, "It is in " + enc.BodyName + " mode.");
                                wr = new StreamWriter(st, enc);
                                rr = new StreamReader(st, enc, false);
                                continue;
                            }
                            if (row.TrimEnd() == "FEAT") {
                                Ut.WriteRes(wr, 211, "Features:| UTF8| MLST modify*;perm*;size*;type*;| END".Replace("|", "\n"));
                                continue;
                            }
                            M = Regex.Match(row, "^RETR\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    IEnt source = TravUt.Find(pwd, cwd, false);

                                    if (source is ICanDL) {
                                        using (Stream si = ((ICanDL)source).OpenRead()) {
                                            Ut.WriteRes(wr, 150, "Opening " + dc.Mode + " mode data connection for " + cwd);
                                            dc.SendSt(si);
                                        }
                                    }
                                    else {
                                        Ut.WriteRes(wr, 550, "We can't get \"" + cwd + "\".");
                                        continue;
                                    }

                                    Ut.WriteRes(wr, 226, "Transfer complete");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, err.Message);
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^STOR\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    using (Stream os = TravUt.Createf(pwd, cwd, cc)) {
                                        Ut.WriteRes(wr, 150, "Opening " + dc.Mode + " mode data connection for " + cwd);
                                        os.SetLength(0);
                                        dc.RecvSt(os);
                                    }

                                    Ut.WriteRes(wr, 226, "Transfer complete");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, err.Message);
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^APPE\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    using (Stream os = TravUt.Createf(pwd, cwd, cc)) {
                                        Ut.WriteRes(wr, 150, "Opening " + dc.Mode + " mode data connection for " + cwd);
                                        os.Seek(0, SeekOrigin.End);
                                        dc.RecvSt(os);
                                    }

                                    Ut.WriteRes(wr, 226, "Transfer complete");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, err.Message);
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^MKD\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    TravUt.CreateDir(pwd, cwd);

                                    Ut.WriteRes(wr, 250, "\"" + cwd + "\" created successfully.");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, "\"" + cwd + "\": Unable to create directory. \n" + err.Message);
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^RMD\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    TravUt.RMDir(pwd, cwd);

                                    Ut.WriteRes(wr, 250, "\"" + cwd + "\" removed successfully.");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, "\"" + cwd + "\": Unable to remove directory. \n" + err.Message);
                                    continue;
                                }
                            }
                            M = Regex.Match(row, "^DELE\\s+(?<a>.+)\\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                            if (M.Success) {
                                String cwd = M.Groups["a"].Value;
                                try {
                                    TravUt.Dele(pwd, cwd, cc);

                                    Ut.WriteRes(wr, 250, "\"" + cwd + "\" removed successfully.");
                                    continue;
                                }
                                catch (EntNotFoundException err) {
                                    Ut.WriteRes(wr, 550, "\"" + cwd + "\": Unable to remove directory. \n" + err.Message);
                                    continue;
                                }
                            }

                            Ut.WriteRes(wr, 500, "NotImpl Error.");
                        }
                        catch (Exception err) {
                            if (tcp.Connected) {
                                Ut.WriteRes(wr, 500, err.ToString());
                            }
                            else break;
                        }
                    }
                }
                tcp.Close();
            }
        }

        class DUt {
            static string[] Mons { get { return ",Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".Split(','); } }

            public static string Format(DateTime? p) {
                if (p.HasValue) {
                    DateTime dt = p.Value;
                    if (dt.Year == DateTime.Today.Year) {
                        return String.Format("{1} {0:dd} {0:HH}:{0:mm}", dt, Mons[dt.Month]);
                    }
                    return String.Format("{1} {0:dd} {0:yyyy}", dt, Mons[dt.Month]);
                }
                return "Jan 01 1970";
            }
        }

        class Uta {
            public MacRoot Login(MyDSI3 comm, ConConf cc, String U, String P) {
                {
                    TransmitRes res = comm.Transmit(new DSIOpenSession());
                    if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

                    }
                    else { throw new ApplicationException("DSIOpenSessionに失敗"); }
                }
                {
                    TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPLogin_Cleartext_Password()
                        .WithUserName(U)
                        .WithPasswd(P)
                        .WithAFPVersion("AFP2.2"))
                        );
                    if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

                    }
                    else { throw new ApplicationException("FPLoginに失敗"); }
                }
                {
                    TransmitRes res1 = comm.Transmit(new DSICommand().WithRequestPayload(new FPGetSrvrParms()));
                    if (res1.pack.IsResponse && res1.pack.ErrorCode == 0) {

                    }
                    else { throw new ApplicationException("FPGetSrvrParmsに失敗"); }

                    GetSrvrParmsPack pack = new GetSrvrParmsPack(res1.br);

                    return new MacRoot(pack, comm, cc);
                }
            }
        }

        ConConf cc = new ConConf();

        class WaitThreads {
            List<Thread> threads = new List<Thread>();

            public Thread Add(Thread t) {
                threads.Add(t);
                return t;
            }

            public void WaitAll() {
                foreach (Thread t in threads)
                    t.Join();
            }
        }

        class Ut {
            public static void WriteRes(StreamWriter wr, int resc, String body) {
                String[] rows = body.Replace("\r\n", "\n").Split('\n');
                for (int y = 0; y < rows.Length; y++) {
                    String row = String.Format("{0:000}{1}{2}"
                        , resc
                        , (y + 1 == rows.Length) ? " " : "-"
                        , rows[y]
                        );
                    byte[] bin = wr.Encoding.GetBytes(row + "\r\n");
                    wr.BaseStream.Write(bin, 0, bin.Length);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            rbNo_CheckedChanged(sender, e);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            CloseIt();
        }

        private void CloseIt() {
            List<IDisposable> ary = new List<IDisposable>();
            if (l != null && l.Server.IsBound) l.Stop();
            lock (tcps) { ary.AddRange(tcps.ToArray()); }
            foreach (IDisposable o in ary) { try { o.Dispose(); } catch (Exception) { } }

            bStart.Enabled = true;
            bStop.Enabled = false;
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e) {
            if (rbNo.Checked) { cc.EnumRes = false; }
            if (rbPrepend.Checked) { cc.EnumRes = true; cc.ResPrefix = true; }
            if (rbAppend.Checked) { cc.EnumRes = true; cc.ResPrefix = false; }
        }
    }

    public class ConConf {
        public bool EnumRes = false, IfAvail = true;
        public bool ResPrefix = false;

        public string GetResName(string fn) {
            if (ResPrefix) return "._" + fn;
            return fn + ".AFP_Resource";
        }

        public MacfNam ParseName(String s1) {
            MacfNam m = new MacfNam();
            m.Name = s1;
            m.IsRes = false;
            if (EnumRes) {
                if (ResPrefix) {
                    if (s1.StartsWith("._")) {
                        m.Name = s1.Substring(2);
                        m.IsRes = true;
                    }
                    else {
                        // data
                    }
                }
                else {
                    if (s1.EndsWith(".AFP_Resource")) {
                        m.Name = s1.Substring(0, s1.Length - 13);
                        m.IsRes = true;
                    }
                    else {
                        // data
                    }
                }
            }
            else {
                // data
            }
            return m;
        }
    }
    public class MacfNam {
        public String Name = String.Empty;
        public bool IsRes = false;
    }

    public class MacVol : IDir {
        VolStruc vol;
        MyDSI3 comm;
        OpenVolPack pack2;
        IDir parentDir;
        ConConf cc;

        public MacVol(VolStruc vol, MyDSI3 comm, ConConf cc, IDir parentDir) {
            this.vol = vol;
            this.comm = comm;
            this.cc = cc;
            this.parentDir = parentDir;
        }

        public ushort VolID { get { return pack2.Ent.VolID.Value; } }
        public uint DirID { get { return 2; } }

        #region IDir メンバ

        public IEnumerable<IEnt> GetEnts() {
            OpenIt();

            return CUt.GetEnts(MLoc.Ut.GetVol(this), comm, cc, this);
        }

        #endregion

        #region IEnt メンバ

        public string Name { get { return vol.VolName; } }
        public string RealName { get { return vol.VolName; } }

        public long Size { get { return -1; } }

        public DateTime? Mt { get { return null; } }

        #endregion

        #region IEnt メンバ

        public IDir ParentDir {
            get { return parentDir; }
        }

        #endregion

        public void OpenIt() {
            if (pack2 == null) {
                TransmitRes res2 = comm.Transmit(new DSICommand().WithRequestPayload(new FPOpenVol()
                    .WithVolumeName(vol.VolName)
                    .WithBitmap(AfpVolumeBitmap.kFPVolIDBit | AfpVolumeBitmap.kFPVolNameBit)
                    ));
                if (res2.pack.IsResponse && res2.pack.ErrorCode == 0) {

                }
                else { throw new DSIException(res2.pack.ErrorCode, res2.pack); }

                pack2 = new OpenVolPack(res2.br);
            }
        }

        #region IDir メンバ

        #endregion

        #region IDir メンバ


        public IEnt FindReal(string name) {
            return CUt.FindReal(name, comm, cc, this);
        }

        #endregion

        #region IDir メンバ


        #endregion

        #region IDir メンバ


        public void CreateDirHere(string loc) {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPCreateDir()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithPath(PUt.CombineRaw(m.RawPath, loc))
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        #endregion

        #region IDir メンバ


        public void RMDirMe() {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPDelete()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithPath(m.RawPath)
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        #endregion

        #region IDir メンバ


        public void CreatefHere(string loc) {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPCreateFile()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithPath(PUt.CombineRaw(m.RawPath, loc))
                .WithSoftCreate()
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        #endregion
    }

    public class RmdException : ApplicationException {
        public RmdException(String message)
            : base(message) {

        }
    }

    public class MkdException : ApplicationException {
        public MkdException(String message)
            : base(message) {

        }
    }

    public class StorException : ApplicationException {
        public StorException(String message)
            : base(message) {

        }
        public StorException(String message, Exception inner)
            : base(message, inner) {

        }
    }

    public class DeleException : ApplicationException {
        public DeleException(String message)
            : base(message) {

        }
        public DeleException(String message, Exception inner)
            : base(message, inner) {

        }
    }

    public class MacEnt : IEnt, ICanDL, ICanUP, ICanDele {
        FileParameters parm;
        MyDSI3 comm;
        ConConf cc;
        IDir parentDir;
        bool data;

        public MacEnt(FileParameters parm, bool data, MyDSI3 comm, ConConf cc, IDir parentDir) {
            this.parm = parm;
            this.data = data;
            this.comm = comm;
            this.cc = cc;
            this.parentDir = parentDir;
        }

        #region IEnt メンバ

        public string Name {
            get { return data ? parm.LongName : cc.GetResName(parm.LongName); }
        }
        public string RealName {
            get { return parm.LongName; }
        }

        public long Size {
            get {
                return data
              ? (parm.DataForkSize.HasValue ? parm.DataForkSize.Value : -1L)
              : (parm.ResourceForkSize.HasValue ? parm.ResourceForkSize.Value : -1L);
            }
        }

        public DateTime? Mt {
            get { return parm.MT; }
        }

        #endregion

        #region IEnt メンバ


        public IDir ParentDir {
            get { return parentDir; }
        }

        #endregion

        #region ICanDL メンバ

        public Stream OpenRead() {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res1 = comm.Transmit(new DSICommand().WithRequestPayload(new FPOpenFork()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithFlag((byte)(data ? 0x00 : 0x80))
                .WithAccessMode(AfpAccessMode.Read)
                .WithBitmap(data ? AfpFileBitmap.DataForkLength : AfpFileBitmap.ResourceForkLength)
                .WithPath(m.RawPath)
            ));
            if (res1.pack.IsResponse && res1.pack.ErrorCode == 0) {

            }
            else { throw new DSIException(res1.pack.ErrorCode, res1.pack); }

            OpenForkPack pack = new OpenForkPack(res1.br);

            return new MacSt(comm, data, pack);
        }

        #endregion

        #region ICanUP メンバ

        public Stream OpenWrite(bool isRes) {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res1 = comm.Transmit(new DSICommand().WithRequestPayload(new FPOpenFork()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithFlag((byte)(!isRes ? 0x00 : 0x80))
                .WithAccessMode(AfpAccessMode.Write)
                .WithBitmap(!isRes ? AfpFileBitmap.DataForkLength : AfpFileBitmap.ResourceForkLength)
                .WithPath(m.RawPath)
            ));
            if (res1.pack.IsResponse && res1.pack.ErrorCode == 0) {

            }
            else { throw new DSIException(res1.pack.ErrorCode, res1.pack); }

            OpenForkPack pack = new OpenForkPack(res1.br);

            return new MacSt(comm, !isRes, pack);
        }

        #endregion

        #region ICanDele メンバ

        public void DeletefMe(bool isRes) {
            MLoc m = MLoc.Ut.Get(this);
            if (isRes) {
                using (Stream st = OpenWrite(true)) {
                    st.SetLength(0);
                }
            }
            else {
                TransmitRes res1 = comm.Transmit(new DSICommand().WithRequestPayload(new FPDelete()
                    .WithVolumeID(m.VolID)
                    .WithDirectoryID(m.DirID)
                    .WithPath(m.RawPath)
                ));
                if (res1.pack.IsResponse && res1.pack.ErrorCode == 0) {

                }
                else { throw new DSIException(res1.pack.ErrorCode, res1.pack); }
            }
        }

        #endregion
    }

    public class MacSt : Stream {
        bool data;
        OpenForkPack pack;
        MyDSI3 comm;
        bool open;
        Int64 pos;

        public MacSt(MyDSI3 comm, bool data, OpenForkPack pack) {
            this.comm = comm;
            this.data = data;
            this.pack = pack;
            this.open = true;
        }

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return true; }
        }

        public override bool CanWrite {
            get { return true; }
        }

        public override void Flush() {
        }

        public override long Length {
            get { return data ? pack.Parms.DataForkSize.Value : pack.Parms.ResourceForkSize.Value; }
        }

        public override long Position {
            get {
                return pos;
            }
            set {
                pos = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count) {
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPRead()
                .WithOForkRefNum(pack.Fork)
                .WithOffset(Convert.ToUInt32(pos))
                .WithReqCount(Convert.ToUInt32(count))
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else if (res.pack.ErrorCode == (int)AFPt2.DSIException.ResultCode.kFPEOFErr) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
            int cb = Convert.ToInt32(res.pack.TotalDataLength);
            Buffer.BlockCopy(res.pack.Payload, 0, buffer, 0, cb);
            pos += cb;
            return cb;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            switch (origin) {
                case SeekOrigin.Begin: pos = offset; break;
                case SeekOrigin.Current: pos += offset; break;
                case SeekOrigin.End: pos = Length + offset; break;
            }
            return pos;
        }

        public override void SetLength(long value) {
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPSetForkParms()
                .WithOForkRefNum(pack.Fork)
                .WithForkLen64(value)
                .WithFileBitmap(data ? AfpFileBitmap.DataForkLength : AfpFileBitmap.ResourceForkLength)
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        public override void Write(byte[] buffer, int offset, int count) {
            TransmitRes res = comm.Transmit(new DSIWrite().WithRequestPayload(new FPWrite()
                .WithStartEndFlag(false)
                .WithOForkRefNum(pack.Fork)
                .WithOffset(Convert.ToInt32(pos))
                .WithReqCount(count)
                .WithForkData(buffer, offset)
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
            pos += count;
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (open) {
                TransmitRes res2 = comm.Transmit(new DSICommand().WithRequestPayload(new FPCloseFork()
                    .WithFork(pack.Fork)
                ));
                open = false;
            }
        }
    }

    public class MLoc {
        public ushort VolID;
        public uint DirID;
        public String MacPath;
        public String MacVol;

        public String RawPath { get { return MacPath.Replace(":", "\0"); } }
        public String UnixPath {
            get {
                String s = "";
                if (MacVol.Length != 0)
                    s += "/" + MacVol;
                if (MacPath.Length != 0)
                    s += "/" + MacPath;
                if (s.Length == 0)
                    s += "/";
                return s.Replace(":", "/");
            }
        }

        public class Ut {
            public static MLoc Get(IEnt p) {
                MLoc m = new MLoc();
                m.MacPath = String.Empty;
                m.MacVol = String.Empty;
                while (p != null) {
                    if (p is MacRoot)
                        break;
                    if (p is MacVol) {
                        MacVol vol = (MacVol)p;
                        vol.OpenIt();
                        m.VolID = vol.VolID;
                        m.DirID = vol.DirID;
                        m.MacVol = p.RealName;
                    }
                    else {
                        m.MacPath = ((String)(p.RealName + ":" + m.MacPath)).TrimEnd(':');
                    }

                    p = p.ParentDir;
                }
                return m;
            }

            public static MLoc GetVol(MacVol vol) {
                MLoc m = new MLoc();
                vol.OpenIt();
                m.MacPath = "";
                m.MacVol = vol.RealName;
                m.VolID = vol.VolID;
                m.DirID = vol.DirID;
                return m;
            }
        }
    }

    public class CUt {
        public static IEnumerable<IEnt> GetEnts(MLoc m, MyDSI3 comm, ConConf cc, IDir self) {
            for (uint x = 0; ; ) {
                TransmitRes res1 = comm.Transmit(new DSICommand().WithRequestPayload(new FPEnumerate()
                    .WithPath(m.RawPath)
                    .WithStartIndex(Convert.ToUInt16(1U + x))
                    .WithVolumeID(m.VolID)
                    .WithDirectoryID(m.DirID)
                    .WithFileBitmap(AfpFileBitmap.DataForkLength | AfpFileBitmap.ResourceForkLength | AfpFileBitmap.LongName | AfpFileBitmap.NodeID | AfpFileBitmap.ModificationDate)
                    .WithDirectoryBitmap(AfpDirectoryBitmap.NodeID | AfpDirectoryBitmap.LongName | AfpDirectoryBitmap.ModificationDate)
                    ));
                if (res1.pack.ErrorCode == -5018) break;
                if (res1.pack.IsResponse && res1.pack.ErrorCode == 0) {
                }
                else { throw new DSIException(res1.pack.ErrorCode, res1.pack); }

                EnumeratePack pack = new EnumeratePack(res1.br);

                foreach (FileParameters ent in pack.Ents) {
                    if (ent.IsDirectory) {
                        yield return new MacDir(ent, comm, cc, self);
                    }
                    else {
                        yield return new MacEnt(ent, true, comm, cc, self);
                        if (cc.EnumRes && ((ent.ResourceForkSize.HasValue && ent.ResourceForkSize.Value > 0) || !cc.IfAvail)) yield return new MacEnt(ent, false, comm, cc, self);
                    }
                }

                if (pack.ActualCount == 0) break;

                x += pack.ActualCount;
            }
        }

        public static IEnt FindReal(string name, MyDSI3 comm, ConConf cc, IDir self) {
            MLoc m = MLoc.Ut.Get(self);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPGetFileDirParms()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithFileBitmap(AfpFileBitmap.DataForkLength | AfpFileBitmap.ResourceForkLength | AfpFileBitmap.LongName | AfpFileBitmap.NodeID)
                .WithDirectoryBitmap(AfpDirectoryBitmap.NodeID | AfpDirectoryBitmap.LongName)
                .WithPath(PUt.CombineRaw(m.RawPath, name))
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else if (res.pack.ErrorCode == (int)AFPt2.DSIException.ResultCode.kFPObjectNotFound) {
                return null;
            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);

            GetFileDirParmsPack pack = new GetFileDirParmsPack(res.br);

            FileParameters ent = pack.Parms;

            if (ent.IsDirectory) {
                return new MacDir(ent, comm, cc, self);
            }
            else {
                return new MacEnt(ent, true, comm, cc, self);
            }
        }
    }

    public class MacDir : IDir {
        FileParameters parm;
        MyDSI3 comm;
        IDir parentDir;
        ConConf cc;

        public MacDir(FileParameters parm, MyDSI3 comm, ConConf cc, IDir parentDir) {
            this.parm = parm;
            this.comm = comm;
            this.cc = cc;
            this.parentDir = parentDir;
        }

        #region IDir メンバ

        public IEnumerable<IEnt> GetEnts() {
            MLoc m = MLoc.Ut.Get(this);
            return CUt.GetEnts(m, comm, cc, this);
        }

        #endregion

        #region IEnt メンバ

        public string Name {
            get { return parm.LongName; }
        }
        public string RealName {
            get { return parm.LongName; }
        }

        public long Size {
            get { return -1; }
        }

        public DateTime? Mt {
            get { return parm.MT; }
        }

        #endregion

        #region IEnt メンバ


        public IDir ParentDir {
            get { return parentDir; }
        }

        #endregion

        #region IDir メンバ


        #endregion

        #region IDir メンバ

        public IEnt FindReal(string name) {
            return CUt.FindReal(name, comm, cc, this);
        }

        #endregion

        #region IDir メンバ


        #endregion

        #region IDir メンバ


        public void CreateDirHere(string loc) {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPCreateDir()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithPath(PUt.CombineRaw(m.RawPath, loc))
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        #endregion

        #region IDir メンバ


        public void RMDirMe() {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPDelete()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithPath(m.RawPath)
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        #endregion

        #region IDir メンバ


        public void CreatefHere(string loc) {
            MLoc m = MLoc.Ut.Get(this);
            TransmitRes res = comm.Transmit(new DSICommand().WithRequestPayload(new FPCreateFile()
                .WithVolumeID(m.VolID)
                .WithDirectoryID(m.DirID)
                .WithPath(PUt.CombineRaw(m.RawPath, loc))
                .WithSoftCreate()
            ));
            if (res.pack.IsResponse && res.pack.ErrorCode == 0) {

            }
            else throw new DSIException(res.pack.ErrorCode, res.pack);
        }

        #endregion
    }

    public class MacRoot : IDir {
        GetSrvrParmsPack pack;
        MyDSI3 comm;
        ConConf cc;

        public MacRoot(GetSrvrParmsPack pack, MyDSI3 comm, ConConf cc) {
            this.pack = pack;
            this.comm = comm;
            this.cc = cc;
        }

        #region IDir メンバ

        public IEnumerable<IEnt> GetEnts() {
            foreach (VolStruc vol in pack.Volumes) {
                yield return new MacVol(vol, comm, cc, this);
            }
        }

        #endregion

        #region IEnt メンバ

        public string Name { get { return "(Root)"; } }
        public string RealName { get { return "(Root)"; } }

        public long Size { get { return -1; } }

        public DateTime? Mt { get { return null; } }

        #endregion

        #region IEnt メンバ

        public IDir ParentDir {
            get { return null; }
        }

        #endregion


        #region IDir メンバ

        #endregion

        #region IDir メンバ


        public IEnt FindReal(string name) {
            foreach (IEnt o in GetEnts()) {
                if (o.RealName.Equals(name)) return o;
            }
            return null;
        }

        #endregion

        #region IDir メンバ


        #endregion

        #region IDir メンバ


        public void CreateDirHere(string loc) {
            throw new MkdException("We can't make a volume.");
        }

        #endregion

        #region IDir メンバ


        public void RMDirMe() {
            throw new RmdException("We can't remove a volume.");
        }

        #endregion

        #region IDir メンバ


        public void CreatefHere(string loc) {
            throw new StorException("We can't write a volume.");
        }

        #endregion
    }

    public interface IDir : IEnt {
        IEnumerable<IEnt> GetEnts();
        IEnt FindReal(String name);
        void CreateDirHere(String loc);
        void RMDirMe();
        void CreatefHere(String loc);
    }

    public interface ICanDL {
        Stream OpenRead();
    }
    public interface ICanUP {
        Stream OpenWrite(bool isRes);
    }
    public interface ICanDele {
        void DeletefMe(bool isRes);
    }

    public interface IEnt {
        String Name { get; }
        String RealName { get; }
        Int64 Size { get; }
        DateTime? Mt { get; }
        IDir ParentDir { get;}
    }

    public class DisconnetedRoot : IDir {
        #region IDir メンバ

        public IEnumerable<IEnt> GetEnts() {
            yield break;
        }

        #endregion

        #region IEnt メンバ

        public string Name {
            get { return "(Root)"; }
        }
        public string RealName {
            get { return "(Root)"; }
        }

        public long Size {
            get { return -1; }
        }

        public DateTime? Mt {
            get { return null; }
        }

        public IDir ParentDir {
            get { return null; }
        }

        #endregion

        #region IDir メンバ


        #endregion

        #region IDir メンバ


        public IEnt FindReal(string name) {
            return null;
        }

        #endregion

        #region IDir メンバ


        #endregion

        #region IDir メンバ


        public void CreateDirHere(string loc) {
            throw new MkdException("The method or operation is not implemented.");
        }

        #endregion

        #region IDir メンバ


        public void RMDirMe() {
            throw new RmdException("The method or operation is not implemented.");
        }

        #endregion

        #region IDir メンバ


        public void CreatefHere(string loc) {
            throw new StorException("The method or operation is not implemented.");
        }

        #endregion
    }

    public class EntNotFoundException : ApplicationException {
        public EntNotFoundException(String message)
            : base(message) {

        }
    }

    public class PUt {
        public static String GetAbs(IDir parent, String rel) {
            return GetAbs(MLoc.Ut.Get(parent).UnixPath, rel);
        }
        public static String GetAbs(String pwd, String rel) {
            if (rel.Length == 0 || rel == ".") {
                return pwd;
            }
            if (rel.StartsWith("/")) {
                return GetAbs("/", rel.Substring(1));
            }
            if (rel == "..") {
                return GetAbs(pwd, "../");
            }
            if (rel.StartsWith("./")) {
                return GetAbs(pwd, rel.Substring(2));
            }
            if (rel.StartsWith("../")) {
                int i = pwd.LastIndexOf('/');
                if (i <= 0) return "/";
                return GetAbs(pwd.Substring(0, i), rel.Substring(3));
            }
            int p = rel.IndexOf('/');
            String part = (p < 0) ? rel : rel.Substring(0, p);
            if (p < 0)
                return pwd + "/" + part;
            return GetAbs(pwd + "/" + part, rel.Substring(p + 1));
        }

        public static string CombineRaw(String lv, String rv) {
            String s = lv;
            if (s.Length != 0) s += "\0";
            s = rv.StartsWith("\0") ? rv : s + rv;
            return s;
        }
    }

    public class TravUt {
        public static IDir Cwd(IDir parent, String rel) {
            return Find(parent, rel, true) as IDir;
        }

        public static IEnt Find(IDir parent, String rel, bool onlyDir) {
            if (rel.Length == 0 || rel == ".") {
                return parent;
            }
            if (rel.StartsWith("/")) {
                return Find(GetRoot(parent), rel.Substring(1), onlyDir);
            }
            if (rel == "..") {
                return parent.ParentDir;
            }
            if (rel.StartsWith("./")) {
                return Find(parent, rel.Substring(2), onlyDir);
            }
            if (rel.StartsWith("../")) {
                return Find(parent.ParentDir, rel.Substring(3), onlyDir);
            }
            int p = rel.IndexOf('/');
            String part = (p < 0) ? rel : rel.Substring(0, p);
            foreach (IEnt o in parent.GetEnts()) {
                if (o.Name.Equals(part)) {
                    if (o is IDir) {
                        if (p < 0)
                            return o;
                        return Find(o as IDir, rel.Substring(p + 1), onlyDir);
                    }
                    else if (onlyDir) {
                        throw new EntNotFoundException("We knew \"" + rel + "\" is a file.");
                    }
                    else {
                        if (p < 0)
                            return o;
                        throw new EntNotFoundException("We knew \"" + rel + "\" is a file.");
                    }
                }
            }
            throw new EntNotFoundException("We don't find \"" + rel + "\".");
        }

        public static IDir GetRoot(IDir parent) {
            if (parent != null) {
                while (parent.ParentDir != null) {
                    parent = parent.ParentDir;
                }
            }
            return parent;
        }

        public static void RMDir(IDir self, String unixPath) {
            if (unixPath == null) {
                self.RMDirMe();
                return;
            }
            if (unixPath.StartsWith("/")) {
                RMDir(TravUt.GetRoot(self), unixPath.Substring(1));
                return;
            }
            int p = unixPath.IndexOf('/');
            String s1 = (p < 0) ? unixPath : unixPath.Substring(0, p);
            String s2 = (p < 0) ? null : unixPath.Substring(1 + p);
            if (s1 == ".") {
                RMDir(self, s2);
                return;
            }
            if (s1 == "..") {
                RMDir(self.ParentDir, s2);
                return;
            }
            IEnt o = self.FindReal(s1);
            if (o is IDir) {
                RMDir((IDir)o, s2);
                return;
            }
            else if (o != null) {
                throw new RmdException("We knew \"" + o.Name + "\" is a file.");
            }
            throw new RmdException("We can't remove \"" + unixPath + "\".");
        }

        public static void CreateDir(IDir self, String unixPath) {
            if (unixPath == null) return; // Already exists
            if (unixPath.StartsWith("/")) {
                CreateDir(GetRoot(self), unixPath.Substring(1));
                return;
            }
            int p = unixPath.IndexOf('/');
            String s1 = (p < 0) ? unixPath : unixPath.Substring(0, p);
            String s2 = (p < 0) ? null : unixPath.Substring(1 + p);
            if (s1 == ".") {
                CreateDir(self, s2);
                return;
            }
            if (s1 == "..") {
                CreateDir(self.ParentDir, s2);
                return;
            }
            for (int t = 0; t < 2; t++) {
                IEnt o = self.FindReal(s1);
                if (o is IDir) {
                    CreateDir((IDir)o, s2);
                    return;
                }
                else if (o != null) {
                    throw new MkdException("We knew \"" + o.Name + "\" is a file.");
                }

                self.CreateDirHere(s1);

                if (s2 == null) return;
            }
            throw new MkdException("We can't create \"" + unixPath + "\".");
        }

        public static Stream Createf(IDir self, String unixPath, ConConf cc) {
            if (unixPath == null) throw new StorException("unixPath is null");
            if (unixPath.StartsWith("/")) {
                return Createf(GetRoot(self), unixPath.Substring(1), cc);
            }
            int p = unixPath.IndexOf('/');
            String s1 = (p < 0) ? unixPath : unixPath.Substring(0, p);
            String s2 = (p < 0) ? null : unixPath.Substring(1 + p);
            if (s1 == ".") {
                return Createf(self, s2, cc);
            }
            if (s1 == "..") {
                return Createf(self.ParentDir, s2, cc);
            }
            if (s2 == null) {
                MacfNam m2 = cc.ParseName(s1);
                try {
                    self.CreatefHere(m2.Name);
                }
                catch (DSIException err) {
                    if (err.ErrorCode == (int)AFPt2.DSIException.ResultCode.kFPObjectExists) {
                    }
                    else throw new StorException("Failed", err);
                }
                IEnt o2 = self.FindReal(m2.Name);
                if (o2 is ICanUP) {
                    return ((ICanUP)o2).OpenWrite(m2.IsRes);
                }
                else throw new StorException("We can't write to \"" + o2.Name + "\".");
            }
            IEnt o = self.FindReal(s1);
            if (o is IDir) {
                return Createf((IDir)o, s2, cc);
            }
            else if (o != null) {
                throw new StorException("We knew \"" + o.Name + "\" is a file.");
            }
            throw new StorException("We can't be here for \"" + o.Name + "\".");
        }

        public static Object Dele(IDir self, String unixPath, ConConf cc) {
            if (unixPath == null) throw new DeleException("unixPath is null");
            if (unixPath.StartsWith("/")) {
                return Dele(GetRoot(self), unixPath.Substring(1), cc);
            }
            int p = unixPath.IndexOf('/');
            String s1 = (p < 0) ? unixPath : unixPath.Substring(0, p);
            String s2 = (p < 0) ? null : unixPath.Substring(1 + p);
            if (s1 == ".") {
                return Dele(self, s2, cc);
            }
            if (s1 == "..") {
                return Dele(self.ParentDir, s2, cc);
            }
            if (s2 == null) {
                MacfNam m2 = cc.ParseName(s1);
                IEnt o2 = self.FindReal(m2.Name);
                if (o2 is ICanDele) {
                    ((ICanDele)o2).DeletefMe(m2.IsRes);
                    return null;
                }
                else throw new DeleException("We can't delete \"" + o2.Name + "\".");
            }
            IEnt o = self.FindReal(s1);
            if (o is IDir) {
                return Dele((IDir)o, s2, cc);
            }
            else if (o != null) {
                throw new DeleException("We knew \"" + o.Name + "\" is a file.");
            }
            throw new DeleException("We can't be here for \"" + o.Name + "\".");
        }
    }
}
