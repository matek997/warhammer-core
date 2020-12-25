using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class ProfessionTrapping
    {
        public string ProfessionId { get; set; }
        public string TrappingId { get; set; }

        public virtual Profession Profession { get; set; }
        public virtual Trapping Trapping { get; set; }
    }
}
