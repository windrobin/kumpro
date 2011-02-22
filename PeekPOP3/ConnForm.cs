using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ShachoUndelivered;
using PeekPOP3.Properties;

namespace PeekPOP3 {
    public partial class ConnForm : Form, IConnInfo {
        public ConnForm() {
            InitializeComponent();
        }

        public POP3 pop3 = null;

        private void bOk_Click(object sender, EventArgs e) {
            pop3 = new POP3(Host, Port, U, P);

            DialogResult = DialogResult.OK;
            Close();
        }

        #region IConnInfo ÉÅÉìÉo

        public string Host {
            get { return tbPOP3.Text; }
        }

        public int Port {
            get { return (int)numPort.Value; }
        }

        public string U {
            get { return tbU.Text; }
        }

        public string P {
            get { return tbP.Text; }
        }

        #endregion

        private void bSave_Click(object sender, EventArgs e) {
            Settings.Default.Save();
            MessageBox.Show(this, "ï€ë∂ÇµÇ‹ÇµÇΩÅB", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public interface IConnInfo {
        String Host { get; }
        int Port { get; }
        String U { get; }
        String P { get; }
    }
}
