using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace DrivePassThru {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("DrivePassThru http://...");
                Environment.Exit(1);
            }
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.ContentType] = "text/plain; charset=utf-8";
            Uri uri = new Uri(args[0]);
            String res = wc.UploadString(uri, "POST", "test");
            Console.WriteLine(res);
        }
    }
}
