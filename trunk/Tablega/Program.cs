using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tablega {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[]args) {
            String url = null;

            foreach (String arg in args) {
                if (arg.StartsWith("/") || arg.StartsWith("-")) continue;

                if (url == null) url = arg;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TForm(url));
        }
    }
}
