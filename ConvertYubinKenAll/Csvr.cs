using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RidocHTMLImp {
    public class Csvr {
        public List<String[]> Rows = new List<string[]>();

        public void ReadAppended(String s, char quote, char delim) {
            int x = 0, cx = s.Length;
            List<string[]> rows = new List<string[]>();
            List<string> cols = new List<string>();
            int vx = 0, vy = 0;
            String t = "";
            while (true) {
                if (x == cx) {
                    if (vx != 0) {
                        rows.Add(cols.ToArray());
                        rows.Clear();
                        vx = 0;
                        vy++;
                    }
                    break;
                }
                if (s[x] == quote) {
                    x++;
                    while (x < cx) {
                        if (s[x] == quote) {
                            if (x + 1 < cx && s[x + 1] == quote) {
                                x += 2;
                                t += quote;
                            }
                            else {
                                x++;
                                break;
                            }
                        }
                        else {
                            t += s[x];
                            x++;
                        }
                    }
                }
                if (x == cx) continue;
                if (s[x] == delim) {
                    x++;
                    cols.Add(t);
                    t = "";
                    vx++;
                }
                else if (s[x] == '\n' || s[x] == '\r') {
                    if (s[x] == '\r') {
                        x++;
                        if (x < cx && s[x] == '\n') {
                            x++;
                        }
                    }
                    else {
                        x++;
                    }
                    cols.Add(t);
                    rows.Add(cols.ToArray());
                    cols.Clear();
                    t = "";
                    vy++;
                    vx = 0;
                }
                else {
                    while (true) {
                        if (x == cx) break;
                        if (s[x] == quote || s[x] == delim || s[x] == '\r' || s[x] == '\n') break;
                        t += s[x];
                        x++;
                    }
                    if (s[x] == delim) x++;
                    cols.Add(t);
                    t = "";
                    vx++;
                }
            }
            Rows.AddRange(rows.ToArray());
        }
    }
}
