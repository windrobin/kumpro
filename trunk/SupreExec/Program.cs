using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace SupreExec {
    static class Program {
        [MTAThread()]
        static void Main(string[] args) {
            String app = "", cmd = "";

            foreach (string arg in args) {
                Match M;
                if ((M = Regex.Match(arg, "^\\*(?<env>[^=]+)=(?<path>(.+))$")).Success) {
                    String path = M.Groups["path"].Value;
                    String env = Convert.ToString(Registry.GetValue(Path.GetDirectoryName(path), Path.GetFileName(path), ""));

                    Environment.SetEnvironmentVariable(M.Groups["env"].Value, env);
                }
                else if (String.IsNullOrEmpty(app)) {
                    app = Environment.ExpandEnvironmentVariables(arg);
                }
                else {
                    cmd += " \"" + Environment.ExpandEnvironmentVariables(arg) + "\"";
                }
            }

            //MessageBox.Show(app + "\n" + cmd);

            ProcessStartInfo psi = new ProcessStartInfo(app, cmd);
            Process.Start(psi);
        }
    }
}