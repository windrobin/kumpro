using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TCPLongTest {
    class Program {
        static void Main(string[] args) {
            new Program().Run();
        }

        private void Run() {
            Console.Title = "TCPLongTest";
            Console.BufferWidth = Math.Max(Console.BufferWidth, 160);
            Console.BufferHeight = Math.Max(Console.BufferHeight, 300);

            Console.Write("受信(R) 又は 送信(S)? ");
            String mode = Console.ReadLine().ToUpperInvariant();
            Console.Write("ポート番号(12321等)? ");
            int port = Convert.ToInt32(Console.ReadLine());
            if (false) { }
            else if (mode == "R") {
                Console.Title += " R側";
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                Console.WriteLine("このパソコンのIPアドレス一覧：");
                foreach (IPAddress ip in Dns.GetHostAddresses(Environment.MachineName)) {
                    LOGC("    `3" + ip + "``\n");
                }
                LOG("接続を待っています。");
                TcpListener lis = new TcpListener(ep);
                lis.Start();
                while (true) {
                    TcpClient cli = lis.AcceptTcpClient();
                    LOG("接続が来ました。相手 ->" + cli.Client.RemoteEndPoint);
                    new Thread(Recv).Start(cli);
                    new Thread(SendIt).Start(cli);
                }
            }
            else if (mode == "S") {
                Console.Title += " S側";
                Console.Write("接続先IPアドレス? ");
                IPAddress ip2 = IPAddress.Parse(Console.ReadLine());
                IPEndPoint ep = new IPEndPoint(ip2, port);
                LOG("接続しています。" + ep);
                TcpClient cli = new TcpClient();
                cli.Connect(ep);
                Socket sock = cli.Client;
                LOG("接続しました。相手   ->" + cli.Client.RemoteEndPoint);
                Thread t1 = new Thread(Recv); t1.Start(cli);
                Thread t2 = new Thread(SendIt); t2.Start(cli);

                t1.Join();
                t2.Join();
            }
            else return;

            Console.ReadLine();
        }

        private void LOG(string p) {
            lock (typeof(Console)) {
                LOGC(String.Format("{0:yyyy/MM/dd HH:mm:ss}|{1}", DateTime.Now, p) + "\n");
            }
        }

        private void LOGC(string p) {
            ConsoleColor[] clrs = new ConsoleColor[]{
                ConsoleColor.Black,
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.Yellow,
            };
            ConsoleColor fc = Console.ForegroundColor;
            for (int x = 0; x < p.Length; x++) {
                char c = p[x];
                if (c == '`') {
                    x++;
                    c = p[x];
                    if (c == '`') {
                        Console.ForegroundColor = fc;
                        continue;
                    }
                    if (char.IsNumber(c)) {
                        Console.ForegroundColor = clrs[int.Parse("" + c)];
                        continue;
                    }
                }
                Console.Write(c);
            }
            try {
                File.AppendAllText("TCPLongTest.log", p.Replace("\r\n", "\n").Replace("\n", "\r\n"), Encoding.UTF8);
            }
            catch (Exception) {
            }
        }

        void SendIt(Object state) {
            TcpClient cli = (TcpClient)state;
            NetworkStream st = cli.GetStream();
            Socket sock = cli.Client;
            EndPoint ep1 = sock.LocalEndPoint;
            EndPoint ep2 = sock.RemoteEndPoint;
            try {
                while (true) {
                    String s = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " from " + Environment.MachineName;
                    byte[] bin = Encoding.UTF8.GetBytes(s);
                    st.Write(bin, 0, bin.Length);
                    LOG(ep1, ep2, "送信「`1" + s + "``」");
                    Thread.Sleep(5000);
                }
            }
            catch (Exception err) {
                LOG(ep1, ep2, err);
            }
        }

        void Recv(Object state) {
            TcpClient cli = (TcpClient)state;
            Socket sock = cli.Client;
            EndPoint ep1 = sock.LocalEndPoint;
            EndPoint ep2 = sock.RemoteEndPoint;
            try {
                byte[] bin = new byte[1000];
                while (true) {
                    int r = sock.Receive(bin, 0, 1000, SocketFlags.None);
                    if (r < 1) {
                        LOG(ep1, ep2, "切断。");
                        break;
                    }
                    LOG(ep1, ep2, "受信「`1" + Encoding.UTF8.GetString(bin, 0, r) + "``」");
                }
            }
            catch (Exception err) {
                LOG(ep1, ep2, err);
            }
        }

        private void LOG(EndPoint ip1, EndPoint ip2, Exception err) {
            foreach (String s in err.Message.Split('\n')) {
                LOG(String.Format("{0,21}<>{1,-21}|`2{2}``", ip1, ip2, s));
            }
        }
        private void LOG(EndPoint ip1, EndPoint ip2, String text) {
            foreach (String s in text.Split('\n')) {
                LOG(String.Format("{0,21}<>{1,-21}|{2}", ip1, ip2, s));
            }
        }
    }
}
