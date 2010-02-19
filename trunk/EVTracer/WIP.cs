using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EVTracer {
    public partial class WIP : UserControl {
        public WIP() {
            InitializeComponent();
        }

        public static WIP Show(Control parent) {
            WIP o = new WIP();
            o.Location = Point.Empty;
            o.Size = parent.ClientSize;
            o.Parent = parent;
            o.Show();
            o.BringToFront();
            o.Update();
            parent.Update();
            return o;
        }
    }
}
