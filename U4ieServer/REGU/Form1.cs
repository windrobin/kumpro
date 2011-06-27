using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using REGU.Properties;
using TaskScheduler;
using System.IO;
using System.Diagnostics;

namespace REGU {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void bSave_Click(object sender, EventArgs e) {
            Settings.Default.Save();
            MessageBox.Show(this, "•Û‘¶‚µ‚Ü‚µ‚½", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        String TN { get { return "U4ieServer"; } }

        private void bREG_Click(object sender, EventArgs e) {
            ScheduledTasks st = new ScheduledTasks();
            st.DeleteTask(TN);
            Task task = st.CreateTask(TN);
            task.ApplicationName = Path.Combine(Application.StartupPath, "U4ieServer.exe");
            task.Parameters = " \"" + textBox1.Text + "\" \"" + comboBox1.Text + "\" \"" + textBox2.Text + "\" ";
            task.Triggers.Add(new DailyTrigger(10, 0));
            task.Save();
            task.DisplayForEdit();
        }

        private void bREG_DisplayStyleChanged(object sender, EventArgs e) {

        }

        private void bListTasks_Click(object sender, EventArgs e) {
            Process.Start("control.exe", " schedtasks");
        }
    }
}