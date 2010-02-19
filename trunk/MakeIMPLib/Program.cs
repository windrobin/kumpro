using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

// Credits: http://support.microsoft.com/?scid=kb;en-us;131313&x=11&y=8

namespace MakeIMPLib {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 2) {
                Console.Error.WriteLine("MakeIMPLib libpoppler-glib-4.dll libpoppler-glib-4.lib");
                Environment.Exit(1);
            }

            StringWriter wr = new StringWriter();
            {
                String fpIn = args[0];
                ProcessStartInfo psi = new ProcessStartInfo("dumpbin.exe", " /exports \"" + fpIn + "\"");
                psi.RedirectStandardOutput = true;
                psi.StandardOutputEncoding = Encoding.ASCII;
                psi.UseShellExecute = false;
                Process p = Process.Start(psi);
                // "        247   F6 0000A5F0 poppler_annot_get_type"
                wr.WriteLine("NAME " + Path.GetFileName(fpIn));
                foreach (Match M in Regex.Matches(p.StandardOutput.ReadToEnd(), "^\\s+(?<ordinal>\\d+)\\s+(?<hint>[0-9a-f]+)\\s+(?<RVA>[0-9a-f]{8})\\s+(?<name>.+)$", RegexOptions.IgnoreCase | RegexOptions.Multiline)) {
                    wr.WriteLine("EXPORTS {0}"
                        , M.Groups["name"].Value
                        , M.Groups["ordinal"].Value
                        );
                }
                p.WaitForExit();
            }

            {
                String fpdef = Path.GetTempFileName();
                String fplib = args[1];
                File.WriteAllText(fpdef, wr.ToString());

                ProcessStartInfo psi = new ProcessStartInfo("lib.exe", " /machine:x86 /def:\"" + fpdef + "\" /out:\"" + fplib + "\"");
                psi.UseShellExecute = false;
                Process p = Process.Start(psi);
                p.WaitForExit();
            }
        }
    }
}
