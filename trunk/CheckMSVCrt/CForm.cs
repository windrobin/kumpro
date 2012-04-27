using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace CheckMSVCrt {
    public partial class CForm : Form {
        public CForm() {
            InitializeComponent();
        }

        [DllImport("msi.dll", SetLastError = true)]
        static extern INSTALLSTATE MsiQueryProductState(string product);

        public enum INSTALLSTATE {
            NotUsed = -7,  // component disabled
            BadConfig = -6,  // configuration data corrupt
            Incomplete = -5,  // installation suspended or in progress
            Sourceabsent = -4,  // run from source, source is unavailable
            MoreData = -3,  // return buffer overflow
            InvalidArg = -2,  // invalid function argument
            Unknown = -1,  // unrecognized product or feature
            Broken = 0,  // broken
            Advertised = 1,  // advertised feature
            Removed = 1,  // component being removed (action state, not settable)
            Absent = 2,  // uninstalled (or action state absent but clients remain)
            Local = 3,  // installed on local drive
            Source = 4,  // run from source, CD or net
            Default = 5,  // use default, local or source
        }

        private void CForm_Load(object sender, EventArgs e) {
            bRefresh.PerformClick();
        }

        private void T(String id, String product) {
            T(id, null, product, null);
        }

        private void T(String id, String ver, String product, String url) {
            INSTALLSTATE ist = MsiQueryProductState(id);
            ListViewItem lvi = new ListViewItem(product);
            lvi.SubItems.Add((ver ?? "").TrimEnd('?'));
            lvi.SubItems.Add(id);
            lvi.SubItems.Add(ist.ToString());
            if (url != null) lvi.SubItems.Add(url);
            switch (ist) {
                case INSTALLSTATE.Default:
                    lvi.ImageKey = "Y";
                    lvi.Font = new Font(lvi.Font, FontStyle.Bold);
                    lvi.BackColor = Color.FromKnownColor(KnownColor.Info);
                    lvi.ForeColor = Color.FromKnownColor(KnownColor.InfoText);
                    break;
                default:
                    lvi.ImageKey = "N";
                    break;
            }
            lvR.Items.Add(lvi);
        }

        private void bRefresh_Click(object sender, EventArgs e) {
            lvR.Items.Clear();

            // http://stackoverflow.com/questions/203195/how-to-detect-vc-2008-redistributable
            T("{6AFCA4E1-9B78-3640-8F72-A7BF33448200}", "VC++2008: 32bit 30729.01");

            T("{A49F249F-0C91-497F-86DF-B2585E8E76B7}", "8.0.50727.42", "VC 8.0 (x86)", "http://www.microsoft.com/downloads/details.aspx?familyid=32BC1BEE-A3F9-4C13-9C99-220B62A191EE");
            T("{6E8E85E8-CE4B-4FF5-91F7-04999C9FAE6A}", "8.0.50727.42?", "VC 8.0 (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=90548130-4468-4bbc-9673-d6acabd5d13b");
            T("{03ED71EA-F531-4927-AABD-1C31BCE8E187}", "8.0.50727.42?", "VC 8.0 (ia64)", "http://www.microsoft.com/downloads/details.aspx?familyid=526BF4A7-44E6-4A91-B328-A4594ADB70E5");

            T("{7299052B-02A4-4627-81F2-1818DA5D550D}", "8.0.50727.762", "VC 8.0 SP1 (x86)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=200B2FD9-AE1A-4A14-984D-389C36F85647");
            T("{071C9B48-7C32-4621-A0AC-3F809523288F}", "8.0.50727.762?", "VC 8.0 SP1 (x64)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=EB4EBE2D-33C0-4A47-9DD4-B9A6D7BD44DA");
            T("{0F8FB34E-675E-42ED-850B-29D98C2ECE08}", "8.0.50727.762?", "VC 8.0 SP1 (ia64)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=747AAD7C-5D6B-4432-8186-85DF93DD51A9");

            T("{837B34E3-7C30-493C-8F6A-2B0F04E2912C}", "8.0.50727.4053", "VC 8.0 SP1 ATL Patch (x86)", "http://www.microsoft.com/downloads/details.aspx?familyid=766A6AF7-EC73-40FF-B072-9112BAB119C2");
            T("{6CE5BAE9-D3CA-4B99-891A-1DC6C118A5FC}", "8.0.50727.4053?", "VC 8.0 SP1 ATL Patch (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=766A6AF7-EC73-40FF-B072-9112BAB119C2");
            T("{85025851-A784-46D8-950D-05CB3CA43A13}", "8.0.50727.4053?", "VC 8.0 SP1 ATL Patch (ia64)", "http://www.microsoft.com/downloads/details.aspx?familyid=766A6AF7-EC73-40FF-B072-9112BAB119C2");

            T("{710F4C1C-CC18-4C49-8CBF-51240C89A1A2}", "8.0.50727.6195", "VC 8.0 SP1 MFCLOC Patch (x86)", "http://www.microsoft.com/downloads/details.aspx?familyid=AE2E1A40-7B45-4FE9-A20F-2ED2923ACA62");
            T("{AD8A2FA1-06E7-4B0D-927D-6E54B3D31028}", "8.0.50727.6195?", "VC 8.0 SP1 MFCLOC Patch (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=AE2E1A40-7B45-4FE9-A20F-2ED2923ACA62");
            T("{C2F60BDA-462A-4A72-8E4D-CA431A56E9EA}", "8.0.50727.6195?", "VC 8.0 SP1 MFCLOC Patch (ia64)", "http://www.microsoft.com/downloads/details.aspx?familyid=AE2E1A40-7B45-4FE9-A20F-2ED2923ACA62");

            T("{FF66E9F6-83E7-3A3E-AF14-8DE9A809A6A4}", "9.0.21022.8", "VC 9.0 (x86)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=9b2da534-3e03-4391-8a4d-074b9f2bc1bf");
            T("{350AA351-21FA-3270-8B7A-835434E766AD}", "9.0.21022.8?", "VC 9.0 (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=bd2a6171-e2d6-4230-b809-9a8d7548c1b6");
            T("{2B547B43-DB50-3139-9EBE-37D419E0F5FA}", "9.0.21022.8?", "VC 9.0 (ia64)", "http://www.microsoft.com/downloads/details.aspx?familyid=461f404b-d0a9-4c69-8086-30c604f885f5");

            T("{9A25302D-30C0-39D9-BD6F-21E6EC160475}", "9.0.30729.1", "VC 9.0 SP1 (x86)", "http://www.microsoft.com/downloads/details.aspx?familyid=A5C84275-3B97-4AB7-A40D-3802B2AF5FC2");
            T("{8220EEFE-38CD-377E-8595-13398D740ACE}", "9.0.30729.1?", "VC 9.0 SP1 (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=BA9257CA-337F-4B40-8C14-157CFDFFEE4E");
            T("{5827ECE1-AEB0-328E-B813-6FC68622C1F9}", "9.0.30729.1?", "VC 9.0 SP1 (ia64)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=DCC211E6-AB82-41D6-8DEC-C79937393FE8");

            T("{1F1C2DFC-2D24-3E06-BCB8-725134ADF989}", "9.0.30729.4148", "VC 9.0 SP1 ATL (x86)", "http://www.microsoft.com/downloads/details.aspx?familyid=2051A0C1-C9B5-4B0A-A8F5-770A549FD78C");
            T("{4B6C7001-C7D6-3710-913E-5BC23FCE91E6}", "9.0.30729.4148?", "VC 9.0 SP1 ATL (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=2051A0C1-C9B5-4B0A-A8F5-770A549FD78C");
            T("{977AD349-C2A8-39DD-9273-285C08987C7B}", "9.0.30729.4148?", "VC 9.0 SP1 ATL (ia64)", "http://www.microsoft.com/downloads/details.aspx?familyid=2051A0C1-C9B5-4B0A-A8F5-770A549FD78C");

            T("{9BE518E6-ECC6-35A9-88E4-87755C07200F}", "9.0.30729.6161?", "VC 9.0 SP1 MFC (x86)", "http://www.microsoft.com/en-us/download/details.aspx?displaylang=en&id=26368");
            T("{5FCE6D76-F5DC-37AB-B2B8-22AB8CEDB1D4}", "9.0.30729.6161?", "VC 9.0 SP1 MFC (x64)", "http://www.microsoft.com/en-us/download/details.aspx?displaylang=en&id=26368");
            T("{515643D1-4E9E-342F-A75A-D1F16448DC04}", "9.0.30729.6161?", "VC 9.0 SP1 MFC (ia64)", "http://www.microsoft.com/en-us/download/details.aspx?displaylang=en&id=26368");

            // http://blogs.msdn.com/b/astebner/archive/2009/01/29/9384143.aspx


            // http://blogs.msdn.com/b/astebner/archive/2010/05/05/10008146.aspx
            T("{196BB40D-1578-3D01-B289-BEFC77A11A1E}", "10.0.30319.1", "VC++ 2010 (x86)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=a7b7a05e-6de6-4d3a-a423-37bf0912db84");
            T("{DA5E371C-6333-3D8A-93A4-6FD5B20BCC6E}", "10.0.30319.1?", "VC++ 2010 (x64)", "http://www.microsoft.com/downloads/details.aspx?familyid=BD512D9E-43C8-4655-81BF-9350143D5867");
            T("{C1A35166-4301-38E9-BA67-02823AD72A1B}", "10.0.30319.1?", "VC++ 2010 (ia64)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=1a2df53a-d8f4-4bfe-be35-152c5d3d0f82");

            T("{F0C3E5D1-1ADE-321E-8167-68EF0DE699A5}", "10.0.40219.1", "VC++ 2010 SP1 (x86)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=C32F406A-F8FC-4164-B6EB-5328B8578F03");
            T("{1D8E6291-B0D5-35EC-8441-6616F567A0F7}", "10.0.40219.1?", "VC++ 2010 SP1 (x64)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=C68CCBB6-75EF-4C9D-A326-879EAB4FCDF8");
            T("{88C73C1C-2DE5-3B01-AFB8-B46EF4AB41CD}", "10.0.40219.1?", "VC++ 2010 SP1 (ia64)", "http://www.microsoft.com/downloads/details.aspx?FamilyID=647A8A36-A058-41A4-88B2-D4A05CC0B6B3");

        }

        class CSVw {
            StringWriter wr = new StringWriter();

            String camma = ",";
            String delim = "\"";
            String enter = "\r\n";

            int x = 0, y = 0;

            public void Write(String s) {
                if (x != 0)
                    wr.Write(camma);
                if (s.IndexOfAny((delim + camma + enter).ToCharArray()) < 0) {
                    wr.Write(s);
                }
                else {
                    wr.Write(delim + s.Replace(delim, delim + delim) + delim);
                }
                x++;
            }

            public void EOL() {
                x = 0;
                y++;
                wr.Write(enter);
            }

            public override string ToString() {
                return wr.ToString();
            }
        }

        private void bCopyCsv_Click(object sender, EventArgs e) {
            CSVw w = new CSVw();
            foreach (ListViewItem lvi in lvR.Items) {
                w.Write(lvi.ImageKey);
                foreach (ListViewItem.ListViewSubItem i in lvi.SubItems) {
                    w.Write(i.Text);
                }
                w.EOL();
            }

            Clipboard.SetText(w.ToString());

            MessageBox.Show(this, "Done.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(ttLink.GetToolTip((Control)sender));
        }

        private void lvR_ItemActivate(object sender, EventArgs e) {
            foreach (ListViewItem lvi in lvR.SelectedItems) {
                if (lvi.SubItems.Count > chDL.Index) {
                    ListViewItem.ListViewSubItem si = lvi.SubItems[chDL.Index];
                    mOpen.Items.Clear();
                    ToolStripMenuItem mi = new ToolStripMenuItem(si.Text, null);
                    mi.Enabled = false;
                    mOpen.Items.Add(mi);
                    mOpen.Items.Add(new ToolStripSeparator());
                    mOpen.Items.Add("&Open the site", il.Images["Web"], delegate(object sender2, EventArgs e2) {
                        Process.Start(si.Text);
                    });
                    mOpen.Items.Add("&Copy the link", bCopyCsv.Image, delegate(object sender2, EventArgs e2) {
                        Clipboard.SetText(si.Text);
                        MessageBox.Show(this, "Copied.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                    mOpen.Show(Cursor.Position);
                }
                break;
            }
        }

        private void bAppwiz_Click(object sender, EventArgs e) {
            Process.Start("control.exe", "appwiz.cpl");
        }
    }
}