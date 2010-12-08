using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace POP3Vcf {
    class Program {
        static void Main(string[] args) {
            new Program().Run();
        }

        private void Run() {
            TcpListener listen = new TcpListener(new IPEndPoint(IPAddress.Any, 110));
            listen.Start();

            while (true) {
                TcpClient tcp = listen.AcceptTcpClient();
                new Thread(POP3).Start(tcp);
            }
        }

        void POP3(object state) {
            TcpClient tcp = (TcpClient)state;
            NetworkStream ns = tcp.GetStream();
            StreamWriter wr = new StreamWriter(ns, Encoding.ASCII);
            StreamReader rr = new StreamReader(ns, Encoding.ASCII);
            wr.AutoFlush = true;
            wr.WriteLine("+OK ready");
            String user = null, pass = null;
            List<Mail> alVcf = new List<Mail>();
            while (true) {
                String row = rr.ReadLine();
                if (row == null) break;
                //Console.WriteLine(row);
                String[] cols = Regex.Replace(row, "\\s+", " ").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (cols.Length < 1) {
                    wr.WriteLine("-ERR No command");
                }
                else if (String.Compare(cols[0], "USER", true) == 0) {
                    user = cols[1];
                    wr.WriteLine("+OK Send your password then");
                }
                else if (String.Compare(cols[0], "PASS", true) == 0) {
                    pass = cols[1];
                    alVcf = null;
                    try {
                        alVcf = OpenMBox(user, pass);
                        wr.WriteLine("+OK Authorized");
                    }
                    catch (UnauthorizedAccessException err) {
                        wr.WriteLine("-ERR Unauthorized: " + err.Message);
                    }
                }
                else if (String.Compare(cols[0], "STAT", true) == 0) {
                    if (alVcf == null) {
                        wr.WriteLine("-ERR Authorize first");
                    }
                    else {
                        int cb = 0;
                        foreach (Mail o in alVcf) cb += o.EMLBin.Length;
                        wr.WriteLine("+OK " + alVcf.Count + " " + cb);
                    }
                }
                else if (String.Compare(cols[0], "UIDL", true) == 0) {
                    if (alVcf == null) {
                        wr.WriteLine("-ERR Authorize first");
                    }
                    else {
                        wr.WriteLine("+OK ");
                        foreach (Mail o in alVcf) {
                            wr.WriteLine(o.Index + " " + o.Uidl);
                        }
                        wr.WriteLine(".");
                    }
                }
                else if (String.Compare(cols[0], "LIST", true) == 0) {
                    if (alVcf == null) {
                        wr.WriteLine("-ERR Authorize first");
                    }
                    else {
                        wr.WriteLine("+OK " + alVcf.Count + " messages");
                        foreach (Mail o in alVcf) {
                            wr.WriteLine(o.Index + " " + o.EMLBin.Length);
                        }
                        wr.WriteLine(".");
                    }
                }
                else if ((String.Compare(cols[0], "TOP", true) == 0 || String.Compare(cols[0], "RETR", true) == 0) && cols.Length >= 2) {
                    if (alVcf == null) {
                        wr.WriteLine("-ERR Authorize first");
                    }
                    else {
                        bool sent = false;
                        foreach (Mail o in alVcf) {
                            if ("" + o.Index == cols[1]) {
                                wr.WriteLine("+OK " + o.EMLBin.Length + " octets");
                                byte[] bin = o.EMLBin;
                                wr.BaseStream.Write(bin, 0, bin.Length);
                                wr.WriteLine();
                                wr.WriteLine(".");
                                sent = true;
                                break;
                            }
                        }
                        if (!sent) {
                            wr.WriteLine("-ERR No message");
                        }
                    }
                }
                else if (String.Compare(cols[0], "QUIT", true) == 0) {
                    wr.WriteLine("+OK Farewell");
                    break;
                }
                else {
                    wr.WriteLine("-ERR Unknown command");
                }
            }
            tcp.Close();
        }

        List<Mail> OpenMBox(string user, string pass) {
            String dirIn = Path.Combine(Environment.CurrentDirectory, user + " " + pass);
            if (Directory.Exists(dirIn)) {
                List<Mail> al = new List<Mail>();
                int i = 1;
                foreach (String fp in Directory.GetFiles(dirIn, "*.vcf")) {
                    try {
                        Mail o = new Mail(i, fp, user);
                        al.Add(o);
                    }
                    catch (Exception) {

                    }
                }
                return al;
            }
            throw new UnauthorizedAccessException("Invalid user or password.");
        }

        class Mail {
            int i;
            string uidl;
            byte[] emlbin;

            public Mail(int i, string fp, string user) {
                this.i = i;

                byte[] bin = File.ReadAllBytes(fp);
                String s = "";
                foreach (byte b in System.Security.Cryptography.MD5.Create().ComputeHash(bin)) {
                    s += b.ToString("x2");
                }
                this.uidl = s;
                String body = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Template.eml"), Encoding.ASCII);
                body = body
                    .Replace("%DATE%", File.GetLastWriteTime(fp).ToUniversalTime().ToString("r"))
                    .Replace("%FROM%", user)
                    .Replace("%TO%", user)
                    .Replace("%UIDL%", uidl)
                    .Replace("%BODY:BASE64%", Convert.ToBase64String(bin, Base64FormattingOptions.InsertLineBreaks))
                    ;

                this.emlbin = Encoding.ASCII.GetBytes(body);
            }

            public int Index { get { return i; } }
            public string Uidl { get { return uidl; } }

            public byte[] EMLBin { get { return emlbin; } }
        }
    }
}
