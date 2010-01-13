using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using RunTimeViser2.Properties;

namespace RunTimeViser2 {
    public class WIP {
        public static Control Show(Control parent) {
            Control o = new Control();
            o.Location = Point.Empty;
            o.Size = parent.ClientSize;
            o.Parent = parent;
            o.BackgroundImage = Resources.ExpirationHS;
            o.BackgroundImageLayout = ImageLayout.Center;
            o.BackColor = Color.WhiteSmoke;
            o.Show();
            o.BringToFront();
            return o;
        }
    }
}
