using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class Profession
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsAdvanced { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public string MainProfile { get; set; }
        public string SecondaryProfile { get; set; }
        public short NumberOfAdvances { get; set; }
        public string Role { get; set; }

        public virtual MainProfile MainProfileNavigation { get; set; }
        public virtual SecondaryProfile SecondaryProfileNavigation { get; set; }
    }
}
