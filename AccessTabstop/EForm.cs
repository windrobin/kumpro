using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using LibROT;
using System.Reflection;
using Microsoft.VisualBasic;

namespace AccessTabstop {
    public partial class EForm : Form {
        public EForm() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e) {
            toolStripDropDownButton1.DropDownItems.Clear();
            Moniker[] keys = ROTUtil.EnumRunning();
            foreach (Moniker key in keys) {
                Microsoft.Office.Interop.Access.Application app = (Microsoft.Office.Interop.Access.Application)ROTUtil.GetObjectAs(key, typeof(Microsoft.Office.Interop.Access.Application));
                if (app != null) {
                    ToolStripMenuItem subItem = new ToolStripMenuItem(key.GetDisplayName());
                    toolStripDropDownButton1.DropDownItems.Add(subItem);

                    int cx = app.Forms.Count;
                    for (int x = 0; x < cx; x++) {
                        Microsoft.Office.Interop.Access.Form form = app.Forms[x];
                        ToolStripItem subSubItem = subItem.DropDownItems.Add(form.Name);
                        subSubItem.Click += new EventHandler(subSubItem_Click);
                        subSubItem.Tag = form;
                    }
                }
            }
        }

        void subSubItem_Click(object sender, EventArgs e) {
            Microsoft.Office.Interop.Access.Form form = (Microsoft.Office.Interop.Access.Form)((ToolStripItem)sender).Tag;
            selectForm(form);
        }

        void refreshTabIndex() {
            foreach (Control ctrl in panelMain.Controls) {
                int tabIndex = Convert.ToInt32(COMUtil.GetProperty(ctrl.Tag, "TabIndex", -1));
                if (tabIndex < 0) continue;
                ctrl.Text = "" + tabIndex;
                ctrl.TabIndex = tabIndex;
            }
        }

