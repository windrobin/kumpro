using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TrackLogging {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            String fp = null;
            for (int x = 0; x < args.Length; x++) {
                if (fp == null) {
                    fp = args[x];
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            List<string> alfp = new List<string>();
            if (fp == null) {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "*.*|*.*||";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Multiselect = true;
                ofd.ReadOnlyChecked = true;
                ofd.Title = "【表示したいログファイルを選ぶ】";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    alfp.AddRange(ofd.FileNames);
                    if (alfp.Count == 1) fp = alfp[0];
                }
            }
            if (fp != null) {
                Application.Run(new LoggingForm(fp));
            }
            else if (alfp.Count != 0) {
                foreach (String fp1 in alfp) {
                    new LoggingForm(fp1).Show();
                }
                Application.Run();
            }
        }
    }
}