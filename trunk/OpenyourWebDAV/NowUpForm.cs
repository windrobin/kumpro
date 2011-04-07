using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OpenyourWebDAV {
    public partial class NowUpForm : Form, INoticeIO {
        EventWaitHandle cancel;

        public NowUpForm(BackgroundWorker bwIO, EventWaitHandle cancel) {
            this.bwIO = bwIO;
            this.cancel = cancel;

            InitializeComponent();
        }

        BackgroundWorker bwIO;

        private void NowUpForm_Load(object sender, EventArgs e) {
            bwIO.RunWorkerAsync();
            bwIO.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwIO_RunWorkerCompleted);
        }

        void bwIO_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Close();
        }

        private void NowUpForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (bwIO.WorkerSupportsCancellation && !bwIO.CancellationPending)
                bwIO.CancelAsync();
            if (cancel != null)
                cancel.Set();
            if (bwIO.IsBusy) {
                if (e.CloseReason == CloseReason.UserClosing) {
                    e.Cancel = true;
                    return;
                }
            }
        }

        #region INoticeIO メンバ

        public void Notice(string fp, long cur, long max) {
            Invoke((System.Threading.ThreadStart)delegate() {
                lfp.Text = fp;
                while (max > int.MaxValue) {
                    max >>= 1;
                    cur >>= 1;
                }
                pbIO.Maximum = Convert.ToInt32(max);
                pbIO.Value = Convert.ToInt32(cur);
            });
        }

        #endregion
    }
}
