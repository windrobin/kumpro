using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using FindBrokenTasks.Properties;
using System.IO;

namespace FindBrokenTasks {
    public partial class FForm : Form {
        public FForm() {
            InitializeComponent();
        }

        private void FForm_Load(object sender, EventArgs e) {
            Text += " " + Application.ProductVersion;
            Check();
        }

        private void Check() {
            flpE.Controls.Clear();
            var ts = new TaskService();
            int n = Walk(ts.RootFolder);
            if (n == 0) {
                PictureBox pb = new PictureBox();
                pb.Image = Resources.info;
                pb.SizeMode = PictureBoxSizeMode.AutoSize;
                pb.Parent = flpE;
                pb.Anchor = AnchorStyles.Left;
                Label la = new Label();
                la.AutoSize = true;
                la.Text = "問題は認められませんでした。";
                la.Parent = flpE;
                la.Anchor = AnchorStyles.Left;
            }
        }

        private int Walk(TaskFolder fo) {
            int n = 0;
            foreach (var t in fo.Tasks) {
                try {
                    String s = t.Xml;
                }
                catch (Exception err) {
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    PictureBox pb = new PictureBox();
                    pb.Image = Resources.warn;
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    pb.Parent = flp;
                    pb.Anchor = AnchorStyles.Left;

                    String fp = Environment.SystemDirectory + "\\Tasks\\" + t.Path.TrimStart('\\');

                    LinkLabel ll = new LinkLabel();
                    ll.AutoSize = true;
                    ll.Text = t.Path;
                    ll.Parent = flp;
                    ll.Anchor = AnchorStyles.Left;
                    ll.LinkClicked += delegate {
                        Process.Start("explorer.exe", " /select,\"" + fp + "\"");
                    };

                    {
                        Button b = new Button();
                        b.Text = "例外情報";
                        b.AutoSize = true;
                        b.Anchor = AnchorStyles.Left;
                        b.Parent = flp;
                        b.Click += delegate {
                            MessageBox.Show(this, err.Message);
                        };
                    }
                    {
                        Button b = new Button();
                        b.Text = "デスクトップへ複製する";
                        b.AutoSize = true;
                        b.Anchor = AnchorStyles.Left;
                        b.Parent = flp;
                        b.Click += delegate {
                            File.Copy(fp, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.GetFileName(fp)));
                            MessageBox.Show(this, "完了");
                        };
                    }
                    {
                        Button b = new Button();
                        b.Text = "デスクトップへ移動する";
                        b.AutoSize = true;
                        b.Anchor = AnchorStyles.Left;
                        b.Parent = flp;
                        b.Click += delegate {
                            if (MessageBox.Show(this, "元の場所からは無くなります。", "", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                                File.Move(fp, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.GetFileName(fp)));
                                MessageBox.Show(this, "完了");
                            }
                        };
                    }

                    flp.AutoSize = true;
                    flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    flp.Parent = flpE;

                    flpE.SetFlowBreak(flp, true);
                    n++;
                }
            }
            foreach (var sf in fo.SubFolders)
                n += Walk(sf);
            return n;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Check();
        }
    }
}
