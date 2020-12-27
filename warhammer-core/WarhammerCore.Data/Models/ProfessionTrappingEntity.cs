using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class ProfessionTrappingEntity
    {
        public string ProfessionId { get; set; }
        public string TrappingId { get; set; }

        public virtual ProfessionEntity Profession { get; set; }
        public virtual TrappingEntity Trapping { get; set; }
    }
}
