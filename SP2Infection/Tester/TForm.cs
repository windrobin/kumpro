using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Tester {
    public partial class TForm : Form {
        public TForm() {
            InitializeComponent();
        }

        private void bSearch_Click(object sender, EventArgs e) {
            Sync = SynchronizationContext.Current;

            bSearch.Enabled = false;
            bwSearcher.RunWorkerAsync(tbIn.Lines);
        }

        SynchronizationContext Sync;

        class SP2Infection {
            public void Test(String fp) {
                ProcessStartInfo psi = new ProcessStartInfo("ildasm.exe", " \"" + fp + "\" /text ");
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process p = Process.Start(psi);
                String s = p.StandardOutput.ReadToEnd();
                int err = 0;
                // http://social.msdn.microsoft.com/Forums/ja-JP/vsgeneralja/thread/5fbe7269-8bbd-4720-b2d7-366b4b48ff84
                String[] al = {
                    "callvirt   instance bool [mscorlib]System.Threading.WaitHandle::WaitOne(int32)",
                    "call       int32 [mscorlib]System.String::Compare(string, int32, string, int32, int32, class [mscorlib]System.Globalization.CultureInfo, valuetype [mscorlib]System.Globalization.CompareOptions)",
                    "call       int32 [mscorlib]System.String::Compare(string, string, class [mscorlib]System.Globalization.CultureInfo, valuetype [mscorlib]System.Globalization.CompareOptions)",
                    "[System]System.ComponentModel.DateTimeOffsetConverter",
                    "callvirt   instance bool [mscorlib]System.Threading.WaitHandle::WaitOne(valuetype [mscorlib]System.TimeSpan)",
                    "callvirt   instance bool [mscorlib]System.Threading.WaitHandle::WaitOne(int32)",
                    "call       bool [mscorlib]System.Threading.WaitHandle::WaitAll(class [mscorlib]System.Threading.WaitHandle[], int32)",
                    "call       bool [mscorlib]System.Threading.WaitHandle::WaitAll(class [mscorlib]System.Threading.WaitHandle[], valuetype [mscorlib]System.TimeSpan)",
                    "call       int32 [mscorlib]System.Threading.WaitHandle::WaitAny(class [mscorlib]System.Threading.WaitHandle[], int32)",
                    "call       int32 [mscorlib]System.Threading.WaitHandle::WaitAny(class [mscorlib]System.Threading.WaitHandle[], valuetype [mscorlib]System.TimeSpan)",
                    "callvirt   instance bool [System.Deployment]System.Deployment.Application.ApplicationDeployment::CheckForUpdate(bool)",
                    "callvirt   instance class [System.Deployment]System.Deployment.Application.UpdateCheckInfo [System.Deployment]System.Deployment.Application.ApplicationDeployment::CheckForDetailedUpdate(bool)",
                    "call       valuetype [mscorlib]System.GCNotificationStatus [mscorlib]System.GC::WaitForFullGCApproach(int32)",
                    "call       valuetype [mscorlib]System.GCNotificationStatus [mscorlib]System.GC::WaitForFullGCApproach()",
                    "call       valuetype [mscorlib]System.GCNotificationStatus [mscorlib]System.GC::WaitForFullGCComplete()",
                    "call       valuetype [mscorlib]System.GCNotificationStatus [mscorlib]System.GC::WaitForFullGCComplete(int32)",

                    "class [mscorlib]System.Security.SecurityState",
                    "class [mscorlib]System.Security.SecuritySafeCriticalAttribute",
                };
                for (int x = 0; x < al.Length; x++) {
                    if (s.Contains(al[x]))
                        err |= 2 << x;
                }
                if (err != 0) {
                    wr.WriteLine(fp + " " + err.ToString("x4"));
                }
            }

            public StringWriter wr = new StringWriter();
        }

        private void bwSearcher_DoWork(object sender, DoWorkEventArgs e) {
            String[] alIn = (String[])e.Argument;
            foreach (String fp in alIn) {
                SP2Infection t = new SP2Infection();
                if (Directory.Exists(fp)) {
                    foreach (String fpAny in Directory.GetFiles(fp, "*", SearchOption.AllDirectories)) {
                        if (String.Compare(".dll", Path.GetExtension(fpAny), true) == 0 || String.Compare(".exe", Path.GetExtension(fpAny), true) == 0) {
                            Updatel(fpAny);
                            t.Test(fpAny);
                        }
                    }
                }
                else if (File.Exists(fp)) {
                    Updatel(fp);
                    t.Test(fp);
                }
                Sync.Send(delegate(object state) {
                    tbInfected.AppendText("\r\n" + t.wr.ToString());
                }, null);
            }
        }

        private void Updatel(string fp) {
            Sync.Post(delegate(object state) {
                lNow.Text = (String)state;
            }, fp);
        }

        private void bwSearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            bSearch.Enabled = true;

            if (e.Error != null) {
                MessageBox.Show(this, e.Error.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}