using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SP2Infection {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("TestAssembly4VanillaNetfx2 assembly-name.exe");
                Environment.Exit(1);
            }
            new Program().Run(args[0]);
        }

        private void Run(string fp) {
            ProcessStartInfo psi = new ProcessStartInfo("ildasm.exe", " \"" + fp + "\" /text ");
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
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
                Console.Error.WriteLine("Infected " + err.ToString("X"));
                Environment.Exit(err);
            }
            Console.WriteLine("Ok");
        }

        void Infection() {
            System.Threading.ManualResetEvent ev = new System.Threading.ManualResetEvent(false);
            ev.WaitOne(new TimeSpan(0, 0, 0, 0, 300));
            ev.WaitOne(300);

        }
    }
}
