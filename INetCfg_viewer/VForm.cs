using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace INetCfg_viewer {
    public partial class VForm : Form {
        public VForm() {
            InitializeComponent();
        }

        private void VForm_Load(object sender, EventArgs e) {
            pnetcfg.Initialize(IntPtr.Zero);

            rbGUID_DEVCLASS_NET_CheckedChanged(sender, e);
        }

        INetCfg pnetcfg = (INetCfg)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5B035261-40F9-11D1-AAEC-00805FC1270E"), true));

        private void EnumComponents(Guid guid) {
            // http://social.msdn.microsoft.com/Forums/en/csharpgeneral/thread/b24acc81-7fb6-43e5-9715-0a74f40163dd

            IEnumNetCfgComponent penumComponent = pnetcfg.EnumComponents(guid);
            ds.Component.Rows.Clear();
            foreach (var co in EUt.All(penumComponent)) {
                String PnpDevNodeId = null;
                try {
                    PnpDevNodeId = co.GetPnpDevNodeId();
                }
                catch (Exception) { }

                ds.Component.AddComponentRow(
                    co.GetDisplayName(),
                    co.GetHelpText(),
                    co.GetId(),
                    co.GetCharacteristics(),
                    co.GetInstanceGuid(),
                    PnpDevNodeId,
                    co.GetClassGuid(),
                    co.GetBindName()
                    );
            }

            gv.AutoResizeColumns();
        }

        class EUt {
            internal static IEnumerable<INetCfgComponent> All(IEnumNetCfgComponent penum) {
                int hr;
                INetCfgComponent one;
                uint celtFetched;
                while (0 == (hr = penum.Next(1, out one, out celtFetched)) && celtFetched == 1) {
                    yield return one;
                }
            }

            internal static IEnumerable<INetCfgBindingPath> All(IEnumNetCfgBindingPath penum) {
                int hr;
                INetCfgBindingPath one;
                uint celtFetched;
                while (0 == (hr = penum.Next(1, out one, out celtFetched)) && celtFetched == 1) {
                    yield return one;
                }
            }

            internal static IEnumerable<INetCfgBindingInterface> All(IEnumNetCfgBindingInterface penum) {
                int hr;
                INetCfgBindingInterface one;
                uint celtFetched;
                while (0 == (hr = penum.Next(1, out one, out celtFetched)) && celtFetched == 1) {
                    yield return one;
                }
            }
        }

        private void rbGUID_DEVCLASS_NET_CheckedChanged(object sender, EventArgs e) {
            if (rbGUID_DEVCLASS_NET.Checked) EnumComponents(GUID_DEVCLASS.GUID_DEVCLASS_NET);
            if (rbGUID_DEVCLASS_NETTRANS.Checked) EnumComponents(GUID_DEVCLASS.GUID_DEVCLASS_NETTRANS);
            if (rbGUID_DEVCLASS_NETSERVICE.Checked) EnumComponents(GUID_DEVCLASS.GUID_DEVCLASS_NETSERVICE);
            if (rbGUID_DEVCLASS_NETCLIENT.Checked) EnumComponents(GUID_DEVCLASS.GUID_DEVCLASS_NETCLIENT);
        }

        const uint EBP_ABOVE = 1;
        const uint EBP_BELOW = 2;

        private void gv_RowEnter(object sender, DataGridViewCellEventArgs e) {
            var row = gv.Rows[e.RowIndex];
            String id = (String)row.Cells[cId.Index].Value;
            var bind = (INetCfgComponentBindings)pnetcfg.FindComponent(id);
            cbPath.Items.Clear();
            {
                foreach (var path in EUt.All(bind.EnumBindingPaths(EBP_ABOVE))) {
                    cbPath.Items.Add(new DevPath { Path = path });
                }
                cbPath.Items.Add("--- Me ---");
                foreach (var path in EUt.All(bind.EnumBindingPaths(EBP_BELOW))) {
                    cbPath.Items.Add(new DevPath { Path = path });
                }
            }
            lvP.Items.Clear();
        }

        class DevPath {
            internal INetCfgBindingPath Path { get; set; }

            public override string ToString() {
                return Path.GetPathToken();
            }
        }

        private void cbPath_SelectedIndexChanged(object sender, EventArgs e) {
            lvP.Items.Clear();
            DevPath path = cbPath.SelectedItem as DevPath;
            if (path == null) return;
            int y = 0;
            foreach (var pif in EUt.All(path.Path.EnumBindingInterfaces())) {
                if (y == 0) {
                    var c0 = pif.GetUpperComponent();
                    var lvi = lvP.Items.Add(c0.GetDisplayName());
                    lvi.ImageKey = "C";
                }

                {
                    var lvi = lvP.Items.Add(pif.GetName());
                    lvi.ImageKey = "ES";
                }

                {
                    var c1 = pif.GetLowerComponent();
                    var lvi = lvP.Items.Add(c1.GetDisplayName());
                    lvi.ImageKey = "C";
                }
                ++y;
            }

            lvP.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
