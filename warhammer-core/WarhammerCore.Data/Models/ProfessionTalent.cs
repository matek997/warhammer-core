using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class ProfessionTalent
    {
        public string ProfessionId { get; set; }
        public string TalentId { get; set; }

        public virtual Profession Profession { get; set; }
        public virtual Talent Talent { get; set; }
    }
}
