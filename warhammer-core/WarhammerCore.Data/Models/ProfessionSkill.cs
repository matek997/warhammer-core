using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class ProfessionSkill
    {
        public string ProfessionId { get; set; }
        public string SkillId { get; set; }

        public virtual Profession Profession { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
