using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class VwProfessionSkillsWithParent
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ProfessionId { get; set; }
        public int? Depth { get; set; }
    }
}
