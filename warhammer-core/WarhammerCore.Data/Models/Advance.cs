using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class Advance
    {
        public string ProfessionId { get; set; }
        public string AdvanceTo { get; set; }

        public virtual Profession AdvanceToNavigation { get; set; }
        public virtual Profession Profession { get; set; }
    }
}
