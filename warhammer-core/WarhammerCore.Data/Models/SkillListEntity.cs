using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class SkillListEntity
    {
        public string ParentId { get; set; }
        public string ChildId { get; set; }

        public virtual SkillEntity Child { get; set; }
        public virtual SkillEntity Parent { get; set; }
    }
}
