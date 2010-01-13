using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;

namespace RunTimeViser2 {
    public partial class RForm : Form {
        public RForm() {
            InitializeComponent();
        }

        List<Evt> alevt = new List<Evt>();

        private void RForm_Load(object sender, EventArgs e) {
            lvt.ListViewItemSorter = new DTComparer(false);

            EventLog el = new EventLog("System");
            Evt evt = null;
            foreach (EventLogEntry ent in el.Entries) {
                if (ent.Source == "EventLog") {
                    ListViewItem lvi = null;
                    bool ison = ent.EventID == 6005;
                    bool isoff = ent.EventID == 6006;

                    if (ison) { // On
                        lvi = new ListViewItem("ì¸");
                        lvi.ImageKey = "on";
                    }
                    else if (isoff) { // Off
                        lvi = new ListViewItem("êÿ");
                        lvi.ImageKey = "off";
                    }
                    if (lvi != null) {
                        lvi.Tag = ent.TimeGenerated;
                        lvi.SubItems.Add(ent.TimeGenerated.ToString("yyyyîNMåédì˙ Héûmï™sïb"));
                        lvt.Items.Add(lvi);
                    }

                    if (ison) {
                        if (evt == null) {
                            evt = new Evt();
                        }
                        evt.dtOn = ent.TimeGenerated;
                        evt.lvion = lvi;
                    }
                    else if (isoff) {
                        if (evt != null) {
                            evt.lvioff = lvi;
                            evt.dtOff = ent.TimeGenerated;
                            alevt.Add(evt);
                        }
                        evt = null;
                    }
                }
            }
        }

        class Evt {
            public DateTime dtOn, dtOff;
            public ListViewItem lvion, lvioff;
        }

        class DTComparer : IComparer {
            int ord;

            public DTComparer(bool asc) {
                ord = asc ? +1 : -1;
            }

            #region IComparer ÉÅÉìÉo

            public int Compare(object xx, object yy) {
                ListViewItem x = (ListViewItem)xx;
                ListViewItem y = (ListViewItem)yy;
                return ord * ((DateTime)x.Tag).CompareTo((DateTime)y.Tag);
            }

            #endregion
        }

        private void lvt_ColumnClick(object sender, ColumnClickEventArgs e) {
            bool asc = 0 != (ModifierKeys & Keys.Shift);
            if (e.Column == 1)
                lvt.ListViewItemSorter = new DTComparer(asc);
        }

        SortedDictionary<DateTime, Evt> dict = new SortedDictionary<DateTime, Evt>();

        private void buttonVerify_Click(object sender, EventArgs e) {
            using (Control wip = WIP.Show(this)) {
                dict.Clear();
                mc.BoldedDates = new DateTime[0];

                Match M = Regex.Match(mtb.Text, "(?<h>\\d+):(?<m>\\d+)");
                if (M.Success) {
                    int h = int.Parse(M.Groups["h"].Value);
                    int m = int.Parse(M.Groups["m"].Value);

                    List<DateTime> aldt = new List<DateTime>();
                    DateTime dt0 = DateTime.MinValue;
                    for (int x = 0; x < alevt.Count; x++) {
                        if (dt0 == DateTime.MinValue)
                            dt0 = alevt[x].dtOn;

                        while (true) {
                            DateTime dt = dt0.Date.AddHours(h).AddMinutes(m);
                            if (alevt[x].dtOn > dt) {
                                dt0 = dt0.AddDays(1);
                                continue;
                            }
                            if (dt <= alevt[x].dtOff) {
                                aldt.Add(dt0.Date);
                                dict[dt0.Date] = alevt[x];
                                dt0 = dt0.AddDays(1);
                            }
                            else break;
                        }
                    }

                    mc.BoldedDates = aldt.ToArray();
                }
            }
        }

        private void mc_DateChanged(object sender, DateRangeEventArgs e) {

        }

        class LVUt {
            public static void Desel(ListView lv) {
                List<int> al = new List<int>();
                foreach (int i in lv.SelectedIndices) al.Add(i);
                foreach (int i in al) {
                    lv.Items[i].Selected = false;
                }
            }
        }

        private void mc_DateSelected(object sender, DateRangeEventArgs e) {
            Evt evt;
            LVUt.Desel(lvt);
            if (dict.TryGetValue(mc.SelectionStart, out evt)) {
                evt.lvioff.Selected = true;
                evt.lvioff.EnsureVisible();
                evt.lvion.Selected = true;
                evt.lvion.Focused = true;
                evt.lvion.EnsureVisible();
            }
        }
    }
}