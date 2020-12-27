using System;
using System.Collections.Generic;

#nullable disable

namespace WarhammerCore.Data.Models
{
    public partial class SkillEntity
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Operator { get; set; }
        public string TargetEnum { get; set; }
        public string Label { get; set; }
    }
}
