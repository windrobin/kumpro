using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Diagnostics;

namespace LibROT {
    public class Moniker : IDisposable {
        public IMoniker moniker;
        IBindCtx pbc;

        [DllImport("ole32.dll")]
        static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);

        public Moniker(IMoniker moniker) {
            this.moniker = moniker;

            CreateBindCtx(0, out pbc);
        }

        public void Dispose() {
            if (moniker != null) Marshal.ReleaseComObject(moniker);
            moniker = null;
            if (pbc != null) Marshal.ReleaseComObject(pbc);
            pbc = null;
        }

        public string GetDisplayName() {
            string str = "";
            moniker.GetDisplayName(pbc, null, out str);
            return str;
        }
    }

    public class ROTUtil {
        [DllImport("ole32.dll")]
        static extern int GetRunningObjectTable(uint reserved, out IRunningObjectTable pprot);

        public static object GetObject(Moniker moniker) {
            IRunningObjectTable prot;
            GetRunningObjectTable(0, out prot);
            try {
                object punkObject = null;
                prot.GetObject(moniker.moniker, out punkObject);
                return punkObject;
            }
            finally { Marshal.ReleaseComObject(prot); }
        }

        public static Moniker[] EnumRunning() {
            IRunningObjectTable prot;
            GetRunningObjectTable(0, out prot);
            try {
                IEnumMoniker penumMoniker;
                prot.EnumRunning(out penumMoniker);
                try {
                    ArrayList al = new ArrayList();
                    IMoniker[] gelt = new IMoniker[] { null };
                    while (penumMoniker.Next(1, gelt, IntPtr.Zero) == 0) {
                        al.Add(new Moniker(gelt[0]));
                    }
                    return (Moniker[])al.ToArray(typeof(Moniker));
                }
                finally { Marshal.ReleaseComObject(penumMoniker); }
            }
            finally { Marshal.ReleaseComObject(prot); }
        }

        public static object GetObjectAs(Moniker key, Type type) {
            object o = GetObject(key);
            if (type.IsInstanceOfType(o))
                return o;
            Marshal.ReleaseComObject(o);
            return null;
        }
    }
}