using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Viewer {
    public class Csvw {
        TextWriter wr;
        char Sep, Quote;

        public Csvw(TextWriter wr, char sep, char quote) {
            this.wr = wr;
            this.Sep = sep;
            this.Quote = quote;
        }

        int x = 0;

        public void Write(String s) {
            if (x != 0) wr.Write("" + Sep);
            if (s.IndexOfAny(new char[] { Sep, Quote, '\r', '\n' }) < 0) {
                wr.Write(s);
            }
            else {
                wr.Write(Quote + s.Replace("" + Quote, "" + Quote + Quote) + Quote);
            }
            x++;
        }

        public void NextRow() {
            x = 0;
            wr.WriteLine();
        }
    }
}
