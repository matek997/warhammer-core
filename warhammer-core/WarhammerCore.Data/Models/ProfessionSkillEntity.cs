using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class ProfessionSkillEntity
    {
        public string ProfessionId { get; set; }
        public string SkillId { get; set; }

        public virtual ProfessionEntity Profession { get; set; }
        public virtual SkillEntity Skill { get; set; }
    }
}
