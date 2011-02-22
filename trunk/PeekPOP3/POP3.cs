using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace ShachoUndelivered {
    public class POP3 : IDisposable {
        TcpClient sock = null;
        Resp resp;

        public POP3(String host, int port, String user, String pass) {
            sock = new TcpClient();
            sock.Connect(host, port);
            NetworkStream siso = sock.GetStream();

            if ((resp = Resp.ReadFrom(siso)).Ok != true) throw new CommException();

            Match M = Regex.Match(resp.Message, "(\\<.+\\>)");
            if (M.Success) {
                byte[] hasher = Encoding.ASCII.GetBytes(M.Groups[1].Value + pass);

                SL.WriteLine(siso, "APOP " + user + " " + Ut.H2S(MD5.Create().ComputeHash(hasher)));

                if ((resp = Resp.ReadFrom(siso)).Ok != true) throw new APOPCommandFailedException(resp.Message);
            }
            else throw new CommandFailedException();
        }

        public List<MailItem> GetList() {
            lock (this) {
                NetworkStream siso = sock.GetStream();
                SL.WriteLine(siso, "LIST");

                if ((resp = Resp.ReadFrom(siso)).Ok != true) throw new CommandFailedException();

                List<MailItem> al = new List<MailItem>();
                while (true) {
                    string s = RL.ReadLine(siso);
                    if (s.StartsWith(".")) break;
                    string[] cols = s.Split(' ');
                    MailItem o = new MailItem();
                    o.i = int.Parse(cols[0]);
                    o.size = int.Parse(cols[1]);
                    al.Add(o);
                }
                return al;
            }
        }

        public List<UidlItem> GetUidl() {
            lock (this) {
                NetworkStream siso = sock.GetStream();
                SL.WriteLine(siso, "UIDL");

                if ((resp = Resp.ReadFrom(siso)).Ok != true) throw new CommandFailedException();

                List<UidlItem> al = new List<UidlItem>();
                while (true) {
                    string s = RL.ReadLine(siso);
                    if (s.StartsWith(".")) break;
                    string[] cols = s.Split(' ');
                    UidlItem o = new UidlItem();
                    o.i = int.Parse(cols[0]);
                    o.uidl = cols[1];
                    al.Add(o);
                }
                return al;
            }
        }

        public string Retrieve(int i) {
            lock (this) {
                NetworkStream siso = sock.GetStream();
                SL.WriteLine(siso, "RETR " + i);

                if ((resp = Resp.ReadFrom(siso)).Ok != true) throw new CommandFailedException();

                StringBuilder res = new StringBuilder(1024 * 1024);
                while (true) {
                    string s = RL.ReadLine(siso);
                    if (s.Equals(".")) break;
                    res.Append(s).Append("\r\n");
                }
                return res.ToString();
            }
        }

        public string Top(int i, int cnt) {
            lock (this) {
                NetworkStream siso = sock.GetStream();
                SL.WriteLine(siso, "TOP " + i + " " + cnt);

                if ((resp = Resp.ReadFrom(siso)).Ok != true) throw new CommandFailedException(resp.Message);

                StringBuilder res = new StringBuilder();
                while (true) {
                    string s = RL.ReadLine(siso);
                    if (s.Equals(".")) break;
                    res.Append(s).Append("\r\n");
                }
                return res.ToString();
            }
        }

        static class Ut {
            public static string H2S(byte[] bin) {
                string s = "";
                foreach (byte b in bin) {
                    s += b.ToString("x2");
                }
                return s;
            }
        }

        class RL {
            public static string ReadLine(Stream si) {
                StringBuilder s = new StringBuilder(200);
                while (true) {
                    int r = si.ReadByte();
                    if (r < 0 && s.Length == 0) return null;
                    if (r < 0) break;
                    if (r == (int)'\n') break;
                    if (r != (int)'\r') s.Append((char)r);
                }
                return s.ToString();
            }
        }
        class SL {
            public static void WriteLine(Stream os, String text) {
                byte[] bin = Encoding.ASCII.GetBytes(text + "\r\n");
                os.Write(bin, 0, bin.Length);
            }
        }

        class Resp {
            public bool Ok;
            public string Message;

            public static Resp ReadFrom(NetworkStream si) {
                Resp o = new Resp();
                String s = "";
                while (true) {
                    int r = si.ReadByte();
                    if (r < 0) throw new EndOfStreamException();
                    if (r == (int)'\n') break;
                    if (r != (int)'\r') s += (char)r;
                }
                o.Ok = s.StartsWith("+OK");
                o.Message = s;
                return o;
            }
        }

        #region IDisposable メンバ

        public void Dispose() {
            if (sock != null) {
                sock.Close();
                sock = null;
            }
        }

        #endregion
    }

    public class UidlItem {
        public int i;
        public string uidl;
    }

    public class MailItem {
        public int i, size;

        public override string ToString() {
            return string.Format("{0}", i);
        }
    }

    public class CommException : ApplicationException {
        public CommException() : base("主に通信が失敗しました") { }
    }
    public class CommandFailedException : ApplicationException {
        public CommandFailedException() : base("主にコマンドが失敗しました") { }
        public CommandFailedException(String resp) : base("主にコマンドが失敗しました。結果『" + resp + "』") { }
    }
    public class APOPCommandFailedException : ApplicationException {
        public APOPCommandFailedException(String res) : base("APOPコマンドが失敗しました。結果『" + res + "』") { }
    }
}
