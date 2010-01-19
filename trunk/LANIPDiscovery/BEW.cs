using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LANIPDiscovery {
    public class BEW {
        Stream os;

        public BEW(Stream os) {
            this.os = os;
        }

        byte[] bin = new byte[8];

        public void Write(byte v) {
            os.WriteByte(v);
        }

        public void Write(short v) {
            bin[0] = (byte)(v >> 8);
            bin[1] = (byte)(v >> 0);
            os.Write(bin, 0, 2);
        }
        public void Write(ushort v) {
            bin[0] = (byte)(v >> 8);
            bin[1] = (byte)(v >> 0);
            os.Write(bin, 0, 2);
        }
        public void Write(int v) {
            bin[0] = (byte)(v >> 24);
            bin[1] = (byte)(v >> 16);
            bin[2] = (byte)(v >> 8);
            bin[3] = (byte)(v >> 0);
            os.Write(bin, 0, 4);
        }
        public void Write(uint v) {
            bin[0] = (byte)(v >> 24);
            bin[1] = (byte)(v >> 16);
            bin[2] = (byte)(v >> 8);
            bin[3] = (byte)(v >> 0);
            os.Write(bin, 0, 4);
        }
        public void Write(byte[] al) {
            os.Write(al, 0, al.Length);
        }
    }
}