        void selectForm(Microsoft.Office.Interop.Access.Form form) {
            panelMain.Controls.Clear();
            selec = null;

            if (form.CurrentView != 0) {
                if (MessageBox.Show("選択されたフォームは、現在デザインビューの為に起動されていません。デザインビューに切り替えないと、変更は保存されません。\n\nいますぐ切り替えますか。", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                    string name = form.Name;
                    form.Application.DoCmd.OpenForm(
                        form.Name,
                        Microsoft.Office.Interop.Access.AcFormView.acDesign,
                        null,
                        null,
                        Microsoft.Office.Interop.Access.AcFormOpenDataMode.acFormPropertySettings,
                        Microsoft.Office.Interop.Access.AcWindowMode.acWindowNormal,
                        null
                        );
                    form = form.Application.Forms[name];
                }
            }

            List<Control> box = new List<Control>();
            SortedDictionary<int, Point> sec2pos = new SortedDictionary<int, Point>();
            if (true) {
                Microsoft.Office.Interop.Access._Section sec;
                int si, y = 0;

                try {
                    sec = form.get_Section(si = (int)Microsoft.Office.Interop.Access.AcSection.acHeader);
                    sec2pos[si] = new Point(0, y);
                    y += sec.Height;
                }
                catch (COMException err) {
                    if (err.ErrorCode == -2146825826) { // {"セクション番号の指定が正しくありません。"}
                    }
                    else {
                        throw;
                    }
                }

                sec = form.get_Section(si = (int)Microsoft.Office.Interop.Access.AcSection.acDetail);
                sec2pos[si] = new Point(0, y);
                y += sec.Height;

                try {
                    sec = form.get_Section(si = (int)Microsoft.Office.Interop.Access.AcSection.acFooter);
                    sec2pos[si] = new Point(0, y);
                    y += sec.Height;
                }
                catch (COMException err) {
                    if (err.ErrorCode == -2146825826) { // {"セクション番号の指定が正しくありません。"}
                    }
                    else {
                        throw;
                    }
                }
            }

            int cz = form.Controls.Count;
            for (int z = 0; z < cz; z++) {
                Microsoft.Office.Interop.Access.Control src = (Microsoft.Office.Interop.Access.Control)form.Controls[z];
                Point pos = sec2pos[Convert.ToInt32(COMUtil.GetProperty(src, "Section"))];
                int x = Convert.ToInt32(COMUtil.GetProperty(src, "Left")) + pos.X;
                int y = Convert.ToInt32(COMUtil.GetProperty(src, "Top")) + pos.Y;
                int cx = Convert.ToInt32(COMUtil.GetProperty(src, "Width"));
                int cy = Convert.ToInt32(COMUtil.GetProperty(src, "Height"));
                int tabIndex = Convert.ToInt32(COMUtil.GetProperty(src, "TabIndex", -1));
                Label c = new Label();
                c.Location = new Point(MeasureUtil.PosToPix(x), MeasureUtil.PosToPix(y));
                c.Size = new Size(MeasureUtil.PosToPix(cx), MeasureUtil.PosToPix(cy));
                c.Text = "" + tabIndex;
                c.TextAlign = ContentAlignment.MiddleCenter;
                c.Visible = true;
                c.BorderStyle = BorderStyle.FixedSingle;
                c.Tag = src;
                c.TabIndex = (tabIndex < 0) ? 10000 : tabIndex;
                c.MouseDown += new MouseEventHandler(c_MouseDown);
                if (tabIndex >= 0) c.BackColor = clrAvail;
                box.Insert(0, c);
            }
            panelMain.Controls.AddRange(box.ToArray());
        }

        Color clrAvail = Color.Azure;
        Color clrSel = Color.BlueViolet;

        Label selec = null;

        void c_MouseDown(object sender, MouseEventArgs e) {
            Label label = (Label)sender;
            string text = label.Text;
            int i = int.Parse(text);
            if (i < 0) return;

            if (startTabIndex < 0) {
                if (selec != null) {
                    int tiSrc = Convert.ToInt32(COMUtil.GetProperty(selec.Tag, "TabIndex"));
                    int tiDst = Convert.ToInt32(COMUtil.GetProperty(label.Tag, "TabIndex"));

                    int tiMin = Math.Min(tiSrc, tiDst);
                    int tiMax = Math.Max(tiSrc, tiDst);

                    SortedDictionary<int, Control> tab2ctrl = new SortedDictionary<int, Control>();

                    foreach (Control ctrl in panelMain.Controls) {
                        if (ctrl.TabIndex > 9999) continue;
                        tab2ctrl[Convert.ToInt32(COMUtil.GetProperty(ctrl.Tag, "TabIndex"))] = ctrl;
                    }

                    COMUtil.SetProperty(tab2ctrl[tiMax].Tag, "TabIndex", tiMin);
                    COMUtil.SetProperty(tab2ctrl[tiMin].Tag, "TabIndex", tiMax);

                    selec.BackColor = clrAvail;
                    selec = null;

                    refreshTabIndex();
                }
                else {
                    label.BackColor = clrSel;
                    selec = label;
                }
            }
            else {
                COMUtil.SetProperty(label.Tag, "TabIndex", startTabIndex);
                startTabIndex++;
                toolStripButtonIndexIndic.Text = "次インデックス=" + startTabIndex;
                refreshTabIndex();
            }
        }

        int startTabIndex = -1;

        private void toolStripButtonAssign_Click(object sender, EventArgs e) {
            string str = Interaction.InputBox("次のインデックスを入力してください", "", "" + startTabIndex, -1, -1);
            if (str.Length < 1) return;

            startTabIndex = int.Parse(str);

            toolStripButtonAssign.Checked = true;
            toolStripButtonSwap.Checked = false;
            toolStripButtonIndexIndic.Text = "次インデックス=" + startTabIndex;
        }

        private void toolStripButtonSwap_Click(object sender, EventArgs e) {
            startTabIndex = -1;

            toolStripButtonAssign.Checked = false;
            toolStripButtonSwap.Checked = true;
            toolStripButtonIndexIndic.Text = "インデックス";
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e) {
            MessageBox.Show("Access 2003 フォームのタブインデックス 振り直し作業　を支援します。");
        }

        private void toolStripButtonHideNeg_Click(object sender, EventArgs e) {
            bool hide = !toolStripButtonHideNeg.Checked;
            toolStripButtonHideNeg.Checked = hide;
            reflectHide();
        }

        private void toolStripButtonHideFrm_Click(object sender, EventArgs e) {
            bool nofrm = !toolStripButtonHideFrm.Checked;
            toolStripButtonHideFrm.Checked = nofrm;
            reflectHide();
        }

        void reflectHide() {
            bool hide = toolStripButtonHideNeg.Checked;
            bool nofrm = toolStripButtonHideFrm.Checked;

            foreach (Control c in panelMain.Controls) {
                if ((9999 < c.TabIndex) && hide || (CtrlTyUtil.IsControlFrame(c) && nofrm)) {
                    c.Hide();
                }
                else {
                    c.Show();
                }
            }
        }
    }

    class CtrlTyUtil {
        public static bool IsControlFrame(Control c) {
            Microsoft.Office.Interop.Access.Control o = (Microsoft.Office.Interop.Access.Control)c.Tag;
            if (o is Microsoft.Office.Interop.Access.OptionGroup)
                return true;
            return false;
        }
    }

    class MeasureUtil {
        public static int PosToPix(int x) {
            return x / 14;
        }
    }

    class COMUtil {
        public static void SetProperty(object what, string name, object val) {
            what.GetType().InvokeMember(name, BindingFlags.SetProperty, null, what, new object[] { val });
        }
        public static object GetProperty(object what, string name) {
            return what.GetType().InvokeMember(name, BindingFlags.GetProperty, null, what, new object[] { });
        }
        public static object GetProperty(object what, string name, object errVal) {
            try {
                return what.GetType().InvokeMember(name, BindingFlags.GetProperty, null, what, new object[] { });
            }
            catch (TargetInvocationException) {
                return errVal;
            }
            catch (COMException) {
                return errVal;
            }
        }
    }
}
