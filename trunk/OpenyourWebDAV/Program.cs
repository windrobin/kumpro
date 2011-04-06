using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace OpenyourWebDAV {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            String fp = null;
            foreach (String a in args) {
                if (a.StartsWith("/")) {
                }
                else if (File.Exists(a)) {
                    fp = a;
                }
            }

            if (fp != null) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new VForm(fp));
            }
        }
    }
}
