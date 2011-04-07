using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
using System.Web;
using System.Threading;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace OpenyourWebDAV {
    public partial class VForm : Form {
        String fpXML;

        public VForm(String fp) {
            this.fpXML = fp;
            InitializeComponent();
        }

        private void ddbView_ButtonClick(object sender, EventArgs e) {
            if (false) { }
            else if (bViewTile.Checked) { bViewTile_Click(bViewIcon, e); }
            else if (bViewIcon.Checked) { bViewTile_Click(bViewList, e); }
            else if (bViewList.Checked) { bViewTile_Click(bViewDetail, e); }
            else if (bViewDetail.Checked) { bViewTile_Click(bViewTile, e); }
        }

        private void bViewTile_Click(object sender, EventArgs e) {
            bViewTile.Checked = (bViewTile == sender);
            bViewIcon.Checked = (bViewIcon == sender);
            bViewList.Checked = (bViewList == sender);
            bViewDetail.Checked = (bViewDetail == sender);

            if (false) { }
            else if (sender == bViewTile) { lvF.View = View.Tile; }
            else if (sender == bViewIcon) { lvF.View = View.LargeIcon; }
            else if (sender == bViewList) { lvF.View = View.List; }
            else if (sender == bViewDetail) { lvF.View = View.Details; }
        }

        ConnInfo conn = null;
        Uri navi = null, firstNavi = null;

        private void VForm_Load(object sender, EventArgs e) {
            lvF.TileSize = new Size(203, 53);

            conn = new ConnInfo(fpXML);

            Open(null);
        }

        void Open(Uri uri) {
            OpenRes res = DInfo.Open(uri, conn);
            if (res.err == null) {
                lvF.Items.Clear();
                firstNavi = firstNavi ?? navi;
                navi = res.baseUrl;
                tbUrl.Text = navi.ToString();
                foreach (Ent ent in res.Ents) {
                    if (ent.IsSelf) {
                        lvF.Tag = ent;
                        continue;
                    }
                    ListViewItem lvi = new ListViewItem(ent.Name);
                    Int64? length = ent.ContentLength;
                    lvi.SubItems.Add(length.HasValue ? length.Value.ToString("#,##0") : "");
                    lvi.SubItems.Add(ent.IsDir ? "フォルダ" : "ファイル");
                    DateTime? mt = ent.Mt;
                    lvi.SubItems.Add((mt.HasValue) ? mt.Value.ToString() : "");

                    lvi.ImageKey = ent.IsDir ? "D" : "F";
                    lvi.Tag = ent;
                    lvF.Items.Add(lvi);
                }
            }
            else {
                MessageBox.Show(this, "失敗しました：" + res.err.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (tbUrl.Text.Length == 0)
                    tbUrl.Text = (firstNavi = uri).ToString();
            }
        }

        private void lvF_ItemActivate(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvF.SelectedItems) {
                Ent ent = (Ent)lvi.Tag;
                if (ent.IsDir) {
                    Open(ent.FullUrl);
                    break;
                }
                else {
                    Process.Start(ent.FullUrl.ToString());
                }
            }
        }

        class UUt {
            public static Uri GetParent(Uri uri) {
                UriBuilder urib = new UriBuilder(uri);
                String s = urib.Path.TrimEnd('/');
                int i = s.LastIndexOf('/');
                urib.Path = (i < 0) ? "" : s.Substring(0, i + 1);
                return urib.Uri;
            }

            public static Uri CombineFile(Uri uriDir, String href) {
                return new Uri(uriDir.OriginalString.TrimEnd('/') + "/" + href);
            }
        }

        private void bGoUp_Click(object sender, EventArgs e) {
            if (navi == null) return;

            Open(UUt.GetParent(navi));
        }

        private void lvF_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None);
        }

        private void lvF_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (alfp != null) {
                SynchronizationContext.Current.Post(delegate(object state) {
                    UploadThem(alfp, navi);
                    RefreshView();
                }, null);
            }
        }

        class Uploader {
            ConnInfo conn;
            Control win;
            INoticeIO nio;
            WaitHandle cancel;

            public Uploader(ConnInfo conn, Control win, INoticeIO nio, WaitHandle cancel) {
                this.conn = conn;
                this.win = win;
                this.nio = nio;
                this.cancel = cancel;
            }

            delegate DialogResult MsgBoxResDelegate();

            DialogResult policy = DialogResult.None;

            DialogResult QueryOvwr(String fp) {
                if (policy != DialogResult.None)
                    return policy;

                DialogResult r;
                switch (r = (DialogResult)win.Invoke((MsgBoxResDelegate)delegate() { using (IfOvrwForm qf = new IfOvrwForm(fp)) { return qf.ShowDialog(win); } })) {
                    case DialogResult.Yes:
                        return DialogResult.Yes;
                    case DialogResult.No:
                        return DialogResult.No;
                    case DialogResult.OK:
                        return policy = DialogResult.Yes;
                    case DialogResult.Cancel:
                        return policy = DialogResult.No;
                }
                return DialogResult.Cancel;
            }

            public UTRes UploadThem(String[] alfp, Uri baseUri) {
                foreach (String fp in alfp) {
                    if (cancel.WaitOne(0, false)) return UTRes.Cancel;
                    Uri uriTo = UUt.CombineFile(baseUri, Path.GetFileName(fp));
                    using (HeadRes head = DInfo.Head(uriTo, conn)) {
                        head.Dispose();
                        if (0 == (File.GetAttributes(fp) & FileAttributes.Directory)) {
                            if (head.Exists) {
                                switch (QueryOvwr(head.baseUri.ToString())) {
                                    case DialogResult.Yes:
                                        break;
                                    case DialogResult.No:
                                        continue;
                                    case DialogResult.Cancel:
                                        return UTRes.Cancel;
                                }
                            }
                            head.Dispose();
                            using (GenRes uploaded = DInfo.Upload2(uriTo, fp, conn, nio, cancel)) {
                                if (cancel.WaitOne(0, true)) {
                                    using (GenRes rest = DInfo.Delete(uriTo, conn)) {
                                    }
                                }
                            }
                            using (GenRes set = DInfo.SetMt(uriTo, fp, conn)) {

                            }
                        }
                        else {
                            Uri uriDir = UUt.CombineFile(baseUri, Path.GetFileName(fp));

                            if (!head.Exists) {
                                using (GenRes newf = DInfo.NewFolder(uriTo, conn)) {
                                    if (!newf.Success) {
                                        continue;
                                    }
                                }
                            }

                            switch (UploadThem(Directory.GetFileSystemEntries(fp), uriDir)) {
                                case UTRes.Cancel:
                                    return UTRes.Cancel;
                            }
                        }
                    }
                }
                return UTRes.Ok;
            }
        }

        enum UTRes {
            Ok, Cancel,
        }

        private void UploadThem(String[] alfp, Uri baseUri) {
            BackgroundWorker bwIO = new BackgroundWorker();

            ManualResetEvent cancel = new ManualResetEvent(false);
            using (NowUpForm form = new NowUpForm(bwIO, cancel)) {
                Uploader io = new Uploader(conn, this, form, cancel);
                bwIO.DoWork += delegate(object sender, DoWorkEventArgs e) {
                    io.UploadThem(alfp, baseUri);
                };

                form.ShowDialog(this);
            }
        }

        private void RefreshView() {
            Open(navi);
        }

        private void bRefresh_Click(object sender, EventArgs e) {
            RefreshView();
        }

        private void lvF_DragOver(object sender, DragEventArgs e) {
#if false
            ListViewHitTestInfo hit = lvF.HitTest(e.X, e.Y);
            if (hit.Item == null) return;

            ListViewItem lvi = hit.Item;
            if (lvi == null) return;
            DEnt ent = lvi.Tag as DEnt;
            if (ent == null) return;

            e.Effect = e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) && ent.IsDir ? DragDropEffects.All : DragDropEffects.None);
