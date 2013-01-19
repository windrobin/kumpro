using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AddMId {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("AddMId A.eml");
                return;
            }
            String fp = args[0];
            StringWriter wr = new StringWriter();
            bool isX = true, isHeader = false, isBody = false, hasMId = false;
            bool isModified = false;
            Encoding enc = Encoding.GetEncoding(28591);
            foreach (String row in File.ReadAllLines(fp, enc)) {
                if (false) { }
                else if (isX && row == "<<MAIL-DATA>>") {
                    isX = false;
                    isHeader = true;
                }
                else if (isX) {

                }
                else if (isHeader && row.Length == 0) {
                    isHeader = false;
                    isBody = true;

                    if (!hasMId) {
                        wr.WriteLine("Message-ID: <{1}.{2}@{0}>"
                            , Environment.MachineName
                            , Guid.NewGuid().ToString("N")
                            , DateTime.UtcNow.Ticks
                            );
                        isModified = true;
                    }
                }
                else if (isHeader) {
                    if (char.IsWhiteSpace(row[0])) {

                    }
                    else {
                        String key = row.Split(':')[0].Trim();
                        if (String.Compare(key, "Message-ID", true) == 0) {
                            hasMId = true;
                        }
                    }
                }
                else if (isBody) {

                }
                wr.WriteLine(row);
            }
            if (isModified) File.WriteAllText(fp, wr.ToString(), enc);
        }
    }
}
