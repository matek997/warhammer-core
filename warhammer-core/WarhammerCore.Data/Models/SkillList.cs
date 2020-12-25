using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class SkillList
    {
        public string ParentId { get; set; }
        public string ChildId { get; set; }

        public virtual Skill Child { get; set; }
        public virtual Skill Parent { get; set; }
    }
}
