namespace WarhammerCore.Abstract.Models
{
    public class MainProfile
    {
        public short Ws { get; set; }
        public short Bs { get; set; }
        public short S { get; set; }
        public short T { get; set; }
        public short Ag { get; set; }
        public short Int { get; set; }
        public short Wp { get; set; }
        public short Fel { get; set; }

        public MainProfile(short ws, short bs, short s, short t, short ag, short intellect, short wp, short fel)
        {
            Ws = ws;
            Bs = ws;
            S = s;
            T = t;
            Ag = ag;
            Int = intellect;
            Wp = wp;
            Fel = fel;
        }
    }
}
