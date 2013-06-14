using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;

namespace ExplodeICO {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void bOpen_Click(object sender, EventArgs e) {
            if (ofdIco.ShowDialog(this) == DialogResult.OK) {
                LoadIcos(ofdIco.FileNames);
            }
        }

        SynchronizationContext Sync;

        private void LoadIcos(String[] alfp) {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate {
                foreach (String fp in alfp) {
                    using (FileStream fs = File.OpenRead(fp)) {
                        BinaryReader br = new BinaryReader(fs);
                        if (br.ReadUInt16() != 0) return;//throw new InvalidDataException("!0");
                        int ty = br.ReadUInt16();
                        if (ty != 1 && ty != 2) throw new InvalidDataException("ico nor cur");
                        int cnt = br.ReadUInt16();
                        for (int g = 0; g < cnt; g++) {
                            int bWidth = br.ReadByte();
                            int bHeight = br.ReadByte();
                            int bColorCount = br.ReadByte();
                            int bReserved = br.ReadByte();
                            int wPlanes = br.ReadUInt16();
                            int wBitCount = br.ReadUInt16();
                            int dwBytesInRes = br.ReadInt32();
                            int dwImageOffset = br.ReadInt32();
                            if (bWidth == 0) bWidth = 256;
                            if (bHeight == 0) bHeight = 256;
                            Int64 pos = fs.Position;
                            {
                                Bitmap pic;
                                fs.Position = dwImageOffset;
                                int biSize = br.ReadInt32();
                                if (biSize == 0x474E5089) {
                                    fs.Position = dwImageOffset;
                                    pic = new Bitmap(new MemoryStream(br.ReadBytes(dwBytesInRes)));
                                }
                                else {
                                    int biWidth = br.ReadInt32();
                                    int biHeight = br.ReadInt32();
                                    int biPlanes = br.ReadUInt16();
                                    int biBitCount = br.ReadUInt16();
                                    int biCompression = br.ReadInt32();
                                    int biSizeImage = br.ReadInt32();
                                    int biXPelsPerMeter = br.ReadInt32();
                                    int biYPelsPerMeter = br.ReadInt32();
                                    int biClrUsed = br.ReadInt32();
                                    int biClrImportant = br.ReadInt32();

                                    Color[] pal = new Color[(bColorCount == 0) ? ((biBitCount <= 8) ? 1 << biBitCount : 0) : bColorCount];
                                    for (int t = 0; t < pal.Length; t++) {
                                        byte rgbBlue = br.ReadByte();
                                        byte rgbGreen = br.ReadByte();
                                        byte rgbRed = br.ReadByte();
                                        byte rgbReserved = br.ReadByte();
                                        pal[t] = Color.FromArgb((biBitCount == 32) ? rgbReserved : 255, rgbRed, rgbGreen, rgbBlue);
                                    }

                                    pic = new Bitmap(bWidth, bHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                    if (bColorCount == 2) {
                                        int baseXor = 0;
                                        int pitchXor = (((bWidth + 7) / 8) + 3) & (~3);
                                        int cbXor = pitchXor * bHeight;

                                        int baseAnd = cbXor;
                                        int pitchAnd = (((bWidth + 7) / 8) + 3) & (~3);
                                        int cbAnd = pitchAnd * bHeight;

                                        byte[] bits = br.ReadBytes(cbXor + cbAnd);

                                        for (int y = 0; y < pic.Height; y++) {
                                            for (int x = 0; x < pic.Width; x++) {
                                                bool f = ((bits[baseAnd + pitchAnd * (pic.Height - y - 1) + (x >> 3)] >> ((7 - x) & 7)) & 1) != 0;
                                                if (f) {
                                                    pic.SetPixel(x, y, Color.Transparent);
                                                }
                                                else {
                                                    int pix = bits[baseXor + pitchXor * (pic.Height - y - 1) + (x >> 3)];
                                                    int pix2 = (pix >> ((7 - x) & 7)) & 1;
                                                    pic.SetPixel(x, y, pal[pix2]);
                                                }
                                            }
                                        }
                                    }
                                    else if (bColorCount == 16) {
                                        int baseXor = 0;
                                        int pitchXor = (bWidth + 1) / 2;
                                        int cbXor = pitchXor * bHeight;

                                        int baseAnd = cbXor;
                                        int pitchAnd = (((bWidth + 7) / 8) + 3) & (~3);
                                        int cbAnd = pitchAnd * bHeight;

                                        byte[] bits = br.ReadBytes(cbXor + cbAnd);

                                        for (int y = 0; y < pic.Height; y++) {
                                            for (int x = 0; x < pic.Width; x++) {
                                                bool f = ((bits[baseAnd + pitchAnd * (pic.Height - y - 1) + (x >> 3)] >> ((7 - x) & 7)) & 1) != 0;
                                                if (f) {
                                                    pic.SetPixel(x, y, Color.Transparent);
                                                }
                                                else {
                                                    int pix = bits[baseXor + pitchXor * (pic.Height - y - 1) + (x / 2)];
                                                    int pix2 = ((x & 1) == 0) ? pix >> 4 : pix & 15;
                                                    pic.SetPixel(x, y, pal[pix2]);
                                                }
                                            }
                                        }
                                    }
                                    else if (bColorCount == 0 && biBitCount == 24) {
                                        int baseXor = 0;
                                        int pitchXor = (bWidth * 3 + 3) & (~3);
                                        int cbXor = pitchXor * bHeight;

                                        int baseAnd = cbXor;
                                        int pitchAnd = (((bWidth + 7) / 8) + 3) & (~3);
                                        int cbAnd = pitchAnd * bHeight;

                                        byte[] bits = br.ReadBytes(cbXor + cbAnd);

                                        for (int y = 0; y < pic.Height; y++) {
                                            for (int x = 0; x < pic.Width; x++) {
                                                bool f = ((bits[baseAnd + pitchAnd * (pic.Height - y - 1) + (x >> 3)] >> ((7 - x) & 7)) & 1) != 0;
                                                if (f) {
                                                    pic.SetPixel(x, y, Color.Transparent);
                                                }
                                                else {
                                                    int off = baseXor + pitchXor * (bHeight - y - 1) + 3 * x;
                                                    pic.SetPixel(x, y, Color.FromArgb(
                                                        bits[off + 2],
                                                        bits[off + 1],
                                                        bits[off + 0]
                                                        ));
                                                }
                                            }
                                        }
                                    }
                                    else if (bColorCount == 0 && biBitCount == 32) {
                                        int baseXor = 0;
                                        int pitchXor = bWidth * 4;
                                        int cbXor = pitchXor * bHeight;

                                        int baseAnd = cbXor;
                                        int pitchAnd = (((bWidth + 7) / 8) + 3) & (~3);
                                        int cbAnd = pitchAnd * bHeight;

                                        byte[] bits = br.ReadBytes(cbXor + cbAnd);

                                        for (int y = 0; y < pic.Height; y++) {
                                            for (int x = 0; x < pic.Width; x++) {
                                                bool f = ((bits[baseAnd + pitchAnd * (pic.Height - y - 1) + (x >> 3)] >> ((7 - x) & 7)) & 1) != 0;
                                                if (f) {
                                                    pic.SetPixel(x, y, Color.Transparent);
                                                }
                                                else {
                                                    int off = baseXor + pitchXor * (bHeight - y - 1) + 4 * x;
                                                    pic.SetPixel(x, y, Color.FromArgb(
                                                        bits[off + 3],
                                                        bits[off + 2],
                                                        bits[off + 1],
                                                        bits[off + 0]
                                                        ));
                                                }
                                            }
                                        }
                                    }
                                    else if (bColorCount == 0 && biBitCount == 8) {
                                        int baseXor = 0;
                                        int pitchXor = (bWidth + 3) & (~3);
                                        int cbXor = pitchXor * bHeight;

                                        int baseAnd = cbXor;
                                        int pitchAnd = (((bWidth + 7) / 8) + 3) & (~3);
                                        int cbAnd = pitchAnd * bHeight;

                                        byte[] bits = br.ReadBytes(cbXor + cbAnd);

                                        for (int y = 0; y < pic.Height; y++) {
                                            for (int x = 0; x < pic.Width; x++) {
                                                bool f = ((bits[baseAnd + pitchAnd * (pic.Height - y - 1) + (x >> 3)] >> ((7 - x) & 7)) & 1) != 0;
                                                if (f) {
                                                    pic.SetPixel(x, y, Color.Transparent);
                                                }
                                                else {
                                                    int off = baseXor + pitchXor * (bHeight - y - 1) + x;
                                                    pic.SetPixel(x, y, pal[bits[off]]);
                                                }
                                            }
                                        }
                                    }
                                    else throw new InvalidDataException("bColorCount = " + bColorCount + ", biBitCount = " + biBitCount);
                                }
                                Sync.Send(delegate { AddPic(pic, Path.GetFileNameWithoutExtension(fp) + "_" + (1 + g) + "_" + bWidth + "_" + bHeight + "_" + Getbpp(pic.PixelFormat)); }, null);
                            }
                            fs.Position = pos;
                        }
                    }
                }
            };
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private int Getbpp(PixelFormat pixelFormat) {
            switch (pixelFormat) {
                case PixelFormat.Format1bppIndexed: return 1;
                case PixelFormat.Format4bppIndexed: return 4;
                case PixelFormat.Format8bppIndexed: return 8;
                case PixelFormat.Format24bppRgb: return 24;
                case PixelFormat.Format32bppArgb: return 32;
            }
            return 0;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) MessageBox.Show(this, e.Error.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddPic(Bitmap pic, String fn) {
            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            pb.Image = pic;
            pb.Parent = flpICO;

            pb.Name = fn;
        }

        private void flpICO_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None);
        }

        private void flpICO_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = (String[])e.Data.GetData(DataFormats.FileDrop);
            if (alfp != null) LoadIcos(alfp);
        }

        private void Form1_Load(object sender, EventArgs e) {
            Sync = SynchronizationContext.Current;
        }

        private void bSaveAll_Click(object sender, EventArgs e) {
            String dir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(dir);
            foreach (PictureBox pb in flpICO.Controls.OfType<PictureBox>()) {
                pb.Image.Save(Path.Combine(dir, pb.Name + ".png"));
            }
            Process.Start(dir);
        }

        private void bClear_Click(object sender, EventArgs e) {
            flpICO.Controls.Clear();
        }
    }
}
