using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;
using System.IO;
using System.Text;

namespace FTP4AFP {
    public class Program : ServiceBase {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            bool service = false;
            Queue<String> vars = new Queue<string>();
            foreach (String v in args) vars.Enqueue(v);
            while (vars.Count != 0) {
                String a = vars.Dequeue();
                if (a.StartsWith("@")) {
                    foreach (String v in CLUt.Parse(File.ReadAllText(a.Substring(1), Encoding.Default))) vars.Enqueue(v);
                    continue;
                }
                if (a.StartsWith("/s=")) {
                    als.Add(a.Substring(3));
                }
                if (a == "/service") {
                    service = true;
                }
            }
            if (service) {
                ServiceBase[] ServicesToRun = new ServiceBase[] { new Program() };
                ServiceBase.Run(ServicesToRun);
            }
            else {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(als));
            }
        }

        static List<String> als = new List<string>();

        class CLUt {
            public static String[] Parse(string s) {
                List<string> args = new List<string>();
                for (int x = 0, cx = s.Length; x < cx; ) {
                    if (char.IsWhiteSpace(s[x])) {
                        x++;
                        continue;
                    }
                    if (s[x] == '"') {
                        x++;
                        String a = "";
                        while (true) {
                            if (x < cx) {
                                if (s[x] == '"') {
                                    if (x + 1 < cx && s[x + 1] == '"') {
                                        x += 2;
                                        a += '"';
                                    }
                                    else {
                                        x++;
                                        break;
                                    }
                                }
                                else {
                                    a += s[x];
                                    x++;
                                }
                            }
                            else break;
                        }
                        args.Add(a);
                    }
                    else {
                        String a = "";
                        while (true) {
                            if (x < cx) {
                                if (char.IsWhiteSpace(s[x]))
                                    break;
                                a += s[x];
                                x++;
                            }
                            else break;
                        }
                        args.Add(a);
                    }
                }
                return args.ToArray();
            }
        }

        protected override void OnStart(string[] args) {
            new Thread((ThreadStart)delegate {
                Svc();
            }).Start();
        }

        protected override void OnStop() {
            evExit.Set();
        }

        ManualResetEvent evExit = new ManualResetEvent(false);

        void Svc() {
            List<AFPServ> al = new List<AFPServ>();

            foreach (String a in als) {
                AFPServ afps = new AFPServ();
                afps.CC.Setting = a;
                if (afps.CC.AutoStart) afps.Start();
                al.Add(afps);
            }

            evExit.WaitOne();

            foreach (AFPServ afps in al) {
                afps.Stop();
            }
        }
    }
}