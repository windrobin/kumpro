using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CGI4Ruby {
    class Program {
        static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-us");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");

            String script = Environment.GetEnvironmentVariable("PATH_TRANSLATED");

            ProcessStartInfo psi = new ProcessStartInfo("ruby.exe", " \"" + script + "\"");
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            Process p = Process.Start(psi);

            String s = Environment.GetEnvironmentVariable("CONTENT_LENGTH");
            Int64 sizeInBytes = (String.IsNullOrEmpty(s) ? 0 : Convert.ToInt64(s));

            Stream sIn = Console.OpenStandardInput();
            Stream sIn2 = p.StandardInput.BaseStream;

            Stream sOut = p.StandardOutput.BaseStream;
            Stream sOut2 = Console.OpenStandardOutput();

            Stream sErr = p.StandardError.BaseStream;

            Thread tIn = new Thread((ThreadStart)delegate {
                if (sizeInBytes != 0) {
                    byte[] buff = new byte[4096];

                    Int64 pos = 0;
                    while (pos < sizeInBytes) {
                        int r = sIn.Read(buff, 0, Convert.ToInt32(Math.Min(buff.Length, sizeInBytes - pos)));
                        if (r < 1) break;
                        sIn2.Write(buff, 0, r);
                        pos += r;
                    }
                }

                sIn.Close();
                sIn2.Close();
            });

            tIn.Start();

            ManualResetEvent evWait = new ManualResetEvent(false);

            Thread tOut = new Thread((ThreadStart)delegate {
                byte[] bin = Ut.ReadLine(sOut);
                String row = Encoding.ASCII.GetString(bin);
                bool isCGI = false
                    || Regex.IsMatch(row, "^HTTP/(\\d+\\.\\d+)\\s+\\d+\\s+")
                    || Regex.IsMatch(row, "^.+?:\\s*.+")
                ;
                if (!isCGI) {
                    byte[] line = Encoding.ASCII.GetBytes("HTTP/1.0 500 Error\nContent-type: text/plain\n\n");
                    sOut2.Write(line, 0, line.Length);
                }
                sOut2.Write(bin, 0, bin.Length);

                evWait.Set();

                {
                    byte[] buff = new byte[4096];

                    while (true) {
                        int r = sOut.Read(buff, 0, buff.Length);
                        if (r < 1) break;
                        sOut2.Write(buff, 0, r);
                    }
                }
            });

            tOut.Start();

            Thread tErr = new Thread((ThreadStart)delegate {
                byte[] buff = new byte[4096];

                evWait.WaitOne();

                while (true) {
                    int r = sErr.Read(buff, 0, buff.Length);
                    if (r < 1) break;
                    sOut2.Write(buff, 0, r);
                }
            });

            tErr.Start();

            tIn.Join();
            tOut.Join();
            tErr.Join();

            sOut.Close();
            sOut2.Close();

            p.WaitForExit();
            Environment.Exit(p.ExitCode);
        }

        class Ut {
            internal static byte[] ReadLine(Stream si) {
                MemoryStream os = new MemoryStream(256);
                while (true) {
                    int r = si.ReadByte();
                    if (r < 0) break;
                    os.WriteByte((byte)r);
                    if (r == 10) break;
                }
                return os.ToArray();
            }
        }
    }
}
