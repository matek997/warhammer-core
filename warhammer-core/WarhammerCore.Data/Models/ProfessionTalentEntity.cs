using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class ProfessionTalentEntity
    {
        public string ProfessionId { get; set; }
        public string TalentId { get; set; }

        public virtual ProfessionEntity Profession { get; set; }
        public virtual TalentEntity Talent { get; set; }
    }
}
