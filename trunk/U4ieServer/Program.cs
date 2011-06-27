using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;

namespace U4ieServer {
    class Program {
        static string DDNS_UPDATE { get { return "https://ieserver.net/cgi-bin/dip.cgi"; } }
        static string REMOTE_ADDR_CHK { get { return "https://ieserver.net/ipcheck.shtml"; } }

        static void Main(string[] args) {
            if (args.Length < 3) {
                Console.Error.WriteLine("U4ieServer account domain password ");
                Environment.Exit(1);
            }

            WebClient wc = new WebClient();
            try {
                String myip = wc.DownloadString(REMOTE_ADDR_CHK);
                if (myip.Trim().Length == 0)
                    Environment.Exit(3);
                byte[] bin = wc.DownloadData(DDNS_UPDATE + "?username=" + HttpUtility.UrlEncode(args[0]) + "&domain=" + HttpUtility.UrlEncode(args[1]) + "&password=" + HttpUtility.UrlEncode(args[2]) + "&updatehost=1");
                String html = Encoding.GetEncoding("euc-jp").GetString(bin);
                if (html.Contains(myip)) {
                    return;
                }
                Environment.Exit(4);
            }
            catch (WebException) {
                Environment.Exit(2);
            }
        }
    }
}
