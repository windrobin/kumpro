using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Globalization;

namespace CGI4Ruby {
    class Program {
        static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-us");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");

            String script = Environment.GetEnvironmentVariable("PATH_TRANSLATED");

            ProcessStartInfo psi = new ProcessStartInfo("ruby.exe", " \"" + script + "\"");
            psi.RedirectStandardInput = true;
            psi.UseShellExecute = false;
            Process p = Process.Start(psi);

            String s = Environment.GetEnvironmentVariable("CONTENT_LENGTH");
            Int64 sizeInBytes = (String.IsNullOrEmpty(s) ? 0 : Convert.ToInt64(s));

            Stream si = Console.OpenStandardInput();
            Stream os = p.StandardInput.BaseStream;

            if (sizeInBytes != 0) {
                byte[] buff = new byte[4096];

                Int64 pos = 0;
                while (pos < sizeInBytes) {
                    int r = si.Read(buff, 0, Convert.ToInt32(Math.Min(buff.Length, sizeInBytes - pos)));
                    if (r < 1) break;
                    os.Write(buff, 0, r);
                    pos += r;
                }

            }

            si.Close();
            os.Close();

            p.WaitForExit();
            Environment.Exit(p.ExitCode);
        }
    }
}
