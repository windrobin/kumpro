using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BIGFile {
    class Program {
        static void Main(string[] args) {
            Int64 size = 0;
            if (args.Length < 2 || !Int64.TryParse(args[1], out size)) {
                Console.Error.WriteLine("BIGFile file size ");
                Environment.Exit(1);
            }
            using (FileStream fs = File.Open(args[0], FileMode.CreateNew)) {
                fs.SetLength(size);
                fs.Close();
            }
        }
    }
}
