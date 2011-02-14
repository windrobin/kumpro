using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TaskScheduler;
using System.IO;
using System.Diagnostics;

namespace SendHostAd {
    public partial class NForm : Form {
        public NForm() {
            InitializeComponent();
        }

        private void bREG_Click(object sender, EventArgs e) {
            E.SetError(tbUri,null);
            if (tbUri.TextLength == 0) { E.SetError(tbUri, "“ü—Í‚µ‚Ä‚­‚¾‚³‚¢"); return; }

            ScheduledTasks st = new ScheduledTasks();
            st.DeleteTask(tbTask.Text);
            Task task = st.CreateTask(tbTask.Text);
            task.ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            task.Flags = TaskFlags.SystemRequired;
            task.MaxRunTime = TimeSpan.FromMinutes(30);
            task.MaxRunTimeLimited = true;
            task.Parameters = " \"" + tbUri.Text + "\"";
            task.WorkingDirectory = Application.StartupPath;

            if (rbH6.Checked) {
                DailyTrigger t = new DailyTrigger(9, 0);

                t.DaysInterval = 1;
                t.DurationMinutes = 24 * 60;
                t.IntervalMinutes = 6 * 60;

                task.Triggers.Add(t);
            }
            else if (rbD1.Checked) {
                DailyTrigger t = new DailyTrigger(9, 0);

                t.DaysInterval = 1;

                task.Triggers.Add(t);
            }
            else if (rbD3.Checked) {
                DailyTrigger t = new DailyTrigger(9, 0);

                t.DaysInterval = 3;

                task.Triggers.Add(t);
            }

            task.Save(tbTask.Text);
            task.DisplayForEdit();
        }

        private void bTL_Click(object sender, EventArgs e) {
            Process.Start("control.exe", "schedtasks");
        }

        private void NForm_Load(object sender, EventArgs e) {

        }
    }
}