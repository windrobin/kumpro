using System;
using System.Collections.Generic;
using System.Text;

namespace CGIPassThru {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();
            while (true) {
                int a = Console.Read();
                if (a < 0) break;
                Console.Write((char)a);
            }
        }
    }
}
