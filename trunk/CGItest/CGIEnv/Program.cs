using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CGIEnv {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();

            IDictionary env = Environment.GetEnvironmentVariables();
            foreach (String key in env.Keys) {
                Console.WriteLine("{0}={1}", key, env[key]);
            }
        }
    }
}
