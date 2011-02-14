using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace SendHostAd {
    static class Program {
        static TraceSource appTrace = new TraceSource("SendHostAd", SourceLevels.Information);

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            try {
                String fp = null;
                bool fSetup = false;
                foreach (String arg in args) {
                    if (arg.StartsWith("/")) {
                        if (arg == "/setup")
                            fSetup = true;
                    }
                    else if (fp == null) fp = arg;
                }

                if (fSetup || fp == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new NForm());
                }
                else {
                    try {
                        WebClient wc = new WebClient();
                        String res = wc.UploadString(fp, "");

                        appTrace.TraceEvent(TraceEventType.Information, (int)EvID.Sent, "送信しました。結果: " + res);
                    }
                    catch (Exception err) {
                        appTrace.TraceEvent(TraceEventType.Error, (int)EvID.Exception, "送信に失敗: " + err);
                        appTrace.Close();
                        Environment.Exit(1);
                    }
                }
            }
            finally {
                appTrace.Close();
            }
        }

        enum EvID {
            None, Sent, Exception,
        }
    }
}