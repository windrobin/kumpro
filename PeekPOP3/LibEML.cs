/*
 * Created by SharpDevelop.
 * User: DD3user
 * Date: 2006/01/13
 * Time: 14:34
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace HDD.LibEML {
    public interface TextDecode {
        string decodeText(byte[] bin);
    }
    public interface TextEncode {
        byte[] encodeText(string text);
    }
    public interface BinaryDecode {
        byte[] decodeBinary(string text);
        bool preserveCRLF { get;}
    }
    public interface BinaryEncode {
        string encodeBinary(byte[] bin);
    }
    public interface CharsetEncoder : TextDecode, TextEncode {

    }
    public interface TransferEncoder : BinaryDecode, BinaryEncode {

    }
    class LL1 {
        public int x, cx;
        public string input;

        public LL1(string input) {
            x = 0;
            cx = input.Length;
            this.input = input;
        }
        public bool EOF {
            get { return !(x < cx); }
        }
        public bool compare(string text, bool cursor) {
            int n = text.Length;
            if (x + n <= cx) {
                if (string.Compare(input.Substring(x, text.Length), text) == 0) {
                    if (cursor)
                        x += text.Length;
                    return true;
                }
            }
            return false;
        }
        public bool compareNoCase(string text, bool cursor) {
            int n = text.Length;
            if (x + n <= cx) {
                if (string.Compare(input.Substring(x, text.Length), text, true) == 0) {
                    if (cursor)
                        x += text.Length;
                    return true;
                }
            }
            return false;
        }
        public char read() {
            char c = input[x];
            x++;
            return c;
        }
        public string readStr(int n) {
            string res = null;
            if (x + n <= cx) {
                res = input.Substring(x, n);
                x += n;
            }
            return res;
        }
        public string peekStr(int n) {
            string res = null;
            if (x + n <= cx) {
                res = input.Substring(x, n);
            }
            return res;
        }
    }
    public class TransferEncoding {
        public TransferEncoder getEncoding(string transfer) {
            if (transfer == null || transfer == "" || string.Compare(transfer, "7bit", true) == 0 || string.Compare(transfer, "8bit", true) == 0) {
                return new transfer7bit();
            }
            else if (string.Compare(transfer, "base64", true) == 0) {
                return new transferBase64();
            }
            else if (string.Compare(transfer, "quoted-printable", true) == 0) {
                return new transferQuotedPrintable();
            }
            return null;
        }
    }
    public class CharsetEncoding {
        public CharsetEncoder getEncoding(string charSet) {
            if (charSet == null) {
                return new charsetDefunct(Encoding.ASCII);
            }
            else if (string.Compare(charSet, "iso-2022-jp", true) == 0) {
                return new charsetDefunct(Encoding.GetEncoding("iso-2022-jp"));
            }
            else if (string.Compare(charSet, "us-ascii", true) == 0) {
                return new charsetDefunct(Encoding.ASCII);
            }
            else if (string.Compare(charSet, "shift_jis", true) == 0) {
                return new charsetDefunct(Encoding.GetEncoding(932));
            }
            else if (string.Compare(charSet, "utf-8", true) == 0) {
                return new charsetDefunct(Encoding.UTF8);
            }
            else if (string.Compare(charSet, "iso-8859-1", true) == 0) {
                return new charsetDefunct(Encoding.GetEncoding("iso-8859-1"));
            }
            return null;
        }
    }
    public class SubjectEncode {
        public string encode(string text) {
            int dbcs = 0, sbcs = 0;
            foreach (char c in text) {
                if (c < 0x100) {
                    sbcs++;
                }
                else {
                    dbcs++;
                }
            }

            if (dbcs != 0) {
                Encoding enc = Encoding.GetEncoding("iso-2022-jp");
                return "=?ISO-2022-JP?B?" + Convert.ToBase64String(enc.GetBytes(text)) + "?=";
            }
            else {
                return text;
            }
        }

        public static readonly SubjectEncode current = new SubjectEncode();
    }
    public class SubjectEncodeNew {
        enum CharClass {
            ansi,
            iso2022jp,
        }

        class CLAUZ {
            public string text = "";
            public CharClass cc = CharClass.ansi;
        }

        public string encode(string text) {
            CLAUZ clauz = new CLAUZ();
            ArrayList al = new ArrayList();
            foreach (char c in text) {
                CharClass ccNew = clauz.cc;
                if (c < 0x100) {
                    ccNew = CharClass.ansi;
                }
                else {
                    ccNew = CharClass.iso2022jp;
                }

                if (clauz.cc != ccNew) {
                    al.Add(clauz);
                    clauz = new CLAUZ();
                    clauz.cc = ccNew;
                }
                clauz.text += c;
            }
            al.Add(clauz);

            Encoding enc = Encoding.GetEncoding("iso-2022-jp");
            string res = "";
            foreach (CLAUZ cl in al) {
                switch (cl.cc) {
                    case CharClass.ansi:
                        res += cl.text;
                        break;
                    case CharClass.iso2022jp:
                        res += "=?ISO-2022-JP?B?" + Convert.ToBase64String(enc.GetBytes(cl.text)) + "?=";
                        break;
                }
            }
            return res;
        }

        public static readonly SubjectEncode current = new SubjectEncode();
    }
    public class SubjectDecode {
        public static string decode(string text) {
            LL1 rr = new LL1(text);
            StringBuilder res = new StringBuilder();
            while (!rr.EOF) {
                if (rr.compare("=?", true)) {
                    res.Append(parseEncode(rr));
                }
                else {
                    res.Append(rr.read());
                }
            }
            return res.ToString();
        }

        static string parseEncode(LL1 rr) {
            Encoding enc = null;

            if (rr.compareNoCase("ISO-2022-JP?B?", true))
                enc = Encoding.GetEncoding("iso-2022-jp");
            else if (rr.compareNoCase("SHIFT_JIS?B?", true))
                enc = Encoding.GetEncoding(932);
            else if (rr.compareNoCase("UTF-8?B?", true))
                enc = Encoding.UTF8;

            if (enc != null) {
                string bEnc = "";
                while (true) {
                    if (rr.compare("?=", true)) {
                        break;
                    }
                    string text = rr.readStr(4);
                    if (text == null)
                        throw new ArgumentException("不正なSubjectEncoding", rr.peekStr(10));
                    bEnc += text;
                }
                return enc.GetString(Convert.FromBase64String(bEnc));
            }
            else {
                throw new NotSupportedException("不明な文字コードでした");
            }
        }
    }
    public class charsetDefunct : CharsetEncoder {
        Encoding encoder;

        public charsetDefunct(Encoding encoder) {
            this.encoder = encoder;
        }
        public string decodeText(byte[] bin) {
            return encoder.GetString(bin);
        }
        public byte[] encodeText(string text) {
            return encoder.GetBytes(text);
        }
    }
    public class transfer7bit : TransferEncoder {
        public byte[] decodeBinary(string text) {
            return Encoding.ASCII.GetBytes(text);
        }
        public string encodeBinary(byte[] bin) {
            return Encoding.ASCII.GetString(bin);
        }
        public bool preserveCRLF { get { return true; } }
    }
    public class transferBase64 : TransferEncoder {
        public byte[] decodeBinary(string text) {
            StringBuilder s = new StringBuilder();
            foreach (String row in text.Replace("\r\n", "\n").Split('\n')) {
                s.Append(row);
            }
            return Convert.FromBase64String(s.ToString());
        }
        public string encodeBinary(byte[] bin) {
            int x, cx = Convert.ToInt32(bin.Length);
            StringBuilder text = new StringBuilder(cx * 3 / 2);
            for (x = 0; x < cx; x += 57) {
                int r = Math.Min(57, cx - x);
                text.Append(Convert.ToBase64String(bin, x, r));
                text.Append("\r\n");
            }
            return text.ToString();
        }
        public bool preserveCRLF { get { return false; } }
    }
    public class transferQuotedPrintable : TransferEncoder {
        public byte[] decodeBinary(string text) {
            MemoryStream bin = new MemoryStream();
            LL1 rr = new LL1(text);
            while (!rr.EOF) {
                char c = rr.read();
                if (c == '=') {
                    string t = rr.readStr(2);
                    if (t == null) bin.WriteByte((byte)'=');
                    else bin.WriteByte(Convert.ToByte(t, 16));
                }
                else {
                    bin.WriteByte((byte)c);
                }
            }
            return bin.ToArray();
        }
        public string encodeBinary(byte[] bin) {
            StringBuilder res = new StringBuilder();
            int cx = bin.Length;
            for (int x = 0; x < cx; x++) {
                int r = bin[x];
                if (r < 0) break;

                if ((r != '\"' && r != '?' && r != '=') && (r == '\r' || r == '\n' || r == '\t' || 0x21 <= r && r <= 0x7E)) {
                    res.Append((char)r);
                }
                else {
                    res.AppendFormat("={0:X2}", r);
                }
            }
            return res.ToString();
        }
        public bool preserveCRLF { get { return true; } }
    }
    public class ContentDispositionContainer {
        public IDictionary m;

        public ContentDispositionContainer(string input) {
            m = ContentTypeDecode.decode(input);
        }
        public override string ToString() {
            return ContentTypeEncode.encode(m);
        }
        public string getValue(string key) {
            return (string)m[key];
        }
        public void setValue(string key, string val) {
            m[key] = val;
        }
        public string major {
            get {
                return Util.Nz(m[""], null);
            }
        }
        public string primary {
            get { return Util.Nz(getValue(""), null); }
            set { setValue("", value); }
        }
        public string filename {
            get { return Util.Nz(getValue("filename"), null); }
            set { setValue("filename", value); }
        }
    }
    public class ContentTransferEncodingContainer {
        public IDictionary m;

        public ContentTransferEncodingContainer(string input) {
            m = ContentTypeDecode.decode(input);
        }
        public override string ToString() {
            return ContentTypeEncode.encode(m);
        }
        public string getValue(string key) {
            return (string)m[key];
        }
        public string major {
            get {
                return Util.Nz(m[""], null);
            }
        }
    }
    public class ContentTypeContainer {
        public IDictionary m;

        public ContentTypeContainer(string input) {
            m = ContentTypeDecode.decode(input);
        }
        public override string ToString() {
            return ContentTypeEncode.encode(m);
        }
        public string getValue(string key) {
            return (string)m[key];
        }
        public void setValue(string key, string val) {
            m[key] = val;
        }
        public string major {
            get {
                string t = Util.Nz(getValue(""), "");
                int r = t.IndexOf('/');
                if (r < 0) return t;
                return t.Substring(0, r);
            }
        }
        public string minor {
            get {
                string t = Util.Nz(getValue(""), "");
                int r = t.IndexOf('/');
                if (r < 0) return "";
                return t.Substring(r + 1);
            }
        }
        public string boundary {
            get { return Util.Nz(getValue("boundary"), null); }
            set { setValue("boundary", value); }
        }
        public string charSet {
            get { return Util.Nz(getValue("charset"), null); }
        }
        public string primary {
            get { return Util.Nz(getValue(""), null); }
            set { setValue("", value); }
        }
        public string name {
            get { return Util.Nz(getValue("name"), null); }
            set { setValue("name", value); }
        }
    }
    class ContentTypeEncode2 {
        public static string encode(IDictionary m) {
            StringBuilder res = new StringBuilder();

            object primary = m[""];
            if (primary != null) res.Append(primary);
            foreach (DictionaryEntry e in m) {
                string k = (string)e.Key;
                string v = (string)e.Value;
                if (k == "") continue;

                res.Append("; ");
                byte[] bin = Encoding.GetEncoding("iso-2022-jp").GetBytes(v);
                string vv = "";
                foreach (byte c in bin) {
                    if (c == '.' || ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z')) {
                        vv += (char)c;
                    }
                    else {
                        vv += string.Concat("%", c.ToString("X2"));
                    }
                }

                res.AppendFormat("{0}*=iso-2022-jp'ja'{1}", k, vv);
            }

            return res.ToString();
        }
    }
    class ContentTypeEncode {
        public static string encode(IDictionary m) {
            string prefix = "";
            string suffix = null;
            foreach (DictionaryEntry e in m) {
                string k = (string)e.Key;
                string v = (string)e.Value;

                if (k.Length == 0) {
                    prefix = v;
                }
                else {
                    if (suffix == null) {
                        suffix = "";
                    }
                    else {
                        suffix += "; ";
                    }
                    suffix += string.Format("{0}=\"{1}\"", k, SubjectEncode.current.encode(v));
                }
            }
            if (suffix == null) {
                return prefix;
            }
            else {
                return prefix + "; " + suffix;
            }
        }
    }
    class ContentTypeDecode {
        public static IDictionary decode(string input) {
            SortedList m = CollectionsUtil.CreateCaseInsensitiveSortedList();
            foreach (string kv in input.Split(';')) {
                int t = kv.IndexOf('=');
                if (t < 0) {
                    m[""] = kv;
                }
                else {
                    string key = kv.Substring(0, t).Trim();
                    string value = kv.Substring(t + 1).Trim();
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                        value = value.Substring(1, value.Length - 2);
                    m[key] = value;
                }
            }
            return m;
        }
    }

    public class JAEncode {
        public static byte[] encodeOuter(string input) {
            byte[] xxx = Encoding.GetEncoding("iso-2022-jp").GetBytes(input);
            return xxx;
        }

        public static string encodeInner(string input) {
            byte[] xxx = Encoding.GetEncoding("iso-2022-jp").GetBytes(input);
            string text = Convert.ToBase64String(xxx);

            return string.Format("=?ISO-2022-JP?B?{0}?=", text);
        }
    }
    public class MailAddr {
        public string person;
        public string domain;

        public MailAddr(string address) {
            int r = address.IndexOf('@');
            if (r < 0)
                throw new ArgumentException("Invalid e-mail address");
            person = address.Substring(0, r);
            domain = address.Substring(r + 1);
        }

        public override string ToString() {
            return person + "@" + domain;
        }
    }
    class Rex {
        public static Regex NameAndAddr = new Regex("(.+?)\\s+<(.+?)>");
        public static Regex Addr = new Regex("<(.+?)>");
    }
    public class People : IEnumerable {
        public readonly ArrayList al = new ArrayList();

        public People() { }
        public People(Person[] ppl) {
            al.AddRange(ppl);
        }

        public IEnumerator GetEnumerator() {
            return al.GetEnumerator();
        }

        public void parse(string text) {
            string[] ppls = text.Split(',');
            foreach (string ppl in ppls) {
                string addr = ppl.Trim();
                if (addr.Length == 0)
                    continue;
                Match m;
                if (null != (m = Rex.NameAndAddr.Match(addr)) && m.Success) {
                    al.Add(new Person(m.Groups[2].Value, SubjectDecode.decode(m.Groups[1].Value)));
                }
                else if (null != (m = Rex.Addr.Match(addr)) && m.Success) {
                    al.Add(new Person(m.Groups[1].Value));
                }
                else {
                    al.Add(new Person(SubjectDecode.decode(addr)));
                }
            }
        }
        public bool contains(string mailAddr) {
            foreach (Person person in al) {
                if (string.Compare(person.address, mailAddr, true) == 0)
                    return true;
            }
            return false;
        }
        public void addPerson(Person p) {
            al.Add(p);
        }
        public override string ToString() {
            StringBuilder text = new StringBuilder();
            int n = 0;
            for (int x = 0; x < al.Count; x++) {
                string name = ((Person)al[x]).ToString();
                if (n != 0) text.Append(',');
                if (name.Length > 0) {
                    text.Append(name);
                    n++;
                }
            }
            return text.ToString();
        }
    }
    public class Person {
        public string name = null, address = null;

        public Person(string address) {
            this.address = address;
        }
        public Person(string address, string name) {
            this.address = address;
            this.name = name;
        }
        public override string ToString() {
            bool nameAvail = name != null;
            bool addrAvail = address != null;
            string res = "";
            if (nameAvail && addrAvail) {
                res = string.Format("{0} <{1}>", SubjectEncode.current.encode(name), address);
            }
            else if (nameAvail) {
                res = string.Format("{0}", SubjectEncode.current.encode(name), address);
            }
            else if (addrAvail) {
                res = string.Format("<{1}>", SubjectEncode.current.encode(name), address);
            }
            else {

            }
            return res;
        }

        public string encode() {
            if (name == null) {
                return address;
            }
            return string.Format("{0} <{1}>", SubjectEncode.current.encode(name), address);
        }

        public static string encode(ArrayList al) {
            string text = "";

            for (int x = 0; x < al.Count; x++) {
                if (x != 0) text += ", ";
                text += ((Person)al[x]).encode();
            }
            return text;
        }
    }
    public class EML_Writer {
        public string mailBody = "";

        public DateTime mailDate = DateTime.Now;
        public Person mailFrom = null;
        public ArrayList mailTo = new ArrayList();
        public string mailSubject = "TEST";

        public MemoryStream createEML() {
            MemoryStream os = new MemoryStream();

            SortedList atts = CollectionsUtil.CreateCaseInsensitiveSortedList();
            atts["Date"] = mailDate.ToUniversalTime().ToString("r");
            atts["From"] = mailFrom.encode();
            atts["X-Mailer"] = "HDD LibEML 1.0";
            atts["MIME-Version"] = "1.0";
            atts["To"] = Person.encode(mailTo);
            atts["Subject"] = JAEncode.encodeInner(mailSubject);
            atts["Content-Type"] = "text/plain; charset=ISO-2022-JP";
            atts["Content-Transfer-Encoding"] = "7bit";

            Encoding enc = Encoding.ASCII;

            string header = "";
            foreach (DictionaryEntry e in atts) {
                header += string.Format("{0}: {1}\r\n", e.Key, e.Value);
            }
            header += "\r\n";

            byte[] xxx = enc.GetBytes(header);
            os.Write(xxx, 0, xxx.Length);

            xxx = JAEncode.encodeOuter(mailBody);
            os.Write(xxx, 0, xxx.Length);

            os.Position = 0;
            return os;
        }
    }

    public class EML_Media {
        public SortedList headerMap = CollectionsUtil.CreateCaseInsensitiveSortedList();
        public People mlTo = null;
        public People mlCC = null;
        public People mlFrom = null;
        public People mlReturnPath = null;
        public People mlReferences = null;
        public People mlInReplyTo = null;
        public People mlMessageID = null;
        public string mlSubject = null;
        public string mlDate = null;
        public ContentTypeContainer mlContentType = null;
        public ContentTransferEncodingContainer mlContentTransferEncoding = null;
        public ContentDispositionContainer mlContentDisposition = null;
        public StringBuilder mlBody = null;
        public MemoryStream mlBinary = null;

        public CharsetEncoding charSetEnc = new CharsetEncoding();
        public TransferEncoding transferEnc = new TransferEncoding();

        public int readHeaders(TextReader rr) {
            headerMap.Clear();
            string curlin = null;
            int n = 0;
            while (true) {
                string lin = rr.ReadLine();
                if (lin != null && lin.Length > 0 && lin != ".") {
                    if (char.IsWhiteSpace(lin[0])) {
                        curlin += lin.Trim();
                        continue;
                    }
                    parseHeaderEntry(curlin);
                    if (lin != ".")
                        curlin = lin;
                    n++;
                }
                else {
                    parseHeaderEntry(curlin);
                    break;
                }
            }
            return n;
        }

        public void processHeader() {
            (mlTo = new People()).parse(getHeaderValue("To", ""));
            (mlCC = new People()).parse(getHeaderValue("Cc", ""));
            (mlFrom = new People()).parse(getHeaderValue("From", ""));
            (mlReturnPath = new People()).parse(getHeaderValue("Return-Path", ""));
            (mlReferences = new People()).parse(getHeaderValue("References", ""));
            (mlInReplyTo = new People()).parse(getHeaderValue("In-Reply-To", ""));
            (mlMessageID = new People()).parse(getHeaderValue("Message-ID", ""));

            mlSubject = SubjectDecode.decode(getHeaderValue("Subject", ""));
            mlContentType = new ContentTypeContainer(getHeaderValue("Content-Type", ""));
            mlContentTransferEncoding = new ContentTransferEncodingContainer(getHeaderValue("Content-Transfer-Encoding", ""));
            mlContentDisposition = new ContentDispositionContainer(getHeaderValue("Content-Disposition", ""));
            mlDate = (getHeaderValue("Date", ""));
        }

        public BoundaryTy readBody(TextReader rr, EML_Media parent) {
            mlBody = null;
            mlBinary = null;

            string major = mlContentType.major;

            string boundary = parent.mlContentType.boundary;

            bool binaryMedia = !major.Equals("text");

            string charSet = mlContentType.charSet;
            CharsetEncoder charSet_enc = charSetEnc.getEncoding(charSet);
            if (charSet_enc == null) {
                throw new ArgumentException("不明な文字コード", charSet);
            }

            string transferEncoding = parent.mlContentTransferEncoding.major;
            TransferEncoder transfer_enc = transferEnc.getEncoding(transferEncoding);
            if (transfer_enc == null) {
                throw new ArgumentException("不明な変換コード", transferEncoding);
            }

            mlBody = new StringBuilder();
            mlBinary = new MemoryStream();

            String boundaryPass = (boundary != null)
                ? "--" + boundary
                : null
                ;
            String boundaryTerm = (boundary != null)
                ? "--" + boundary + "--"
                : null
                ;

            MemoryStream tempText = new MemoryStream();

            bool preserveCRLF = transfer_enc.preserveCRLF;

            ArrayList alDEBUG = new ArrayList();
            BoundaryTy bty = BoundaryTy.Pass;
            while (true) {
                string lin = rr.ReadLine();
                if (lin == null)
                    break;
                alDEBUG.Add(lin);
                if (boundaryTerm != null && lin.Equals(boundaryTerm)) {
                    bty = BoundaryTy.Term;
                    break;
                }
                if (boundaryPass != null && lin.Equals(boundaryPass))
                    break;
                byte[] bin = transfer_enc.decodeBinary(lin);
                if (binaryMedia) {
                    mlBinary.Write(bin, 0, bin.Length);
                    if (preserveCRLF) {
                        mlBinary.WriteByte((byte)'\r');
                        mlBinary.WriteByte((byte)'\n');
                    }
                    continue;
                }
                tempText.Write(bin, 0, bin.Length);
                if (preserveCRLF) {
                    tempText.WriteByte((byte)'\r');
                    tempText.WriteByte((byte)'\n');
                }
            }
            if (!binaryMedia) {
                string text = charSet_enc.decodeText(tempText.ToArray());
                mlBody.Append(text);
            }
            return bty;
        }

        public string getHeaderValue(string key, string nz) {
            return Util.Nz(headerMap[key], nz);
        }

        public void writeTo(TextWriter wr) {
            if (mlTo != null) wr.WriteLine("{0}: {1}", "To", mlTo.ToString());
            if (mlCC != null) wr.WriteLine("{0}: {1}", "CC", mlCC.ToString());
            if (mlFrom != null) wr.WriteLine("{0}: {1}", "From", mlFrom.ToString());
            if (mlReturnPath != null) wr.WriteLine("{0}: {1}", "ReturnPath", mlReturnPath.ToString());
            if (mlReferences != null) wr.WriteLine("{0}: {1}", "References", mlReferences.ToString());
            if (mlInReplyTo != null) wr.WriteLine("{0}: {1}", "InReplyTo", mlInReplyTo.ToString());
            if (mlMessageID != null) wr.WriteLine("{0}: {1}", "MessageID", mlMessageID.ToString());
            if (mlSubject != null) wr.WriteLine("{0}: {1}", "Subject", SubjectEncode.current.encode(mlSubject));
            if (mlDate != null) wr.WriteLine("{0}: {1}", "Date", mlDate);
            if (mlContentType != null) wr.WriteLine("{0}: {1}", "Content-Type", mlContentType);
            if (mlContentTransferEncoding != null) wr.WriteLine("{0}: {1}", "Content-Transfer-Encoding", mlContentTransferEncoding);
            if (mlContentDisposition != null) wr.WriteLine("{0}: {1}", "Content-Disposition", mlContentDisposition);
            wr.WriteLine("{0}: {1}", "MIME-Version", "1.0");
            wr.WriteLine("{0}: {1}", "X-Mailer", "HDD LibEML 1.0");

            string transferEncoding = mlContentTransferEncoding.major;
            TransferEncoder transfer_enc = transferEnc.getEncoding(transferEncoding);

            string charSet = mlContentType.charSet;
            CharsetEncoder charSet_enc = charSetEnc.getEncoding(charSet);

            wr.WriteLine();

            if (mlBody != null) {
                byte[] bin = charSet_enc.encodeText(mlBody.ToString());

                if (transfer_enc == null) {
                    throw new ArgumentException("不明な変換コード", transferEncoding);
                }

                wr.Write(transfer_enc.encodeBinary(bin));
            }
            else {
                if (transfer_enc == null) {
                    throw new ArgumentException("不明な変換コード", transferEncoding);
                }

                mlBinary.Position = 0;

                wr.Write(transfer_enc.encodeBinary(mlBinary.ToArray()));
            }
        }

        void parseHeaderEntry(string lin) {
            if (lin == null)
                return;
            int t = lin.IndexOf(':');
            if (t < 1)
                throw new ArgumentException("ヘッダとして解析しません", lin);
            string key = lin.Substring(0, t).Trim();
            string value = lin.Substring(t + 1).Trim();

            headerMap[key] = value;
        }

    }

    public enum BoundaryTy {
        Pass, Term,
    }

    public class EML_Writer2 {
        public ArrayList medium = new ArrayList();

        public EML_Writer2() {

        }

        public void addMedia(EML_Media media) {
            medium.Add(media);
        }

        public void writeTo(TextWriter wr) {
            int cx = medium.Count;
            if (cx > 0) {
                EML_Media M0 = (EML_Media)medium[0];
                string boundary = M0.mlContentType.boundary;
                int n = 0;
                foreach (EML_Media Mx in medium) {
                    if (boundary != null && n != 0) {
                        wr.WriteLine();
                        wr.WriteLine("--" + boundary);
                    }
                    Mx.writeTo(wr);
                    n++;
                }
                if (boundary != null) {
                    wr.WriteLine();
                    wr.WriteLine("--" + boundary + "--");
                }
            }
        }
    }

    public class EML_Reader {
        public EML_Media[] medium = new EML_Media[0];

        public EML_Reader() {

        }

        public void read(string filename) {
            using (StreamReader rr = new StreamReader(filename, Encoding.ASCII)) {
                read(rr);
            }
        }
        public void read(TextReader rr) {
            {
                EML_Media m = new EML_Media();
                m.readHeaders(rr);
                m.processHeader();
                m.readBody(rr, m);

                ArrayList al = new ArrayList();
                al.Add(m);

                while (rr.Peek() != -1) {
                    EML_Media mm = new EML_Media();
                    mm.readHeaders(rr);
                    mm.processHeader();
                    bool isTerm = mm.readBody(rr, m) == BoundaryTy.Term;

                    al.Add(mm);

                    if (isTerm) break;
                }

                medium = (EML_Media[])al.ToArray(typeof(EML_Media));
            }
        }

        public EML_Media main {
            get {
                if (medium.Length < 1)
                    return null;
                return medium[0];
            }
        }
    }

    class Util {
        public static string Nz(object input, string nz) {
            if (input != null)
                return (string)input;
            return nz;
        }
        public static void transfer(Stream so, Stream si) {
            byte[] bin = new byte[4096];
            while (true) {
                int r = si.Read(bin, 0, bin.Length);
                if (r < 1)
                    break;
                so.Write(bin, 0, r);
            }
        }
    }
}
