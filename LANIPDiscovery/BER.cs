using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LANIPDiscovery {
    public class BER {
        Stream si;

        public BER(Stream si) {
            this.si = si;
        }

        byte[] bin = new byte[8];

        public byte ReadByte() {
            int v = si.ReadByte();
            if (v == -1) throw new EndOfStreamException();
            return (byte)v;
        }

        void ReadSurely(byte[] bin, int x, int cx) {
            while (cx > 0) {
                int r = si.Read(bin, x, cx);
                if (r < 1) throw new EndOfStreamException();
                x += r;
                cx -= r;
            }
        }

        public short ReadInt16() {
            ReadSurely(bin, 0, 2);
            uint r = bin[0];
            r <<= 8; r |= bin[1];
            return (short)r;
        }
        public ushort ReadUInt16() {
            ReadSurely(bin, 0, 2);
            uint r = bin[0];
            r <<= 8; r |= bin[1];
            return (ushort)r;
        }

        public int ReadInt32() {
            ReadSurely(bin, 0, 4);
            uint r = bin[0];
            r <<= 8; r |= bin[1];
            r <<= 8; r |= bin[2];
            r <<= 8; r |= bin[3];
            return (int)r;
        }
        public uint ReadUInt32() {
            ReadSurely(bin, 0, 4);
            uint r = bin[0];
            r <<= 8; r |= bin[1];
            r <<= 8; r |= bin[2];
            r <<= 8; r |= bin[3];
            return r;
        }

        public byte[] ReadBytes(int cb) {
            byte[] bin = new byte[cb];
            ReadSurely(bin, 0, cb);
            return bin;
        }
    }
}
