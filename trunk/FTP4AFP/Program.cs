using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FTP4AFP {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[]args) {
            List<String> als = new List<string>();
            foreach (String a in args) {
                if (a.StartsWith("/s=")) {
                    als.Add(a.Substring(3));
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(als));
        }
    }
}