#endif
        }

        private void mDelSel_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "選択したファイル・フォルダを削除しますか？", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            foreach (Ent ent in lvF.SelectedItems.Cast<ListViewItem>().Select(p => (Ent)p.Tag)) {
                using (GenRes killed = DInfo.Delete(ent.FullUrl, conn)) {

                }
            }
            RefreshView();
        }

        private void bNewFolder_Click(object sender, EventArgs e) {
            String name = Interaction.InputBox("フォルダ名称", "", "", -1, -1);
            if (name.Length == 0) return;

            using (GenRes newf = DInfo.NewFolder(UUt.CombineFile(navi, name), conn)) {
                if (!newf.Success) {
                    MessageBox.Show(this, "フォルダの作成に失敗しました：" + newf.err.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            RefreshView();
        }

        private void lvF_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.F2:
                    mRenameSel.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Delete:
                    mDelSel.PerformClick();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Up:
                    if (e.Alt) {
                        bGoUp.PerformClick();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        private void bNavi_Click(object sender, EventArgs e) {
            Open(new Uri(tbUrl.Text));
        }

        private void tstop_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

        }

        private void mRenameSel_Click(object sender, EventArgs e) {
            foreach (Ent ent in lvF.SelectedItems.Cast<ListViewItem>().Select(p => (Ent)p.Tag)) {
                String newname = Interaction.InputBox("新しい名前", "", ent.Name, -1, -1);
                if (newname.Length == 0) break;
                using (GenRes moved = DInfo.Move(ent.FullUrl, new Uri(ent.SelfFullUrl, newname + "/"), conn)) {
                    if (!moved.Success) {
                        MessageBox.Show(this, "フォルダの移動に失敗しました：" + moved.err.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                RefreshView();
                break;
            }
        }

        private void cmsLvf_Opening(object sender, CancelEventArgs e) {
            mRenameSel.Enabled = lvF.SelectedItems.Count == 1;
            mDelSel.Enabled = lvF.SelectedItems.Count >= 1;
        }

        class Cmp1 : System.Collections.IComparer {
            Comparison<Ent> fn;
            int ord;

            public Cmp1(Comparison<Ent> fn, int ord) { this.fn = fn; this.ord = ord; }

            #region IComparer メンバ

            public int Compare(object vx, object vy) {
                Ent x = ((ListViewItem)vx).Tag as Ent;
                Ent y = ((ListViewItem)vy).Tag as Ent;

                bool fx = x != null;
                bool fy = y != null;
                if (!fx || fx != fy) return ord * fx.CompareTo(fy);

                return ord * fn(x, y);
            }

            #endregion
        }

        private void lvF_ColumnClick(object sender, ColumnClickEventArgs e) {
            int ord = (0 == (ModifierKeys & Keys.Shift)) ? 1 : -1;
            switch (e.Column) {
                case 0:
                    lvF.ListViewItemSorter = new Cmp1(delegate(Ent x, Ent y) {
                        int t;
                        t = (x.IsDir ? 0 : 1) - (y.IsDir ? 0 : 1);
                        if (t != 0) return t;

                        return x.Name.CompareTo(y.Name);
                    }, ord);
                    break;
                case 1:
                    lvF.ListViewItemSorter = new Cmp1(delegate(Ent x, Ent y) {
                        int t;
                        t = (x.IsDir ? 0 : 1) - (y.IsDir ? 0 : 1);
                        if (t != 0) return t;

                        t = (x.ContentLength.HasValue ? 1 : 0) - (y.ContentLength.HasValue ? 1 : 0);
                        if (t != 0 || !x.ContentLength.HasValue) return t;

                        return x.ContentLength.Value.CompareTo(y.ContentLength.Value);
                    }, ord);
                    break;
                case 2:
                    lvF.ListViewItemSorter = new Cmp1(delegate(Ent x, Ent y) {
                        int t;
                        t = (x.IsDir ? 0 : 1) - (y.IsDir ? 0 : 1);
                        if (t != 0) return t;

                        return 0;
                    }, ord);
                    break;
                case 3:
                    lvF.ListViewItemSorter = new Cmp1(delegate(Ent x, Ent y) {
                        int t;
                        t = (x.IsDir ? 0 : 1) - (y.IsDir ? 0 : 1);
                        if (t != 0) return t;

                        t = (x.Mt.HasValue ? 1 : 0) - (y.Mt.HasValue ? 1 : 0);
                        if (t != 0 || !x.Mt.HasValue) return t;

                        return x.Mt.Value.CompareTo(y.Mt.Value);
                    }, ord);
                    break;
            }
        }

        private void bGoHome_Click(object sender, EventArgs e) {
            Open(firstNavi);
        }

        private void tbUrl_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                Open(new Uri(tbUrl.Text));
            }
        }
    }

    public class Ent {
        OpenRes ret;
        XmlElement elres;

        public Ent(OpenRes ret, XmlElement elres) {
            this.ret = ret;
            this.elres = elres;
        }

        public bool IsSelf {
            get {
                return FullUrl == ret.baseUrl || String.Compare(FullUrl.OriginalString.TrimEnd('/'), ret.baseUrl.OriginalString.TrimEnd('/'), true) == 0;
            }
        }

        public bool IsDir {
            get {
                XmlNamespaceManager xnm = new XmlNamespaceManager(elres.OwnerDocument.NameTable);
                xnm.AddNamespace("D", "DAV:");

                if (elres.SelectNodes("D:propstat/D:prop/D:resourcetype/D:collection", xnm).Count > 0)
                    return true;

                foreach (XmlText xt in elres.SelectNodes("D:propstat/D:prop/D:getcontenttype/text()", xnm)) {
                    return (xt.Value == "httpd/unix-directory");
                }
                return false;
            }
        }

        public String Name {
            get {
                try {
                    return HttpUtility.UrlDecode(Path.GetFileName(Href.TrimEnd('/')), Encoding.UTF8);
                }
                catch (InvalidOperationException) {
                    return null;
                }
            }
        }

        public Int64? ContentLength {
            get {
                XmlNamespaceManager xnm = new XmlNamespaceManager(elres.OwnerDocument.NameTable);
                xnm.AddNamespace("D", "DAV:");

                foreach (XmlText xt in elres.SelectNodes("D:propstat/D:prop/D:getcontentlength/text()", xnm)) {
                    return Int64.Parse(xt.Value);
                }
                return null;
            }
        }

        public DateTime? Mt {
            get {
                XmlNamespaceManager xnm = new XmlNamespaceManager(elres.OwnerDocument.NameTable);
                xnm.AddNamespace("D", "DAV:");
                xnm.AddNamespace("Z", "urn:schemas-microsoft-com:");

                foreach (XmlText xt in elres.SelectNodes("D:propstat/D:prop/Z:Win32LastModifiedTime/text()", xnm)) {
                    return DateTime.Parse(xt.Value);
                }

                foreach (XmlText xt in elres.SelectNodes("D:propstat/D:prop/D:getlastmodified/text()", xnm)) {
                    return DateTime.Parse(xt.Value);
                }
                return null;
            }
        }

        public String Href {
            get {
                XmlNamespaceManager xnm = new XmlNamespaceManager(elres.OwnerDocument.NameTable);
                xnm.AddNamespace("D", "DAV:");

                return elres.SelectSingleNode("D:href/text()", xnm).Value;
            }
        }

        public Uri RelUrl {
            get {
                return ret.baseUrl.MakeRelativeUri(FullUrl);
            }
        }

        public Uri SelfFullUrl {
            get {
                return new Uri(ret.baseUrl, Href.TrimEnd('/'));
            }
        }

        public Uri FullUrl {
            get {
                return new Uri(ret.baseUrl, Href);
            }
        }
    }

    public class OpenRes {
        public Uri baseUrl;
        public XmlDocument xmlo = new XmlDocument();
        public WebException err = null;

        public IEnumerable<Ent> Ents {
            get {
                XmlNamespaceManager xnm = new XmlNamespaceManager(xmlo.NameTable);
                xnm.AddNamespace("D", "DAV:");

                foreach (XmlElement elres in xmlo.SelectNodes("/D:multistatus/D:response", xnm)) {
                    yield return new Ent(this, elres);
                }
            }
        }
    }

    public class HeadRes : GenRes {
        public bool Exists {
            get {
                return res != null && res.StatusCode == HttpStatusCode.OK;
            }
        }

        public String Name {
            get {
                return HttpUtility.UrlDecode(Path.GetFileName(baseUri.LocalPath.TrimEnd('/')), Encoding.UTF8);
            }
        }
    }

    public class GenRes : IDisposable {
        public Uri baseUri;
        public WebException err = null;
        public HttpWebResponse res = null;

        public bool Success {
            get {
                return res != null && (((int)res.StatusCode / 100) % 10) == 2;
            }
        }

        #region IDisposable メンバ

        public void Dispose() {
            if (res as IDisposable != null) (res as IDisposable).Dispose();
        }

        #endregion
    }

    public class DInfo {
        public static OpenRes Open(Uri uri, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri ?? conn.Url);
            req.Credentials = conn.Auth;
            req.UserAgent = "OpenyourWebDAV/" + Application.ProductVersion;
            req.Method = "PROPFIND";
            req.Headers["Depth"] = "1";
            req.ContentLength = 0;
            req.PreAuthenticate = true;

            OpenRes ret = new OpenRes();
            ret.baseUrl = req.RequestUri;
            try {
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse()) {
                    ret.xmlo.Load(res.GetResponseStream());
                    ret.xmlo.Save(Path.GetTempFileName());
                }
            }
            catch (WebException err) {
                ret.err = err;
            }
            return ret;
        }

        public static HeadRes Head(Uri uri, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "HEAD";
            req.PreAuthenticate = true;

            HeadRes ret = new HeadRes();
            ret.baseUri = uri;
            try {
                using (ret.res = (HttpWebResponse)req.GetResponse()) {
                }
            }
            catch (WebException err) {
                ret.err = err;
            }
            return ret;
        }

        static string UA { get { return "OpenyourWebDAV/" + Application.ProductVersion; } }

        public static GenRes Upload2(Uri uri, String fp, ConnInfo conn, INoticeIO nio, WaitHandle cancel) {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri ?? conn.Url);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "PUT";
            req.PreAuthenticate = true;
            using (FileStream si = File.OpenRead(fp)) {
                req.ContentLength = si.Length;
                req.ContentType = "application/octet-stream";
                req.SendChunked = true;
                req.Pipelined = true;
                req.AllowWriteStreamBuffering = true;
                using (Stream os = req.GetRequestStream()) {
                    byte[] bin = new byte[4096];
                    while (true) {
                        nio.Notice(fp, si.Position, si.Length);
                        if (cancel.WaitOne(0, false)) break;

                        int r = si.Read(bin, 0, bin.Length);
                        if (r < 1) break;
                        os.Write(bin, 0, r);
                    }
                }

                GenRes ret = new GenRes();
                ret.baseUri = req.RequestUri;
                try {
                    ret.res = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException err) {
                    ret.err = err;
                }
                return ret;
            }
        }

        public static GenRes Upload(Uri uri, String fp, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri ?? conn.Url);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "PUT";
            req.PreAuthenticate = true;
            using (FileStream si = File.OpenRead(fp)) {
                req.ContentLength = si.Length;
                req.ContentType = "application/octet-stream";
                req.SendChunked = true;
                using (Stream os = req.GetRequestStream()) {
                    byte[] bin = new byte[4096];
                    while (true) {
                        int r = si.Read(bin, 0, bin.Length);
                        if (r < 1) break;
                        os.Write(bin, 0, r);
                    }
                }

                GenRes ret = new GenRes();
                ret.baseUri = req.RequestUri;
                try {
                    ret.res = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException err) {
                    ret.err = err;
                }
                return ret;
            }
        }

        public static GenRes SetMt(Uri uri, String fp, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri ?? conn.Url);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "PROPPATCH";
            req.PreAuthenticate = true;

            MemoryStream si = new MemoryStream();
            {
                XmlDocument xmlo = new XmlDocument();
                XmlElement elroot = xmlo.CreateElement("D", "propertyupdate", "DAV:");
                xmlo.AppendChild(elroot);
                XmlElement elset = xmlo.CreateElement("D", "set", "DAV:");
                elroot.AppendChild(elset);
                XmlElement elprop = xmlo.CreateElement("D", "prop", "DAV:");
                elset.AppendChild(elprop);
                // http://code.google.com/p/sem-fs/source/browse/trunk/milton-api-patch/src/docs/vista/proppatch?spec=svn218&r=218
                XmlElement elmt = xmlo.CreateElement("Z", "Win32LastModifiedTime", "urn:schemas-microsoft-com:");
                elprop.AppendChild(elmt);
                elmt.AppendChild(xmlo.CreateTextNode(File.GetLastWriteTimeUtc(fp).ToString("r")));

                StreamWriter wr = new StreamWriter(si);
                xmlo.Save(wr);
            }

            si.Position = 0;
            {
                req.ContentLength = si.Length;
                req.ContentType = "text/xml; charset=\"utf-8\"";
                req.SendChunked = true;
                using (Stream os = req.GetRequestStream()) {
                    byte[] bin = new byte[4096];
                    while (true) {
                        int r = si.Read(bin, 0, bin.Length);
                        if (r < 1) break;
                        os.Write(bin, 0, r);
                    }
                }

                GenRes ret = new GenRes();
                ret.baseUri = req.RequestUri;
                try {
                    ret.res = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException err) {
                    ret.err = err;
                }
                return ret;
            }
        }

        public static GenRes Delete(Uri uri, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "DELETE";
            req.PreAuthenticate = true;

            GenRes ret = new GenRes();
            try {
                ret.res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException err) {
                ret.err = err;
            }
            return ret;
        }

        public static GenRes NewFolder(Uri uri, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "MKCOL";
            req.PreAuthenticate = true;

            GenRes ret = new GenRes();
            try {
                ret.res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException err) {
                ret.err = err;
            }
            return ret;
        }

        public static GenRes Move(Uri uriFrm, Uri uriTo, ConnInfo conn) {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uriFrm);
            req.Credentials = conn.Auth;
            req.UserAgent = UA;
            req.Method = "MOVE";
            req.PreAuthenticate = true;
            req.Headers["Destination"] = HttpUtility.UrlPathEncode(uriTo.ToString());
            req.Headers["Overwrite"] = "F";

            GenRes ret = new GenRes();
            try {
                ret.res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException err) {
                ret.err = err;
            }
            return ret;
        }
    }

    public class ConnInfo {
        XmlDocument xmlo = new XmlDocument();

        public ConnInfo(String fpXML) {
            xmlo.Load(fpXML);
        }

        public ICredentials Auth {
            get {
                return new NetworkCredential(User, Pass);
            }
        }

        public String Pass {
            get {
                foreach (XmlAttribute att in xmlo.SelectNodes("/OpenyourWebDAV/@P")) {
                    return att.Value;
                }
                return null;
            }
        }

        public String User {
            get {
                foreach (XmlAttribute att in xmlo.SelectNodes("/OpenyourWebDAV/@U")) {
                    return att.Value;
                }
                return null;
            }
        }

        public Uri Url {
            get {
                foreach (XmlAttribute att in xmlo.SelectNodes("/OpenyourWebDAV/@url")) {
                    return new Uri(att.Value);
                }
                return null;
            }
        }
    }

    public interface INoticeIO {
        void Notice(String fp, Int64 cur, Int64 max);
    }
}
