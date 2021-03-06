﻿namespace WarhammerCore.Abstract.Models
{
    public class SecondaryProfile
    {
        public short A { get; set; }
        public short W { get; set; }
        public short Sb { get; set; }
        public short Tb { get; set; }
        public short M { get; set; }
        public short Mag { get; set; }
        public short Ip { get; set; }
        public short Fp { get; set; }

        public SecondaryProfile(short a, short w, short sb, short tb, short m, short mag, short ip, short fp)
        {
            A = a;
            W = w;
            Sb = sb;
            Tb = tb;
            M = m;
            Mag = mag;
            Ip = ip;
            Fp = fp;
        }
    }
}
