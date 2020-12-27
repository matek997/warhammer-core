using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class AdvanceEntity
    {
        public string ProfessionId { get; set; }
        public string AdvanceTo { get; set; }

        public virtual ProfessionEntity AdvanceToNavigation { get; set; }
        public virtual ProfessionEntity Profession { get; set; }
    }
}
